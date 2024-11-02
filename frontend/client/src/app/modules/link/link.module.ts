import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LinkRoutingModule } from './link-routing.module';
import { ShortenLinkComponent } from './pages/shorten-link/shorten-link.component';
import { SharedModule } from 'src/app/shared/shared.module';



@NgModule({
  declarations: [
    ShortenLinkComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    LinkRoutingModule
  ]
})
export class LinkModule { }
