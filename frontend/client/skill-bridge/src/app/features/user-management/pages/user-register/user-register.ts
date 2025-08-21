import { Component, inject, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { NotificationService } from '../../../../core/services/notification';

import { catchError, finalize, tap, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { UserApiService, UserRegisterRequest } from '../..';

@Component({
  selector: 'page-user-register',
  imports: [ReactiveFormsModule, ButtonModule, InputTextModule, PasswordModule],
  templateUrl: './user-register.html',
  styleUrls: ['./user-register.less'],
})
export class UserRegister implements OnInit {
  registerForm!: FormGroup;
  loading = false;
  showPassword = false;

  userService = inject(UserApiService);
  notificationService = inject(NotificationService);
  private router = inject(Router);

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  private initializeForm(): void {
    this.registerForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
      fullName: ['', [Validators.required, Validators.minLength(3)]],
      preferredName: ['', [Validators.required, Validators.minLength(2)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  onSubmit(): void {
    if (this.registerForm.invalid) {
      this.markFormGroupTouched();
      return;
    }

    this.loading = true;

    const userData: UserRegisterRequest = this.registerForm.value;

    this.userService
      .register(userData)
      .pipe(
        tap((response) => {
          this.notificationService.showSuccess(response.message);
          this.router.navigate(['/']);
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
  }

  private markFormGroupTouched(): void {
    Object.keys(this.registerForm.controls).forEach((key) => {
      const control = this.registerForm.get(key);
      control?.markAsTouched();
    });
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.registerForm.get(fieldName);
    return !!(field && field.invalid && (field.dirty || field.touched));
  }

  getFieldError(fieldName: string): string {
    const field = this.registerForm.get(fieldName);
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

  togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
  }
}
