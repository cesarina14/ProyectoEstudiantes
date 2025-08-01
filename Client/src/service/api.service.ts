// src/app/services/api.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})

  
  export class ApiService <T> {
    private controller: string ='';
    readonly api = 'https://localhost:7231/api'; 
    constructor(protected http: HttpClient) {}
       
      PostWithParam(item: T, endpoint: string = '') {
        return this.http.post<ApiResponse<T>>(`${this.api}/${endpoint}`, item);
        }
      getById(id: number,endpoint: string = ''){
        return this.http.get<ApiResponse<T>>(`${this.api}/${endpoint}/${id}`);
    
      }
      Post(id: number,endpoint: string = ''){
        return this.http.post<ApiResponse<T>>(`${this.api}/${endpoint}/`,id);
    
      }
      delete(id: number,endpoint: string = ''){
        return this.http.delete<ApiResponse<T>>(`${this.api}/${endpoint}/${id}`);
    
      }
      Put(item: T, endpoint: string = '') {
        return this.http.put<ApiResponse<T>>(`${this.api}/${endpoint}`, item);
        }
        GetAll( endpoint: string = '') {
            return this.http.get<ApiResponse<T[]>>(`${this.api}/${endpoint}`);
      }
            PostList(items: T[], endpoint: string = '') {
              return this.http.post<ApiResponse<T>>(`${this.api}/${endpoint}`,items );
              }
  }
  

