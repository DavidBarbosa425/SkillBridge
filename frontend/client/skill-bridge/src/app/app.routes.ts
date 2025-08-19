import { Routes } from '@angular/router';
import { Login } from './features/authentication/pages/login/login';
import { Dashboard } from './features/dashboard/pages/dashboard/dashboard';

export const routes: Routes = [
  { path: '', component: Login },
  { path: 'dashboard', component: Dashboard },
];
