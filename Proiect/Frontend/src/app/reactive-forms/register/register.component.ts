import {Component} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import { AuthentificationService } from '../../core/services/authentification.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent {
  loginForm = this.formBuilder.group({
    username: ['', Validators.required],
    password: ['', Validators.required]
  })

  registerForm = new FormGroup({
    username: new FormControl('', Validators.required),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', Validators.required)
  })

  constructor(private readonly formBuilder: FormBuilder, private readonly authentificationService: AuthentificationService) {

  }

  message: any;

  register() {
    console.log(this.registerForm.value);
    this.message = null;
    if (this.registerForm.valid)
      this.authentificationService.register(this.registerForm.value).subscribe(data => {
          this.message = String(data.message);
      },
        response => {
          this.message = "Eroare necunoscută";
          if (response && typeof response === 'object' && 'error' in response) {
            let error = response.error;
            if (error && typeof error === 'object' && 'message' in error) {
              this.message = String(error.message);
            }
          }
        }
      );
    else this.message = "User registration failed.";
  }

  login() {
    console.log(this.loginForm.value);
    this.message = null;
    if (this.loginForm.valid)
    {
      this.authentificationService.login(this.loginForm.value).subscribe(
        data => {
          localStorage.setItem('token', data.token);
          this.message = "User logged in successfully!";
          console.log(localStorage.getItem('token'));
          setTimeout(() => {
            this.logout();
          }, 1800000);
        },
        response => {
          this.message = "Eroare necunoscută";
          if (response && typeof response === 'object' && 'error' in response) {
            let error = response.error;
            if (error && typeof error === 'object' && 'message' in error) {
              this.message = String(error.message);
            }
          }
        }
      );
    }
    else this.message = "User authentication failed.";
  }

  logout() {
    this.message = null;
    if (localStorage.getItem('token'))
      localStorage.removeItem('token');
    this.message = "User logged out successfully!";
  }
}
