// estudiantes.service.ts
import { Injectable } from '@angular/core';
import { ApiService } from '../../service/api.service';
import { Student } from '../model/student';
import { catchError, map, Observable, throwError } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { SummaryCalification } from '../../app/dashboard/model/summary-calification';


@Injectable({
  providedIn: 'root'
})
export class StudentService extends ApiService<Student> {

    constructor(http: HttpClient) {
        super(http);
      }
   
    public List():Observable<ApiResponse<Student[]>> {
        return this.GetAll('student/listAll').pipe(map(_response=> {
            if (_response.Success) {
                _response.Value = Student.JsonInstaces(_response.Value);
              }
              return _response; 
        }))
      } 
      public ListToAttendence():Observable<ApiResponse<Student[]>> {
        return this.GetAll('student/listToAttendenceList').pipe(map(_response=> {
            if (_response.Success) {
                _response.Value = Student.JsonInstaces(_response.Value);
              }
              return _response; 
        }))
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
    
    public  Create(student: Student): Observable<ApiResponse<Student>> {
        return this.PostWithParam(student, 'student/create').pipe(
          map(_respon => {
            if (_respon.Success) {
              _respon.Value = Student.JsonInstance(_respon.Value);
            }
            return _respon; 
          })
        );
      }
      public  GetById(id: number): Observable<ApiResponse<Student>> {
        return this.getById(id, 'student').pipe(
          map(_respon => {
            if (_respon.Success) {
              _respon.Value = Student.JsonInstance(_respon.Value);
            }
            return _respon; 
          })
        );
      }
      public  Remove(id: number): Observable<ApiResponse<Student>> {
        return this.delete(id, 'student').pipe(
          map(_respon => {
            return _respon; 
          })
        );
      }
      public Update(student: Student): Observable<ApiResponse<Student>> {
        return this.Put(student, 'student/update').pipe(
          map(_respon => {
            if (_respon.Success) {
              _respon.Value = Student.JsonInstance(_respon.Value);
            }
            return _respon; 
          })
        );
      }
}

