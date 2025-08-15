import { Routes } from '@angular/router';
import { Dashboard } from './dashboard/dashboard';
import { Login } from './features/authentication/components/login/login';

export const routes: Routes = [
  { path: '', component: Login },
  { path: 'dashboard', component: Dashboard },
];
