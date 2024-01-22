import { Inject, Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private readonly apiUrl: string;

  constructor(private readonly httpClient: HttpClient, @Inject('https://localhost:7146/api') apiUrl: string) {
    this.apiUrl = 'https://localhost:7146/api/';
  }

  get<T>(path: string, params?: any): Observable<any>{
    return this.httpClient.get<T>(`${this.apiUrl}${path}`, params);
  }

  put<T>(path: string, body = {}, options?: any): Observable<any>{
    return this.httpClient.put<T>(`${this.apiUrl}${path}`, body, options);
  }

  post<T>(path: string, body = {}, options?: any): Observable<any>{
    return this.httpClient.post<T>(`${this.apiUrl}${path}`, body, options);
  }

  delete<T>(path: string, options?: any): Observable<any>{
    return this.httpClient.delete<T>(`${this.apiUrl}${path}`, options);
  }
}
