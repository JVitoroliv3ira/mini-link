import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LinkRoutingModule } from './link-routing.module';
import { ShortenLinkComponent } from './pages/shorten-link/shorten-link.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { ShortenLinkSectionComponent } from './components/shorten-link-section/shorten-link-section.component';
import { FormsModule } from '@angular/forms';
import { ShortenedLinkResultComponent } from './components/shortened-link-result/shortened-link-result.component';
import { ShortenLinkFormComponent } from './components/shorten-link-form/shorten-link-form.component';
import {ClipboardModule} from "ngx-clipboard";



@NgModule({
  declarations: [
    ShortenLinkComponent,
    ShortenLinkSectionComponent,
    ShortenedLinkResultComponent,
    ShortenLinkFormComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ClipboardModule,
    SharedModule,
    LinkRoutingModule
  ]
})
export class LinkModule { }
