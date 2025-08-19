import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { ApiResult } from '../../../core/models/base/api-result';
import { catchError, Observable, tap, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class DashboardService {
  url = 'https://localhost:44319/api/itServiceProviders';

  private http = inject(HttpClient);

  register(loginRequest: any): Observable<ApiResult<any>> {
    return this.http.post<ApiResult<any>>(`${this.url}`, loginRequest).pipe(
      tap((response) => {
        if (response.success && response.data) {
          alert('oi');
        }
      }),
      catchError((error) => {
        return throwError(() => error);
      })
    );
  }
}
