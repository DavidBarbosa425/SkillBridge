import { Routes } from '@angular/router';
import { RegisterUserPage } from './features/user-management';
import {
  ConfirmEmailPage,
  ForgotPasswordPage,
  LoginPage,
  ResetPasswordPage,
} from './features/authentication';
import { DashboardPage } from './features/dashboard';

export const routes: Routes = [
  { path: '', component: LoginPage },
  { path: 'dashboard', component: DashboardPage },
  { path: 'register-user', component: RegisterUserPage },
  {
    path: 'confirm-email/:userId/:token',
    component: ConfirmEmailPage,
  },
  {
    path: 'forgot-password',
    component: ForgotPasswordPage,
  },
  {
    path: 'reset-password/:userId/:token',
    component: ResetPasswordPage,
  },
];
