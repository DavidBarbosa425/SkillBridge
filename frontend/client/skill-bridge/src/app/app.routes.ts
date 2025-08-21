import { Routes } from '@angular/router';
import { Login } from './features/authentication/pages/login/login';
import { Dashboard } from './features/dashboard/pages/dashboard/dashboard';
import { RegisterUser } from './features/user-management/pages/register-user/register-user';

export const routes: Routes = [
  { path: '', component: Login },
  { path: 'dashboard', component: Dashboard },
  { path: 'register-user', component: RegisterUser },
];
