import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { YeshivaStudent } from '../Models/YeshivaStudent';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class YeshivaStudentService {

  constructor(private http: HttpClient) { }
  private apiUrl = "https://localhost:7218/api/YeshivaStudent";
    GetYeshivaStudentById(id:string): Observable<YeshivaStudent> {
    return this.http.get<YeshivaStudent>(`${this.apiUrl}/GetById?YeshivaStudentId=${id}`)
}
    }