import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MessageService } from 'primeng/api';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';

interface UserRegisterModel {
  name: string;
  fullName: string;
  preferredName: string;
  email: string;
  password: string;
}

@Component({
  selector: 'app-user-register',
  imports: [ReactiveFormsModule, ButtonModule, InputTextModule, PasswordModule],
  templateUrl: './user-register.html',
  styleUrls: ['./user-register.less'],
})
export class UserRegisterComponent implements OnInit {
  registerForm!: FormGroup;
  loading = false;
  showPassword = false;

  constructor(
    private fb: FormBuilder,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  private initializeForm(): void {
    this.registerForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
      fullName: ['', [Validators.required, Validators.minLength(3)]],
      preferredName: ['', [Validators.required, Validators.minLength(2)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  onSubmit(): void {
    if (this.registerForm.valid) {
      this.loading = true;
      const userData: UserRegisterModel = this.registerForm.value;

      // Simular chamada da API
      setTimeout(() => {
        this.messageService.add({
          severity: 'success',
          summary: 'Sucesso',
          detail: 'Usuário cadastrado com sucesso!',
        });
        this.loading = false;
        this.registerForm.reset();
      }, 2000);
    } else {
      this.markFormGroupTouched();
      this.messageService.add({
        severity: 'error',
        summary: 'Erro',
        detail:
          'Por favor, preencha todos os campos obrigatórios corretamente.',
      });
    }
  }

  private markFormGroupTouched(): void {
    Object.keys(this.registerForm.controls).forEach((key) => {
      const control = this.registerForm.get(key);
      control?.markAsTouched();
    });
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.registerForm.get(fieldName);
    return !!(field && field.invalid && (field.dirty || field.touched));
  }

  getFieldError(fieldName: string): string {
    const field = this.registerForm.get(fieldName);
    if (field && field.errors && (field.dirty || field.touched)) {
      if (field.errors['required'])
        return `${this.getFieldLabel(fieldName)} é obrigatório`;
      if (field.errors['minlength'])
        return `${this.getFieldLabel(fieldName)} deve ter pelo menos ${field.errors['minlength'].requiredLength} caracteres`;
      if (field.errors['email']) return 'E-mail deve ter um formato válido';
    }
    return '';
  }

  private getFieldLabel(fieldName: string): string {
    const labels: { [key: string]: string } = {
      name: 'Nome',
      fullName: 'Nome Completo',
      preferredName: 'Nome Preferido',
      email: 'E-mail',
      password: 'Senha',
    };
    return labels[fieldName] || fieldName;
  }

  togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
  }
}
