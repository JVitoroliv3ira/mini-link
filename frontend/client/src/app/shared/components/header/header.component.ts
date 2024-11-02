import { Component } from '@angular/core';

@Component({
  selector: 'mini-link-header',
  templateUrl: './header.component.html',
})
export class HeaderComponent {
  mobileMenuIsOpen = false;

  public toggleMobileMenuStatus(): void {
    this.mobileMenuIsOpen = !this.mobileMenuIsOpen;
  }
}
