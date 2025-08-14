import { Injectable } from "@angular/core";
import { UserModel } from "../models/auth/user.model";

@Injectable({
  providedIn: 'root'
})
export class StorageService {
  
  setToken(token: string): void {
    localStorage.setItem('APP_CONFIG.tokenKey', token);
  }

  getToken(): string | null {
    return localStorage.getItem('APP_CONFIG.tokenKey');
  }

  setRefreshToken(refreshToken: string): void {
    localStorage.setItem('APP_CONFIG.refreshTokenKey', refreshToken);
  }

  getRefreshToken(): string | null {
    return localStorage.getItem('APP_CONFIG.refreshTokenKey');
  }

  setUser(user: UserModel): void {
    localStorage.setItem('APP_CONFIG.userKey', JSON.stringify(user));
  }

  getUser(): UserModel | null {
    const userStr = localStorage.getItem('APP_CONFIG.userKey');
    return userStr ? JSON.parse(userStr) : null;
  }

  clearAuthData(): void {
    localStorage.removeItem('APP_CONFIG.tokenKey');
    localStorage.removeItem('APP_CONFIG.refreshTokenKey');
    localStorage.removeItem('APP_CONFIG.userKey');
  }
}