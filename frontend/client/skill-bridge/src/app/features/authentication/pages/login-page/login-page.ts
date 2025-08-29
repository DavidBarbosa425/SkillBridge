import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { catchError, finalize, tap, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { NotificationService } from '../../../../core/services/notification';
import { AuthService } from '../../services/auth-service';
import { LoginRequest } from '../../models';

@Component({
  selector: 'app-login',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    InputTextModule,
    PasswordModule,
    ButtonModule,
    CardModule,
    CheckboxModule,
  ],
  templateUrl: './login-page.html',
  styleUrl: './login-page.less',
})
export class LoginPage {
  form: FormGroup;
  loading = false;

  private router = inject(Router);
  private loginService = inject(AuthService);
  private notificationService = inject(NotificationService);

  constructor(private fb: FormBuilder) {
    this.form = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      remember: [false],
    });
  }

  login() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const loginRequest: LoginRequest = this.form.value;

    this.loading = true;

    this.loginService
      .login(loginRequest)
      .pipe(
        tap(() => {
          this.router.navigate(['dashboard']);
        }),
        catchError((err) => {
          const apiMessage = err.error?.message;
          if (apiMessage) {
            this.notificationService.showError(apiMessage);
          }
          return throwError(() => err);
        }),
        finalize(() => (this.loading = false))
      )
      .subscribe();

    this.loading = false;
  }

  get email() {
    return this.form.get('email')!;
  }
  get password() {
    return this.form.get('password')!;
  }

  getEmailError() {
    if (this.email.hasError('required')) return 'E-mail é obrigatório.';
    if (this.email.hasError('email')) return 'E-mail inválido.';
    return '';
  }

  getPasswordError() {
    if (this.password.hasError('required')) return 'Senha é obrigatória.';
    if (this.password.hasError('minlength'))
      return 'A senha precisa ter no mínimo 6 caracteres.';
    return '';
  }

  onSubmit() {
    if (this.form.invalid) return;
    console.log('login payload', this.form.value);
  }

  forgot() {
    console.log('navegar para recuperar senha');
  }

  signup() {
    this.router.navigate(['register-user-page']);
  }

  social(provider: string) {
    console.log('login social', provider);
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.form.get(fieldName);
    return !!(field && field.invalid && (field.dirty || field.touched));
  }

  getFieldError(fieldName: string): string {
    const field = this.form.get(fieldName);
    if (field && field.errors && (field.dirty || field.touched)) {
      if (field.errors['required'])
        return `${this.getFieldLabel(fieldName)} é obrigatório`;
      if (field.errors['minlength'])
        return `${this.getFieldLabel(fieldName)} deve ter pelo menos ${field.errors['minlength'].requiredLength} caracteres`;
      if (field.errors['email']) return 'E-mail deve ter um formato válido';
    }
    return '';
  }

  private getFieldLabel(fieldName: string): string {
    const labels: { [key: string]: string } = {
      name: 'Nome',
      fullName: 'Nome Completo',
      preferredName: 'Nome Preferido',
      email: 'E-mail',
      password: 'Senha',
    };
    return labels[fieldName] || fieldName;
  }
}
