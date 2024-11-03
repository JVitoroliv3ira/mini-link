import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LinkRoutingModule } from './link-routing.module';
import { ShortenLinkComponent } from './pages/shorten-link/shorten-link.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { ShortenLinkSectionComponent } from './components/shorten-link-section/shorten-link-section.component';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    ShortenLinkComponent,
    ShortenLinkSectionComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    SharedModule,
    LinkRoutingModule
  ]
})
export class LinkModule { }
