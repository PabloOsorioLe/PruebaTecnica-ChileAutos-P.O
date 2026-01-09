import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideRouter } from '@angular/router';
// 1. Corregimos el nombre de la importación
import { EpisodiosComponent } from './episodios'; 

describe('EpisodiosComponent', () => {
  let component: EpisodiosComponent;
  let fixture: ComponentFixture<EpisodiosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      // 2. Importamos el componente con su nombre correcto
      imports: [EpisodiosComponent], 
      // 3. Importante: Agregamos proveedores para que el test no falle por falta de servicios
      providers: [
        provideHttpClient(),
        provideRouter([])
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EpisodiosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});