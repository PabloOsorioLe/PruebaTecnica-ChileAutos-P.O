import { Injectable, signal, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment'; // Importación genérica
import { Episode, EpisodeResponse } from '../models/episode.model';

@Injectable({ providedIn: 'root' })
export class EpisodeService {
  private http = inject(HttpClient);
  
  // Usamos la URL del environment
  private apiUrl = `${environment.apiUrl}/Episodes`; 

  episodes = signal<Episode[]>([]);
  isLoading = signal<boolean>(false);

  getEpisodes(page: number = 1) {
    this.isLoading.set(true);
    this.http.get<EpisodeResponse>(`${this.apiUrl}?page=${page}`).subscribe({
      next: (res) => {
        this.episodes.set(res.results);
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error('Error conectando al BFF:', err);
        this.isLoading.set(false);
      }
    });
  }
}