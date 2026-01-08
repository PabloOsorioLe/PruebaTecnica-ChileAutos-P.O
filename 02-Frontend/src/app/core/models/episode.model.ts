export interface Episode {
  id: number;
  name: string;
  airDate: string;
  episode: string;
}

export interface EpisodeResponse {
  info: {
    count: number;
    pages: number;
    next: string | null;
    prev: string | null;
  };
  results: Episode[];
}