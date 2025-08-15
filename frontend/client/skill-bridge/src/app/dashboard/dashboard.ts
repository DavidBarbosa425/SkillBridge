import { Component, OnInit } from '@angular/core';
import { AuthService } from '../core/services/auth.service';
import { LoginRequest } from '../core/models/auth/login-request.model';

@Component({
  selector: 'app-dashboard',
  imports: [],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.less',
})
export class Dashboard {}
