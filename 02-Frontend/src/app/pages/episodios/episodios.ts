import { Component, OnInit, inject, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EpisodeService } from '../../core/services/episode.service';
import { RouterModule } from '@angular/router'; 
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-episodios',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './episodios.html',
  styleUrl: './episodios.css'
})
export class EpisodiosComponent implements OnInit {

  public showModal = signal(false);
  public selectedEpisode = signal<any>(null);
  public modalCharacters = signal<any[]>([]);
  public isModalLoading = signal(false);

  openModal(id: number) {
    this.showModal.set(true);
    this.isModalLoading.set(true);
    this.modalCharacters.set([]);

    this.episodeService.getEpisodeById(id).subscribe({
      next: (ep) => {
        this.selectedEpisode.set(ep);
        if (ep.characters && ep.characters.length > 0) {
          this.cargarPersonajesModal(ep.characters);
        } else {
          this.isModalLoading.set(false);
        }
      }
    });
  }
  private cargarPersonajesModal(urls: string[]) {
    const requests = urls.slice(0, 8).map(url => this.episodeService.getCharacterByUrl(url));
    forkJoin(requests).subscribe((chars: any) => {
      this.modalCharacters.set(chars);
      this.isModalLoading.set(false);
    });
  }

  closeModal() {
    this.showModal.set(false);
    this.selectedEpisode.set(null);
  }

  public episodeService = inject(EpisodeService);
  

  searchTerm = signal('');
  

  currentPage = signal(1);

  filteredEpisodes = computed(() => {
    const term = this.searchTerm().toLowerCase().trim();
    const results = this.episodeService.episodes();

    if (!term) return results;

    return results.filter(ep => 
      ep.name.toLowerCase().includes(term) || 
      ep.episode.toLowerCase().includes(term)
    );
  });

  ngOnInit(): void {
    this.loadPage(1);
  }
  onSearch(event: Event): void {
    const element = event.target as HTMLInputElement;
    this.searchTerm.set(element.value);
  }

  loadPage(page: number): void {
    if (page < 1) return;
    this.currentPage.set(page);
    this.episodeService.getEpisodes(page);
  
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }
}