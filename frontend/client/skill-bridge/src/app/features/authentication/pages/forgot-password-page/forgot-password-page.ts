import { CommonModule } from "@angular/common";
import { Component } from "@angular/core";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { MessageService } from "primeng/api";
import { ButtonModule } from "primeng/button";
import { InputTextModule } from "primeng/inputtext";
import { Router } from "@angular/router";

@Component({
    selector: 'app-forgot-password',
    standalone: true,
    templateUrl: './forgot-password-page.html',
    imports: [
      CommonModule,
      ReactiveFormsModule,
      InputTextModule,
      ButtonModule
    ],   
    styleUrls: ['./forgot-password-page.less'],
    providers: [MessageService]
  })  
  export class ForgotPassword {
    forgotForm!: FormGroup;
    loading = false;
  
    constructor(
      private fb: FormBuilder, 
      private messageService: MessageService,
      private router: Router
    ) {
      this.forgotForm = this.fb.group({
        email: ['', [Validators.required, Validators.email]]
      });
    }
  
    get email() {
      return this.forgotForm.get('email')!;
    }
  
    onSubmit() {
      if (this.forgotForm.valid) {
        this.loading = true;
        const emailValue = this.email.value;
        console.log('Enviar e-mail para:', emailValue);
        
        // Simular envio do e-mail
        setTimeout(() => {
          this.messageService.add({severity:'success', summary:'Sucesso', detail:'Link enviado para seu e-mail'});
          this.forgotForm.reset();
          this.loading = false;
        }, 2000);
      }
    }

    backToLogin() {
      this.router.navigate(['']);
    }
  }
  