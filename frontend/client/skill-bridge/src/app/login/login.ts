import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { LoginService } from './services/login-service';
import { LoginModel } from './models/login-model';
import { LoginRequest } from '../core/models/auth/login-request.model';
import { AuthService } from '../core/services/auth.service';

@Component({
  selector: 'app-login',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    InputTextModule,
    PasswordModule,
    ButtonModule,
    CardModule,
    CheckboxModule,
  ],
  templateUrl: './login.html',
  styleUrl: './login.less',
})
export class Login {
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private loginService: AuthService
  ) {
    this.form = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      remember: [false],
    });
  }

  login() {
    debugger;
    const loginRequest: LoginRequest = {
      email: this.form.value.email,
      password: this.form.value.password,
      rememberMe: this.form.value.rememberMe || false,
    };
    this.loginService.login(loginRequest).subscribe((res) => console.log(res));
  }

  get email() {
    return this.form.get('email')!;
  }
  get password() {
    return this.form.get('password')!;
  }

  getEmailError() {
    if (this.email.hasError('required')) return 'E-mail é obrigatório.';
    if (this.email.hasError('email')) return 'E-mail inválido.';
    return '';
  }

  getPasswordError() {
    if (this.password.hasError('required')) return 'Senha é obrigatória.';
    if (this.password.hasError('minlength'))
      return 'A senha precisa ter no mínimo 6 caracteres.';
    return '';
  }

  onSubmit() {
    if (this.form.invalid) return;
    console.log('login payload', this.form.value);
  }

  forgot() {
    console.log('navegar para recuperar senha');
  }

  signup() {
    console.log('navegar para cadastro');
  }

  social(provider: string) {
    console.log('login social', provider);
  }
}
