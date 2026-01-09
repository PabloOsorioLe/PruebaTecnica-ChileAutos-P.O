import { Routes } from '@angular/router';
import { EpisodiosComponent } from './pages/episodios/episodios';

export const routes: Routes = [
  { path: '', redirectTo: 'episodios', pathMatch: 'full' },
  { path: 'episodios', component: EpisodiosComponent },
  { path: '**', redirectTo: 'episodios' } 
];