import { Injectable } from '@angular/core';
import { User } from '../models/auth/user';

@Injectable({
  providedIn: 'root',
})
export class StorageService {
  private readonly USER_KEY = 'user_data';
  private readonly EXPIRES_IN_KEY = 'expires_in';

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

  saveExpiresIn(expiresIn: number): void {
    sessionStorage.setItem(this.EXPIRES_IN_KEY, expiresIn.toString());
  }

  getExpiresIn(): number | null {
    const expiresIn = sessionStorage.getItem(this.EXPIRES_IN_KEY);
    return expiresIn ? parseInt(expiresIn) : null;
  }

  isTokenExpired(): boolean {
    const expiresIn = this.getExpiresIn();
    if (!expiresIn) return true;

    const expirationTime = Math.floor(Date.now() / 1000) + expiresIn;
    const currentTime = Math.floor(Date.now() / 1000);

    return currentTime >= expirationTime;
  }

  clearSession(): void {
    sessionStorage.removeItem(this.USER_KEY);
    sessionStorage.removeItem(this.EXPIRES_IN_KEY);
  }

  hasUser(): boolean {
    return !!this.getUser();
  }
}
