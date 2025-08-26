import { Routes } from '@angular/router';
import { Login } from './features/authentication/pages/login-page/login-page';
import { Dashboard } from './features/dashboard/pages/dashboard/dashboard';
import { ConfirmEmailPage } from './features/authentication/pages/confirm-email-page/confirm-email-page';
import { RegisterUserPage } from './features/user-management';

export const routes: Routes = [
  { path: '', component: Login },
  { path: 'dashboard', component: Dashboard },
  { path: 'user-register', component: RegisterUserPage },
  {
    path: 'confirm-email/:userId/:token',
    component: ConfirmEmailPage,
  },
];
