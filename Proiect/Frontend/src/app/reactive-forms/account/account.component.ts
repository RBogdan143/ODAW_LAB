import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule} from "@angular/forms";
import { AccountService } from '../../core/services/account.secvice';

@Component({
  selector: 'app-account',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.scss'],
})

export class AccountComponent implements OnInit {
  accountForm = this.formBuilder.group({
    username: '',
    email: '',
    password: '',
    FirstName: '',
    LastName: ''
  })

  public account: Account = {
    firstName: '',
    lastName: '',
    username: '',
    email: '',
    password: ''
  };

  constructor(private readonly formBuilder: FormBuilder, private readonly accountService: AccountService) {
    
  }

  acces() {
    this.accountService.Profile().subscribe(data => {
      this.account = data;
      this.account.password = '*'.repeat(Number(this.account.password));
    }, error => console.error(error));
  }

  ngOnInit() {
    this.acces();
  }

  message: any;

  modify() {
    console.log(this.accountForm.value);
    this.message = null;
    if (Object.values(this.accountForm.value).some(value => value !== null && value !== '' && value.trim() !== ''))
    {
      for (let field in this.accountForm.value) {
        let val = (this.accountForm.value as { [key: string]: any })[field];
        if (typeof val === 'string' && val.trim() === '') {
          (this.accountForm.value as { [key: string]: any })[field] = null;
        }
      }
      this.accountService.UpdateProfile(this.accountForm.value).subscribe(
        data => {
          this.message = String(data.message);
        });
    }
    else this.message = "Nu aţi introdus încă date!";
  }
}

interface Account {
  firstName: string;
  lastName: string;
  username: string;
  email: string;
  password: string;
}
