#!/bin/bash

DB_CONTAINER_NAME="minilink_database_container"
DB_ADMIN_USER="minilink"  # Usuário para migrações (superprivilégios)
DB_ADMIN_PASSWORD="@myStrongPassword"
DB_APP_USER="dminilink"   # Usuário da aplicação (apenas leitura)
DB_APP_PASSWORD="@myStrongPassword"
DB_NAME="minilink"
DB_PORT="5432"
DB_HOST="localhost"
POSTGRES_PASSWORD="postgres_password"

check_container_running() {
    docker ps --filter "name=$DB_CONTAINER_NAME" --filter "status=running" | grep "$DB_CONTAINER_NAME" > /dev/null
    return $?
}

if check_container_running; then
    echo "O container $DB_CONTAINER_NAME já está em execução."
    exit 0
fi

echo "O container $DB_CONTAINER_NAME não está rodando. Criando o container PostgreSQL..."

docker run --name "$DB_CONTAINER_NAME" \
    -e POSTGRES_PASSWORD="$POSTGRES_PASSWORD" \
    -p "$DB_PORT:5432" \
    -d postgres

if [ $? -ne 0 ]; then
    echo "Falha ao criar o container PostgreSQL. Abortando..."
    exit 1
fi

MAX_TRIES=30
TRIES=0

echo "Aguardando o container $DB_CONTAINER_NAME iniciar..."

while ! docker exec "$DB_CONTAINER_NAME" pg_isready -h "$DB_HOST" -p "$DB_PORT" > /dev/null 2>&1; do
    TRIES=$((TRIES+1))
    
    if [ "$TRIES" -ge "$MAX_TRIES" ]; then
        echo "Container $DB_CONTAINER_NAME não está ativo após $MAX_TRIES tentativas. Abortando..."
        exit 1
    fi

    echo "Tentativa $TRIES de $MAX_TRIES: Aguardando container iniciar..."
    sleep 2
done

echo "Container $DB_CONTAINER_NAME está ativo!"

echo "Criando banco de dados, esquema e usuários..."

docker exec -i "$DB_CONTAINER_NAME" psql -U postgres <<EOF
-- Criar banco de dados
CREATE DATABASE $DB_NAME;

-- Criar usuários
CREATE USER $DB_ADMIN_USER WITH PASSWORD '$DB_ADMIN_PASSWORD' SUPERUSER; -- Usuário de migração com superprivilégios
CREATE USER $DB_APP_USER WITH PASSWORD '$DB_APP_PASSWORD';  -- Usuário da aplicação sem superprivilégios

-- Conceder todos os privilégios ao usuário de migração
GRANT ALL PRIVILEGES ON DATABASE $DB_NAME TO $DB_ADMIN_USER;

\c $DB_NAME

-- Dar permissão de leitura ao usuário de aplicação no esquema public
GRANT CONNECT ON DATABASE $DB_NAME TO $DB_APP_USER;
GRANT USAGE ON SCHEMA public TO $DB_APP_USER;
GRANT SELECT ON ALL TABLES IN SCHEMA public TO $DB_APP_USER;
ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT SELECT ON TABLES TO $DB_APP_USER;

EOF

if [ $? -eq 0 ]; then
    echo "Banco de dados $DB_NAME e usuários $DB_ADMIN_USER (superuser) e $DB_APP_USER (read-only) criados com sucesso, com permissões adequadas!"
else
    echo "Falha ao criar banco de dados e usuários."
    exit 1
fi
