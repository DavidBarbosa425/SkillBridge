import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { catchError, Observable, tap, throwError } from 'rxjs';
import { ApiResult } from '../../../core/models/base/api-result';
import { RegisterUserRequest } from '../models';

@Injectable({
  providedIn: 'root',
})
export class UserApiService {
  apiUrl = `${environment.apiUrl}/auth`;

  private http = inject(HttpClient);

  register(userRegister: RegisterUserRequest): Observable<ApiResult<any>> {
    return this.http
      .post<ApiResult<any>>(`${this.apiUrl}/register`, userRegister)
      .pipe(
        tap((response) => {
          if (response.success && response.data) {
          }
        }),
        catchError((error) => {
          return throwError(() => error);
        })
      );
  }
}
