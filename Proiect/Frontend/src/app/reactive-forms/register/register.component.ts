import {Component} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import { AuthenticationService } from '../../core/services/authentication.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent {
  registerForm = this.formBuilder.group({
    username: ['', Validators.required],
    email: ['', Validators.email],
    password: ['', Validators.required],
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
  })

  registerForm2 = new FormGroup({
    username: new FormControl('', Validators.required),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', Validators.required)
  })

  constructor(private readonly formBuilder: FormBuilder, private readonly authenticationService: AuthenticationService) {

  }

  message: any;

  register() {
    console.log(this.registerForm2.value);
    if (this.registerForm2.valid)
      this.authenticationService.register(this.registerForm2.value).subscribe(data => {
        this.message = String(data.message);
      });
    else this.message = "User registration failed.";
  }

}
