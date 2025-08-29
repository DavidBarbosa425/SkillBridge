import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  ReactiveFormsModule,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { CardModule } from 'primeng/card';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { ButtonModule } from 'primeng/button';
import { MessageModule } from 'primeng/message';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { DividerModule } from 'primeng/divider';
import { RippleModule } from 'primeng/ripple';
import { ToastModule } from 'primeng/toast';

import { MessageService } from 'primeng/api';
import { NotificationService } from '../../../../core/services/notification';
import { AuthService } from '../../services/auth-service';
import { finalize, tap } from 'rxjs';

@Component({
  selector: 'app-reset-password',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    CardModule,
    InputTextModule,
    PasswordModule,
    ButtonModule,
    MessageModule,
    ProgressSpinnerModule,
    DividerModule,
    RippleModule,
    ToastModule,
  ],
  providers: [MessageService],
  templateUrl: './reset-password-page.html',
  styleUrls: ['./reset-password-page.less'],
})
export class ResetPasswordPage implements OnInit {
  resetForm: FormGroup;
  isLoading = false;
  isSubmitted = false;
  hasValidParams = false;
  userId: string = '';
  token: string = '';
  private authService = inject(AuthService);

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private notificationService: NotificationService
  ) {
    this.resetForm = this.fb.group(
      {
        newPassword: ['', [Validators.required, Validators.minLength(8)]],
        confirmPassword: ['', [Validators.required]],
      },
      { validators: this.passwordMatchValidator }
    );
  }

  ngOnInit() {
    this.route.params.subscribe((params) => {
      this.userId = params['userId'];
      this.token = params['token'];

      if (this.userId && this.token) {
        this.hasValidParams = true;
      } else {
        this.showInvalidLinkError();
      }
    });
  }

  private showInvalidLinkError() {
    this.notificationService.showError(
      'O link para redefinir a senha é inválido ou expirou. Solicite um novo link.'
    );
  }

  passwordMatchValidator(form: FormGroup) {
    const password = form.get('newPassword');
    const confirmPassword = form.get('confirmPassword');

    if (
      password &&
      confirmPassword &&
      password.value !== confirmPassword.value
    ) {
      confirmPassword.setErrors({ passwordMismatch: true });
      return { passwordMismatch: true };
    }

    return null;
  }

  onSubmit() {
    if (!this.hasValidParams) {
      this.showInvalidLinkError();
      return;
    }

    this.isSubmitted = true;

    if (this.resetForm.valid) {
      this.isLoading = true;

      const formData = {
        userId: this.userId,
        token: this.token,
        newPassword: this.resetForm.value.newPassword,
      };

      this.authService
        .resetPassword(formData)
        .pipe(
          tap((response) => {
            if (response.success) {
              this.notificationService.showSuccess(response.message);
            } else {
              this.notificationService.showError(response.message);
            }
          }),
          finalize(() => (this.isLoading = false))
        )
        .subscribe();
    }
  }

  getFieldError(fieldName: string): string {
    const field = this.resetForm.get(fieldName);
    if (
      field &&
      field.errors &&
      (field.dirty || field.touched || this.isSubmitted)
    ) {
      if (field.errors['required'])
        return `${this.getFieldLabel(fieldName)} é obrigatório`;
      if (field.errors['minlength'])
        return 'Senha deve ter pelo menos 8 caracteres';
      if (field.errors['pattern'])
        return 'Senha deve conter ao menos: 1 maiúscula, 1 minúscula, 1 número e 1 caractere especial';
      if (field.errors['passwordMismatch']) return 'Senhas não coincidem';
    }
    return '';
  }

  private getFieldLabel(fieldName: string): string {
    const labels: { [key: string]: string } = {
      newPassword: 'Nova senha',
      confirmPassword: 'Confirmação da senha',
    };
    return labels[fieldName] || fieldName;
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.resetForm.get(fieldName);
    return !!(
      field &&
      field.errors &&
      (field.dirty || field.touched || this.isSubmitted)
    );
  }
}
