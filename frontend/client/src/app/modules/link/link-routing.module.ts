import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ShortenLinkComponent } from './pages/shorten-link/shorten-link.component';

const routes: Routes = [
  {
    path: '', component: ShortenLinkComponent
  }
];


@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LinkRoutingModule { }
