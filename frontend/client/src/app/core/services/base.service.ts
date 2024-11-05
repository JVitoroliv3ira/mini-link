import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
  import { environment } from '../../../environments/environment';

export abstract class BaseService {
  protected baseUrl: string;

  constructor(protected http: HttpClient, controllerPath: string) {
    this.baseUrl = `${environment.apiUrl}/${controllerPath}`;
  }

  protected get<R>(path: string = '', params?: HttpParams, headers?: HttpHeaders): Observable<R> {
    return this.http.get<R>(`${this.baseUrl}/${path}`, { params, headers });
  }

  protected post<T, R>(path: string = '', body: T, headers?: HttpHeaders): Observable<R> {
    return this.http.post<R>(`${this.baseUrl}/${path}`, body, { headers });
  }

  protected put<T, R>(path: string = '', body: T, headers?: HttpHeaders): Observable<R> {
    return this.http.put<R>(`${this.baseUrl}/${path}`, body, { headers });
  }

  protected patch<T, R>(path: string = '', body: T, headers?: HttpHeaders): Observable<R> {
    return this.http.patch<R>(`${this.baseUrl}/${path}`, body, { headers });
  }

  protected delete<R>(path: string = '', params?: HttpParams, headers?: HttpHeaders): Observable<R> {
    return this.http.delete<R>(`${this.baseUrl}/${path}`, { params, headers });
  }
}
