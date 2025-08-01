// estudiantes.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiService } from '../../../service/api.service';
import {  Attendence } from '../models/attendance';
import { Observable } from 'rxjs/internal/Observable';
import { map } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class AttendenceService extends ApiService<Attendence> {

    constructor(http: HttpClient) {
        super(http);
      }

   
            public List():Observable<ApiResponse<Attendence[]>> {
                return this.GetAll('attendence/listAll').pipe(map(_response=> {
                    if (_response.Success) {
                        _response.Value = Attendence.JsonInstaces(_response.Value);
                      }
                      return _response; 
                }))
              } 
      
    
       public Create(student: Attendence[]): Observable<ApiResponse<Attendence>> {
             return this.PostList(student, 'attendence/saveattedencebatch').pipe(
               map(_respon => {
                 if (_respon.Success) {
                   _respon.Value = Attendence.JsonInstance(_respon.Value);
                 }
                 return _respon; 
               })
             );
           }
         public  GetById(id: number): Observable<ApiResponse<Attendence>> {
             return this.getById(id, 'attendence').pipe(
               map(_respon => {
                 if (_respon.Success) {
                   _respon.Value = Attendence.JsonInstance(_respon.Value);
                 }
                 return _respon; 
               })
             );
           }
         public  Remove(id:number): Observable<ApiResponse<Attendence>> {
             return this.Post(id, 'attendence/remove').pipe(
               map(_respon => {
                 if (_respon.Success) {
                   _respon.Value = Attendence.JsonInstance(_respon.Value);
                 }
                 return _respon; 
               })
             );
           }
         public  Update(student: Attendence): Observable<ApiResponse<Attendence>> {
             return this.Put(student, 'attendence/update').pipe(
               map(_respon => {
                 if (_respon.Success) {
                   _respon.Value = Attendence.JsonInstance(_respon.Value);
                 }
                 return _respon; 
               })
             );
           }
}

