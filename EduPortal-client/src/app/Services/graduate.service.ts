import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Graduate } from '../Models/Graduate';
import { Observable } from 'rxjs';
import { environment } from '../Enviroments/enviroment';
@Injectable({
  providedIn: 'root'
})
export class GraduateService {
  constructor(private http: HttpClient) { }
  private apiUrl = `${environment.apiUrl}Graduate`;
  GetGraduateById(id:string): Observable<Graduate> {
  return this.http.get<Graduate>(`${this.apiUrl}/GetById?GraduateId=${id}`);
  }}
