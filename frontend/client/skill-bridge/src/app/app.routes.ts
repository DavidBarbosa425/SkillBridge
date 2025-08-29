import { Routes } from '@angular/router';
import { Login } from './features/authentication/pages/login-page/login-page';
import { Dashboard } from './features/dashboard/pages/dashboard/dashboard';
import { ConfirmEmailPage } from './features/authentication/pages/confirm-email-page/confirm-email-page';
import { RegisterUserPage } from './features/user-management';
import { ForgotPassword } from './features/authentication/pages/forgot-password-page/forgot-password-page';
import { ResetPasswordPage } from './features/authentication/pages/reset-password-page/reset-password-page';

export const routes: Routes = [
  { path: '', component: Login },
  { path: 'dashboard', component: Dashboard },
  { path: 'register-user-page', component: RegisterUserPage },
  {
    path: 'confirm-email/:userId/:token',
    component: ConfirmEmailPage,
  },
  {
    path: 'forgot-password',
    component: ForgotPassword,
  },
  {
    path: 'reset-password/:userId/:token',
    component: ResetPasswordPage,
  },
];
