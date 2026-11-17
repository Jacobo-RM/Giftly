import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GiftSuggestion } from '../Models/GiftSuggestion';
import { GiftRequest } from '../Models/GiftRequest';

@Injectable({ providedIn: 'root' })
export class GiftService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5105/api/gifts';

  suggest(req: GiftRequest): Observable<GiftSuggestion[]> {
    return this.http.post<GiftSuggestion[]>(this.apiUrl, req);
  }
}