import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { LoginModel } from "../../login/models/login-model";
import { LoginRequest } from "../models/auth/login-request.model";
import { LoginResponse } from "../models/auth/login-response.model";
import { catchError, Observable, tap, throwError } from "rxjs";
import { ApiResponse } from "../models/base/api-response.model";
import { StorageService } from "./storage.service";
import { TokenModel } from "../models/auth/token.model";

@Injectable({
  providedIn: 'root'
})

export class AuthService {

url = "https://localhost:44319/api/auth/"

  private http = inject(HttpClient);
  private storageService = inject(StorageService);

    login(loginRequest: LoginRequest){
     return this.http.post<ApiResponse<LoginResponse>>(
      `${this.url}/login`, 
      loginRequest
    ).pipe(
      tap(response => {
        if (response.success && response.data) {
          this.handleAuthSuccess(response.data);
        }
      }),
      catchError(error => {
        alert('Erro ao fazer login')
        // this.notificationService.showError('Erro ao fazer login');
        return throwError(() => error);
      })
    );
  }

   

    refreshToken(): Observable<ApiResponse<TokenModel>> {
    const refreshToken = this.storageService.getRefreshToken();
    
    return this.http.post<ApiResponse<TokenModel>>(
      `${this.url}/refresh-token`, 
      { refreshToken }
    ).pipe(
      tap(response => {
        if (response.success && response.data) {
          this.storageService.setToken('response.data.token');
          this.storageService.setRefreshToken('response.data.refreshToken');
        }
      }),
      catchError(error => {
        this.handleLogout();
        return throwError(() => error);
      })
    );
  }

  isAuthenticated(): boolean {
    const token = this.storageService.getToken();
    return !!token && !this.isTokenExpired(token);
  }

  // ✅ Obter usuário atual
  // getCurrentUser(): UserModel | null {
  //   return this.currentUserSubject.value;
  // }

  // ✅ Obter token atual
  getToken(): string | null {
    return this.storageService.getToken();
  }

  // ===== MÉTODOS PRIVADOS =====

  private handleAuthSuccess(authData: LoginResponse): void {
    // Salvar tokens
    this.storageService.setToken(authData.token);
    this.storageService.setRefreshToken(authData.refreshToken);
    
    // Salvar dados do usuário
    this.storageService.setUser(authData.user);
    
    // Atualizar subjects
    // this.currentUserSubject.next(authData.user);
    // this.isAuthenticatedSubject.next(true);
    
    // Notificação de sucesso
    // this.notificationService.showSuccess(`Bem-vindo, ${authData.user.firstName}!`);
  }

  private handleLogout(): void {
    // Limpar storage
    this.storageService.clearAuthData();
    
    // Atualizar subjects
    // this.currentUserSubject.next(null);
    // this.isAuthenticatedSubject.next(false);
    
    // Notificação
    // this.notificationService.showInfo('Você foi desconectado');
  }

  private checkStoredAuth(): void {
    const token = this.storageService.getToken();
    const user = this.storageService.getUser();
    
    if (token && user && !this.isTokenExpired(token)) {
      // this.currentUserSubject.next(user);
      // this.isAuthenticatedSubject.next(true);
    } else {
      this.handleLogout();
    }
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

}