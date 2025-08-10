import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SearchResult } from '../Models/SearchResult';

@Injectable({
  providedIn: 'root',
})
export class ChildrenExcelUploadService {

  constructor(private http: HttpClient) { }
  private apiUrl = "https://localhost:7218/api/Search";
  GetResultByRows(form:FormData): Observable<SearchResult[]> {
    return this.http.post<SearchResult[]>(`${this.apiUrl}`,form);
  }

}
