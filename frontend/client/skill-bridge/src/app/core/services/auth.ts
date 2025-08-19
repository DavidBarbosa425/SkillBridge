import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { LoginRequest } from '../models/auth/login-request';
import { LoginResult } from '../models/auth/login-result';
import { BehaviorSubject, catchError, Observable, tap, throwError } from 'rxjs';
import { ApiResult } from '../models/base/api-result';
import { StorageService } from './storage';
import { User } from '../models/auth/user';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  url = 'https://localhost:44319/api/auth';

  private http = inject(HttpClient);
  private storageService = inject(StorageService);

  private currentUserSubject = new BehaviorSubject<User | null>(null);
  private isAuthenticatedSubject = new BehaviorSubject<boolean>(false);

  currentUser$ = this.currentUserSubject.asObservable();
  isAuthenticated$ = this.isAuthenticatedSubject.asObservable();

  login(loginRequest: LoginRequest): Observable<ApiResult<LoginResult>> {
    return this.http
      .post<ApiResult<LoginResult>>(`${this.url}/login`, loginRequest)
      .pipe(
        tap((response) => {
          console.log('response', response);
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
