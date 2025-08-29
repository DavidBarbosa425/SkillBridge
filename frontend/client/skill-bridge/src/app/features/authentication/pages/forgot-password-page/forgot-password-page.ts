import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MessageService } from 'primeng/api';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth-service';
import { NotificationService } from '../../../../core/services/notification';
import { finalize, tap } from 'rxjs';
import { ForgotPasswordRequest } from '../../models';

@Component({
  selector: 'app-forgot-password',
  standalone: true,
  templateUrl: './forgot-password-page.html',
  imports: [CommonModule, ReactiveFormsModule, InputTextModule, ButtonModule],
  styleUrls: ['./forgot-password-page.less'],
  providers: [MessageService],
})
export class ForgotPasswordPage {
  forgotForm!: FormGroup;
  loading = false;
  private authService = inject(AuthService);
  private notificationService = inject(NotificationService);

  constructor(
    private fb: FormBuilder,
    private messageService: MessageService,
    private router: Router
  ) {
    this.forgotForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
    });
  }

  get email() {
    return this.forgotForm.get('email')!;
  }

  onSubmit() {
    if (this.forgotForm.valid) {
      this.loading = true;
      const forgotPasswordRequest: ForgotPasswordRequest =
        this.forgotForm.value;

      this.authService
        .forgotPassword(forgotPasswordRequest)
        .pipe(
          tap((response) => {
            if (response.success) {
              this.notificationService.showSuccess(response.message);
            } else {
              this.notificationService.showError(response.message);
            }
          }),
          finalize(() => (this.loading = false))
        )
        .subscribe();
    }
  }

  backToLogin() {
    this.router.navigate(['']);
  }
}
