import {Component, Input} from '@angular/core';
import {ClipboardService} from "ngx-clipboard";

@Component({
  selector: 'mini-link-shortened-link-result',
  templateUrl: './shortened-link-result.component.html',
})
export class ShortenedLinkResultComponent {
  @Input() originalUrl!: string;
  @Input() shortUrl!: string;
  public copied = false;

  constructor(private clipboardService: ClipboardService) {
  }

  public copyToClipboard(): void {
    this.clipboardService.copy(this.shortUrl);
    this.copied = true;
    setTimeout(() => {
      this.copied = false;
    }, 2000);
  }
}
