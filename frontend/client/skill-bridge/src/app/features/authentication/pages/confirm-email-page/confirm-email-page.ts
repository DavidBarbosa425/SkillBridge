// pages/confirm-email/confirm-email-page.component.ts
import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { AuthService } from '../../services/auth-service';
import { ConfirmEmailRequest } from '../../models';
import { finalize, tap } from 'rxjs';

@Component({
  selector: 'app-confirm-email-page',
  imports: [
    CommonModule,
    RouterModule,
    CardModule,
    ButtonModule,
    ProgressSpinnerModule,
  ],
  templateUrl: './confirm-email-page.html',
  styleUrls: ['./confirm-email-page.less'],
})
export class ConfirmEmailPage implements OnInit {
  userId: string | null = null;
  token: string | null = null;
  loading = true;
  confirmed = false;

  private route = inject(ActivatedRoute);
  private authService = inject(AuthService);

  ngOnInit(): void {
    const confirmEmailRequest: ConfirmEmailRequest = {
      userId: this.route.snapshot.paramMap.get('userId') ?? '',
      token: this.route.snapshot.paramMap.get('token') ?? '',
    };

    this.authService
      .confirmEmail(confirmEmailRequest)
      .pipe(
        tap((response) => (this.confirmed = response.success)),
        finalize(() => (this.loading = false))
      )
      .subscribe();
  }
}
