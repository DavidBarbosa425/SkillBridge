import { Injectable } from '@angular/core';
import { User } from '../models/auth/user.model';

@Injectable({
  providedIn: 'root',
})
export class StorageService {
  private readonly TOKEN_KEY = 'auth_token';
  private readonly REFRESH_TOKEN_KEY = 'refresh_token';
  private readonly USER_KEY = 'user_data';

  saveAccessToken(token: string): void {
    sessionStorage.setItem(this.TOKEN_KEY, token);
  }

  getAccessToken(): string | null {
    return sessionStorage.getItem(this.TOKEN_KEY);
  }

  saveRefreshToken(refreshToken: string): void {
    sessionStorage.setItem(this.REFRESH_TOKEN_KEY, refreshToken);
  }

  getRefreshToken(): string | null {
    return sessionStorage.getItem(this.REFRESH_TOKEN_KEY);
  }

  saveUser(user: User): void {
    try {
      sessionStorage.setItem(this.USER_KEY, JSON.stringify(user));
    } catch (error) {
      console.error('Erro ao salvar usuário no sessionStorage:', error);
    }
  }

  getUser(): User | null {
    try {
      const userStr = sessionStorage.getItem(this.USER_KEY);
      return userStr ? (JSON.parse(userStr) as User) : null;
    } catch (error) {
      console.error('Erro ao recuperar usuário do sessionStorage:', error);
      return null;
    }
  }

  clearSession(): void {
    sessionStorage.removeItem(this.TOKEN_KEY);
    sessionStorage.removeItem(this.REFRESH_TOKEN_KEY);
    sessionStorage.removeItem(this.USER_KEY);
  }

  hasToken(): boolean {
    return !!this.getAccessToken();
  }

  hasUser(): boolean {
    return !!this.getUser();
  }

  isAuthenticated(): boolean {
    return this.hasToken() && this.hasUser();
  }

  getSessionInfo(): {
    hasToken: boolean;
    hasUser: boolean;
    isAuthenticated: boolean;
  } {
    return {
      hasToken: this.hasToken(),
      hasUser: this.hasUser(),
      isAuthenticated: this.isAuthenticated(),
    };
  }
}
