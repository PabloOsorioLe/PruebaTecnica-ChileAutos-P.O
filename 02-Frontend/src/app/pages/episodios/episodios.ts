import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EpisodeService } from '../../core/services/episode.service';

@Component({
  selector: 'app-episodios',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './episodios.html',
  styleUrl: './episodios.css'
})
export class EpisodiosComponent implements OnInit {
  // Inyección del servicio (Patrón moderno)
  public episodeService = inject(EpisodeService);
  
  // Signal local para manejar la paginación
  currentPage = signal(1);

  ngOnInit(): void {
    this.loadPage(1);
  }

  loadPage(page: number): void {
    if (page < 1) return;
    this.currentPage.set(page);
    this.episodeService.getEpisodes(page);
    
    // Scroll al inicio al cambiar de página
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }
}