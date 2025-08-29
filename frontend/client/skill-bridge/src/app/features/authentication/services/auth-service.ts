import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments';
import {
  ConfirmEmailRequest,
  LoginRequest,
  LoginResult,
  ResetPasswordRequest,
} from '../models';
import { BehaviorSubject, catchError, Observable, tap, throwError } from 'rxjs';
import { ApiResult } from '../../../core';
import { HttpClient } from '@angular/common/http';
import { StorageService } from '../storage';
import { User } from '../../user-management';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  apiUrl = `${environment.apiUrl}/auth`;
  private http = inject(HttpClient);
  private storageService = inject(StorageService);

  private currentUserSubject = new BehaviorSubject<User | null>(null);
  private isAuthenticatedSubject = new BehaviorSubject<boolean>(false);

  currentUser$ = this.currentUserSubject.asObservable();
  isAuthenticated$ = this.isAuthenticatedSubject.asObservable();

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

  resetPassword(
    resetPasswordRequest: ResetPasswordRequest
  ): Observable<ApiResult<boolean>> {
    return this.http
      .post<
        ApiResult<boolean>
      >(`${this.apiUrl}/reset-password`, resetPasswordRequest)
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

  login(loginRequest: LoginRequest): Observable<ApiResult<LoginResult>> {
    return this.http
      .post<ApiResult<LoginResult>>(`${this.apiUrl}/login`, loginRequest)
      .pipe(
        tap((response) => {
          if (response.success && response.data) {
            this.handleAuthSuccess(response.data);
          }
        }),
        catchError((error) => {
          return throwError(() => error);
        })
      );
  }

  logout(): void {
    this.handleLogout();
  }

  getCurrentUser(): User | null {
    return this.currentUserSubject.value;
  }

  private handleAuthSuccess(authData: LoginResult): void {
    this.storageService.saveUser(authData.user);
    this.storageService.saveExpiresIn(authData.expiresIn);

    this.currentUserSubject.next(authData.user);
    this.isAuthenticatedSubject.next(true);
  }

  private handleLogout(): void {
    this.storageService.clearSession();
    this.currentUserSubject.next(null);
    this.isAuthenticatedSubject.next(false);
  }
}
