#!/bin/bash

DB_CONTAINER_NAME="minilink_database_container"
DB_ADMIN_USER="minilink"
DB_ADMIN_PASSWORD="@myStrongPassword"
DB_APP_USER="dminilink"
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

echo "Criando banco de dados e usuários..."

docker exec -i "$DB_CONTAINER_NAME" psql -U postgres <<EOF
CREATE DATABASE $DB_NAME;
CREATE USER $DB_ADMIN_USER WITH PASSWORD '$DB_ADMIN_PASSWORD';
CREATE USER $DB_APP_USER WITH PASSWORD '$DB_APP_PASSWORD';
GRANT ALL PRIVILEGES ON DATABASE $DB_NAME TO $DB_ADMIN_USER;
EOF

if [ $? -eq 0 ]; then
    echo "Banco de dados $DB_NAME e usuários $DB_ADMIN_USER (admin) e $DB_APP_USER (aplicação) criados com sucesso!"
else
    echo "Falha ao criar banco de dados e usuários."
    exit 1
fi
