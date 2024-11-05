import {Component, EventEmitter, Input, Output} from '@angular/core';

@Component({
  selector: 'mini-link-shorten-link-form',
  templateUrl: './shorten-link-form.component.html'
})
export class ShortenLinkFormComponent {
  @Input() url: string = '';
  @Input() loading = false;
  @Output() shortenLink = new EventEmitter<string>();

  public onSubmit(): void {
    this.shortenLink.emit(this.url);
  }
}
