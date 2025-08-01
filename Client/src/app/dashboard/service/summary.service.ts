import { Injectable } from '@angular/core';

import { catchError, map, Observable, throwError } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ApiService } from '../../../service/api.service';
import { SummaryCalification } from '../model/summary-calification';


@Injectable({
  providedIn: 'root'
})
export class SummaryService extends ApiService<SummaryCalification> {

    constructor(http: HttpClient) {
        super(http);
      }
   
public listCalificationSummary(): Observable<ApiResponse<SummaryCalification[]>> {
        return this.http.get<ApiResponse<SummaryCalification[]>>(`${this.api}/student/listCalificationSummary`)
          .pipe(
            catchError(error => {
              console.error('Error al obtener resumen de calificaciones', error);
              return throwError(() => new Error('Error al cargar el resumen'));
            })
          );
      }
    

  
}

