// pages/confirm-email/confirm-email-page.component.ts
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { ProgressSpinnerModule } from 'primeng/progressspinner';

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

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.userId = this.route.snapshot.paramMap.get('userId');
    this.token = this.route.snapshot.paramMap.get('token');

    // simula chamada de API
    setTimeout(() => {
      this.loading = false;
      this.confirmed = true; // supondo sucesso
    }, 2000);
  }
}
