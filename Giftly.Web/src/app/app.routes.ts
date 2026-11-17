import { Routes } from '@angular/router';
import { GiftIdeaComponent } from './Components/gift-idea-component/gift-idea-component';

export const routes: Routes = [
  { path: '', component: GiftIdeaComponent },
  { path: 'gidtIdea', component: GiftIdeaComponent },
  { path: '**', redirectTo: '' },
];
