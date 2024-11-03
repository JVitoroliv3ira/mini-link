import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { BaseService } from "../base.service";
import { ApiResponse } from '../../domain/dtos/responses/api-response';
import { ShortenLinkResponse } from '../../domain/dtos/responses/link/shorten-link-response';
import { ShortenLinkRequest } from '../../domain/dtos/requests/link/shorten-link-request';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LinkService extends BaseService {
  constructor(protected override http: HttpClient) {
    super(http, 'http://localhost:5067/api/v1/link');
  }

  public shorten(req: ShortenLinkRequest): Observable<ApiResponse<ShortenLinkResponse>> {
    return this.post<ShortenLinkRequest, ApiResponse<ShortenLinkResponse>>('/shorten', req);
  }
};

