import { HttpErrorResponse } from '@angular/common/http';
import {Component, ViewEncapsulation} from '@angular/core';
import { finalize, take } from 'rxjs';
import { ShortenLinkRequest } from 'src/app/core/domain/dtos/requests/link/shorten-link-request';
import { ApiResponse } from 'src/app/core/domain/dtos/responses/api-response';
import { ShortenLinkResponse } from 'src/app/core/domain/dtos/responses/link/shorten-link-response';
import { LinkService } from 'src/app/core/services/api/link.service';

@Component({
  selector: 'mini-link-shorten-link-section',
  templateUrl: './shorten-link-section.component.html',
  styleUrls: ['./shorten-link-section.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ShortenLinkSectionComponent {
  loading = false;

  constructor(private linkService: LinkService) { }

  public shortenLink(originalUrl: string): void {
    this.loading = true;
    this.linkService
      .shorten({ originalUrl } as ShortenLinkRequest)
      .pipe(take(1), finalize(() => this.loading = false))
      .subscribe({
        next: (res: ApiResponse<ShortenLinkResponse>) => {
          console.log(res);
        },
        error: (err: HttpErrorResponse) => {
          console.log(err);
        }
      })
  }
}
