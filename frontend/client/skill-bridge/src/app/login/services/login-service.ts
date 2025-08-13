import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginModel } from '../models/login-model';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  url = "https://localhost:44319/api/auth/login"

  constructor(private httpClient:HttpClient) { }

    login(loginModel: LoginModel){
    return this.httpClient.post(this.url, loginModel)
  }
  
}
