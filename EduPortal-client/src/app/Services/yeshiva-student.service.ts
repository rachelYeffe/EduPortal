import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { YeshivaStudent } from '../Models/YeshivaStudent';
import { Observable } from 'rxjs';
import { environment } from '../Enviroments/enviroment';
@Injectable({
  providedIn: 'root'
})
export class YeshivaStudentService {

  constructor(private http: HttpClient) { }
  private apiUrl =`${environment.apiUrl}YeshivaStudent`;
    GetYeshivaStudentById(id:string): Observable<YeshivaStudent> {
    return this.http.get<YeshivaStudent>(`${this.apiUrl}/GetById?YeshivaStudentId=${id}`)
}
    }