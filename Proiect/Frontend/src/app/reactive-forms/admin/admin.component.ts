import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from "@angular/forms";
import { AdminService } from '../../core/services/admin.service';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss'],
})

export class AdminComponent {
  RoleForm = this.formBuilder.group({
    username: [String(), Validators.required],
    role: [String(), Validators.required]
  })

  constructor(private readonly formBuilder: FormBuilder, private readonly adminService: AdminService) {
    
  }
  
  message: any;

  UpdateRole() {
    console.log(this.RoleForm.value);
    this.message = null;
    if (this.RoleForm.valid) {
      let username = this.RoleForm.value.username;
      let role = this.RoleForm.value.role;
      if (role?.toLowerCase() == 'admin') {
        let role = 0;
        this.adminService.Update(username, role).subscribe(
          data => {
            this.message = String(data.message);
          },
          error => {
            this.message = String(error);
          });
      }
      else {
        let role = 1;
        this.adminService.Update(username, role).subscribe(
          data => {
            this.message = String(data.message);
          },
          error => {
            this.message = String(error);
          });
      }
    }
    else this.message = "Username-ul sau Rolul este invalid!";
  }

  RemoveRole() {
    console.log(this.RoleForm.value);
    this.message = null;
    let username = this.RoleForm.value.username;
    this.adminService.Remove(username).subscribe(
      data => {
        this.message = String(data.message);
      },
      error => {
        this.message = String(error);
      });
  }

  user: any;

  FindUser() {
    console.log(this.RoleForm.value);
    this.message = null;
    let username = this.RoleForm.value.username;
    this.adminService.GetUser(username).subscribe(
      data => {
        this.user = data;
        // Verifică valoarea lui user.role și atribuie corespunzător
        if (this.user.role === 1) {
          this.user.role = 'Utilizator';
        } else if (this.user.role === 0) {
          this.user.role = 'Admin';
        }
      },
      error => {
        this.message = String(error);
      });
  }

  DeleteUser() {
    console.log(this.RoleForm.value);
    this.message = null;
    let username = this.RoleForm.value.username;
    this.adminService.Delete(username).subscribe(
      data => {
        this.message = String(data.message);
      },
      error => {
        this.message = String(error);
      });
  }
}
