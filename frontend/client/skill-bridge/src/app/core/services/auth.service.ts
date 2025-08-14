import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { LoginRequest } from '../models/auth/login-request.model';
import { LoginResult } from '../models/auth/login-response.model';
import { BehaviorSubject, catchError, Observable, tap, throwError } from 'rxjs';
import { ApiResult } from '../models/base/api-result.model';
import { StorageService } from './storage.service';
import { User } from '../models/auth/user.model';
import { TokenResult } from '../models/auth/token.model';

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
          if (response.success && response.data) {
            this.handleAuthSuccess(response.data);
          }
        }),
        catchError((error) => {
          alert('Erro ao fazer login');
          // this.notificationService.showError('Erro ao fazer login');
          return throwError(() => error);
        })
      );
  }

  refreshToken(): Observable<ApiResult<TokenResult>> {
    const refreshToken = this.storageService.getRefreshToken();

    if (!refreshToken) {
      this.handleLogout();
      return throwError(() => new Error('No refresh token available'));
    }

    return this.http
      .post<
        ApiResult<TokenResult>
      >(`${this.url}/refresh-token`, { refreshToken })
      .pipe(
        tap((response) => {
          if (response.success && response.data) {
            this.storageService.setToken(response.data.token);
            this.storageService.setRefreshToken(response.data.refreshToken);
          }
        }),
        catchError((error) => {
          this.handleLogout();
          return throwError(() => error);
        })
      );
  }

  logout(): void {
    this.handleLogout();
  }

  isAuthenticated(): boolean {
    const token = this.storageService.getToken();
    return !!token && !this.isTokenExpired(token);
  }

  getCurrentUser(): User | null {
    return this.currentUserSubject.value;
  }

  getToken(): string | null {
    return this.storageService.getToken();
  }

  // ===== MÉTODOS PRIVADOS =====

  private handleAuthSuccess(authData: LoginResult): void {
    this.storageService.setToken(authData.token);
    this.storageService.setRefreshToken(authData.refreshToken);
    this.storageService.setUser(authData.user);

    this.currentUserSubject.next(authData.user);
    this.isAuthenticatedSubject.next(true);

    // Notificação de sucesso
    // this.notificationService.showSuccess(`Bem-vindo, ${authData.user.firstName}!`);
  }

  private handleLogout(): void {
    this.storageService.clearAuthData();
    this.currentUserSubject.next(null);
    this.isAuthenticatedSubject.next(false);

    // Notificação
    // this.notificationService.showInfo('Você foi desconectado');
  }

  private isTokenExpired(token: string): boolean {
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      const currentTime = Math.floor(Date.now() / 1000);
      return payload.exp < currentTime;
    } catch {
      return true;
    }
  }

  private checkStoredAuth(): void {
    const token = this.storageService.getToken();
    const user = this.storageService.getUser();

    if (token && user && !this.isTokenExpired(token)) {
      this.currentUserSubject.next(user);
      this.isAuthenticatedSubject.next(true);
    } else {
      this.handleLogout();
    }
  }
}
