import { Routes } from '@angular/router';
import { Login } from './features/authentication/pages/login/login';
import { Dashboard } from './features/dashboard/pages/dashboard/dashboard';
import { UserRegister } from './features/user-management/pages/user-register/user-register';

export const routes: Routes = [
  { path: '', component: Login },
  { path: 'dashboard', component: Dashboard },
  { path: 'user-register', component: UserRegister },
];
