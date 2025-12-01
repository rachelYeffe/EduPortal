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
  private apiUrl =`${environment.apiUrl}`;
    GetYeshivaStudentById(id:string): Observable<YeshivaStudent> {
    return this.http.get<YeshivaStudent>(`${this.apiUrl}YeshivaStudent/GetById?YeshivaStudentId=${id}`)
}
    
    addYeshiva(form: FormData): Observable<number> {
      return this.http.post<number>(`${environment.apiUrl}ExcelImport/UploadExcelYeshivaStudent`, form);
    }
    }