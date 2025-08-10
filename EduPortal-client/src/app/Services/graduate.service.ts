import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Graduate } from '../Models/Graduate';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GraduateService {
  constructor(private http: HttpClient) { }
  private apiUrl = "https://localhost:7218/api/Graduate";
  GetGraduateById(id:string): Observable<Graduate> {
  return this.http.get<Graduate>(`${this.apiUrl}/GetById?GraduateId=${id}`);
  }}
