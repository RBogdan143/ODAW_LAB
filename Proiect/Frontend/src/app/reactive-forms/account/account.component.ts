import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule} from "@angular/forms";
import { AccountService } from '../../core/services/account.secvice';

@Component({
  selector: 'app-account',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.scss'],
})

export class AccountComponent {
  accountForm = this.formBuilder.group({
    username: null,
    email: null,
    password: null,
    FirstName: null,
    LastName: null
  })

  constructor(private readonly formBuilder: FormBuilder, private readonly accountService: AccountService) {

  }

  message: any;

  modify() {
    console.log(this.accountForm.value);
    this.message = null;
    this.accountService.UpdateProfile(this.accountForm.value).subscribe(
      data => {
        this.message = String(data.message);
      });
  }
}
