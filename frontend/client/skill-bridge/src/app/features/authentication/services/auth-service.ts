import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments';
import { ConfirmEmailRequest } from '../models';
import { catchError, Observable, tap, throwError } from 'rxjs';
import { ApiResult } from '../../../core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  apiUrl = `${environment.apiUrl}/auth`;
  private http = inject(HttpClient);

  confirmEmail(
    confirmEmailRequest: ConfirmEmailRequest
  ): Observable<ApiResult<boolean>> {
    return this.http
      .post<
        ApiResult<boolean>
      >(`${this.apiUrl}/confirm-email`, confirmEmailRequest)
      .pipe(
        tap((response) => {
          if (response.success) {
            // this.handleAuthSuccess(response.data);
          }
        }),
        catchError((error) => {
          return throwError(() => error);
        })
      );
  }
}
