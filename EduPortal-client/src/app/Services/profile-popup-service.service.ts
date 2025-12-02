import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../Enviroments/enviroment';
import { Observable } from 'rxjs';
import { YeshivaStudent } from '../Models/YeshivaStudent';
import { Graduate } from '../Models/Graduate';

@Injectable({
  providedIn: 'root'
})
export class ProfilePopupServiceService {

  constructor(private http: HttpClient) { }
  private apiUrl = `${environment.apiUrl}`;
  GetYeshivaStudentById(id: string): Observable<YeshivaStudent> {
    return this.http.get<YeshivaStudent>(`${this.apiUrl}YeshivaStudent/GetById?YeshivaStudentId=${id}`)
  }
  GetGraduateById(id: string): Observable<Graduate> {
    return this.http.get<Graduate>(`${this.apiUrl}Graduate/GetById?GraduateId=${id}`);
  }
}
