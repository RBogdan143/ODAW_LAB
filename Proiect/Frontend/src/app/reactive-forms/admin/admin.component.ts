import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from "@angular/forms";
import { AdminService } from '../../core/services/admin.service';
import { AdminGuard } from '../../core/guards/AdminGuard';

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

  ProdusForm = this.formBuilder.group({
    Id: '',
    Nume: ['', Validators.required],
    Pret: [Number(), Validators.required],
    Imagine: ['', Validators.required],
    Descriere: ['', Validators.required],
    IdStoc: '',
    Stoc: [Number()]
  })

  DiscountForm = this.formBuilder.group({
    Promo_Code: ['', Validators.required],
    Discount_Percent: [Number(), Validators.required]
  })

  constructor(private readonly formBuilder: FormBuilder, private readonly adminService: AdminService, private readonly adminGuard: AdminGuard) {
    
  }

  V = this.adminGuard.canActivate();

  message = {
    m1: '',
    m2: '',
    m3: '',
    m4: '',
    m5: '',
    m6: '',
    m7: '',
    m8: '',
    m9: '',
    m10: '',
    m11: '',
    m12: '',
    m13: '',
    m14: ''
  };

  UpdateRole() {
    console.log(this.RoleForm.value);
    this.message.m1 = '';
    if (this.RoleForm.valid) {
      let username = this.RoleForm.value.username;
      let role = this.RoleForm.value.role;
      if (role?.toLowerCase() == 'admin') {
        let role = 0;
        this.adminService.Update(username, role).subscribe(
          data => {
            this.message.m1 = String(data.message);
          },
          error => {
            this.message.m1 = String(error);
          });
      }
      else {
        let role = 1;
        this.adminService.Update(username, role).subscribe(
          data => {
            this.message.m1 = String(data.message);
          },
          error => {
            this.message.m1 = String(error);
          });
      }
    }
    else this.message.m1 = "Username-ul sau Rolul este invalid!";
  }

  RemoveRole() {
    console.log(this.RoleForm.value);
    this.message.m2 = '';
    if (this.RoleForm.value.username && this.RoleForm.value.username.trim() !== '')
    {
      let username = this.RoleForm.value.username;
      this.adminService.Remove(username).subscribe(
        data => {
          this.message.m2 = String(data.message);
        },
        error => {
          this.message.m2 = String(error);
        });
    }
    else this.message.m2 = "Username invalid!";
  }

  user: any;

  FindUser() {
    console.log(this.RoleForm.value);
    this.unloadUser();
    if (this.RoleForm.value.username && this.RoleForm.value.username.trim() !== '')
    {
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
          this.loadUser();
          this.message.m3 = '';
        },
        error => {
          this.message.m3 = String(error);
        });
      if (!this.user)
        this.message.m3 = "User-ul selectat nu exista!";
    }
    else this.message.m3 = "Username invalid!";
  }

  isUserLoaded = false;

  loadUser() {
    this.isUserLoaded = true;
  }

  unloadUser() {
    this.isUserLoaded = false;
  }

  DeleteUser() {
    console.log(this.RoleForm.value);
    this.message.m4 = '';
    if (this.RoleForm.value.username && this.RoleForm.value.username.trim() !== '')
    {
      let username = this.RoleForm.value.username;
      this.adminService.Delete(username).subscribe(
        data => {
          this.message.m4 = String(data.message);
        },
        error => {
          this.message.m4 = String(error);
        });
    }
    else this.message.m4 = "Username invalid!";
  }

  AddProdus() {
    console.log(this.ProdusForm.value);
    this.message.m5 = '';
    if (this.ProdusForm.valid && this.ProdusForm.value.Stoc != null && this.ProdusForm.value.Stoc > 0 && this.ProdusForm.value.Pret != null && this.ProdusForm.value.Pret > 0) {
      let stoc = {
        Nr_Produse: this.ProdusForm.value.Stoc
      };
      let produs = {
        Nume: this.ProdusForm.value.Nume,
        Pret: this.ProdusForm.value.Pret,
        Descriere: this.ProdusForm.value.Descriere,
        Imagine: this.ProdusForm.value.Imagine
      };
      this.adminService.CreateProd(produs, stoc).subscribe(
        data => {
          this.message.m5 = String(data.message);
        },
        error => {
          this.message.m5 = String(error);
        });
    }
    else this.message.m5 = "Nu aţi completat toate câmpurile necesare.";
  }

  AddStocPartener() {
    console.log(this.ProdusForm.value);
    this.message.m6 = '';
    if (this.ProdusForm.value.Id != null && this.ProdusForm.value.Id.trim() !== '' && this.ProdusForm.value.Stoc != null && this.ProdusForm.value.Stoc > 0)
    {
      let IdProd = this.ProdusForm.value.Id;
      let stoc = {
        Nr_Produse: this.ProdusForm.value.Stoc
      };
      this.adminService.AddStoc(stoc, IdProd).subscribe(
        data => {
          this.message.m6 = String(data.message);
        },
        error => {
          this.message.m6 = String(error);
        });
    }
    else this.message.m6 = "Nu aţi completat toate câmpurile necesare.";
  }

  AddDiscount() {
    console.log(this.DiscountForm.value);
    this.message.m7 = '';
    if (this.DiscountForm.valid && this.DiscountForm.value.Discount_Percent != null && this.DiscountForm.value.Discount_Percent > 0) {
      this.adminService.CreateDisc(this.DiscountForm.value).subscribe(
        data => {
          this.message.m7 = String(data.message);
        },
        response => {
          this.message.m7 = "Eroare necunoscută";
          if (response && typeof response === 'object' && 'error' in response) {
            let error = response.error;
            if (error && typeof error === 'object' && 'message' in error) {
              this.message.m7 = String(error.message);
            }
          }
        }
      );
    }
    else this.message.m7 = "Nu aţi completat toate câmpurile necesare.";
  }

  UpdateProdus() {
    console.log(this.ProdusForm.value);
    this.message.m8 = '';
    if (this.ProdusForm.valid && this.ProdusForm.value.Id != null && this.ProdusForm.value.Id.trim() !== '' && this.ProdusForm.value.Stoc != null && this.ProdusForm.value.Stoc > 0 && this.ProdusForm.value.IdStoc != null && this.ProdusForm.value.IdStoc.trim() !== '' && this.ProdusForm.value.Pret != null && this.ProdusForm.value.Pret > 0)
    {
      let stoc = {
        Nr_Produse: this.ProdusForm.value.Stoc
      };
      let produs = {
        Nume: this.ProdusForm.value.Nume,
        Pret: this.ProdusForm.value.Pret,
        Descriere: this.ProdusForm.value.Descriere,
        Imagine: this.ProdusForm.value.Imagine
      };
      let stocprod = {
        StocId: this.ProdusForm.value.IdStoc,
        Stoc: stoc,
        ProdusId: this.ProdusForm.value.Id,
        Produs: produs
      };
      this.adminService.UpdateProd(stocprod).subscribe(
        data => {
          this.message.m8 = String(data.message);
        },
        response => {
          this.message.m8 = "Eroare necunoscută";
          if (response && typeof response === 'object' && 'error' in response) {
            let error = response.error;
            if (error && typeof error === 'object' && 'message' in error) {
              this.message.m8 = String(error.message);
            }
          }
        }
      );
    }
    else this.message.m8 = "Nu aţi completat toate câmpurile necesare.";
  }

  ChangeStoc() {
    console.log(this.ProdusForm.value);
    this.message.m9 = '';
    if (this.ProdusForm.value.Stoc != null && this.ProdusForm.value.Stoc > 0 && this.ProdusForm.value.IdStoc != null && this.ProdusForm.value.IdStoc.trim() !== '')
    {
      let Id = this.ProdusForm.value.IdStoc;
      let Nr_Prod = this.ProdusForm.value.Stoc;
      this.adminService.UpdateStoc(Id, Nr_Prod).subscribe(
        data => {
          this.message.m9 = String(data.message);
        },
        response => {
          this.message.m9 = "Eroare necunoscută";
          if (response && typeof response === 'object' && 'error' in response) {
            let error = response.error;
            if (error && typeof error === 'object' && 'message' in error) {
              this.message.m9 = String(error.message);
            }
          }
        }
      );
    }
    else this.message.m9 = "Nu aţi completat toate câmpurile necesare.";
  }

  DeleteProdus() {
    console.log(this.ProdusForm.value);
    this.message.m10 = '';
    if (this.ProdusForm.value.Id != null && this.ProdusForm.value.Id.trim() !== '')
    {
      let produs = this.ProdusForm.value.Id;
      this.adminService.DelProd(produs).subscribe(
        data => {
          this.message.m10 = String(data.message);
        },
        response => {
          this.message.m10 = "Eroare necunoscută";
          if (response && typeof response === 'object' && 'error' in response) {
            let error = response.error;
            if (error && typeof error === 'object' && 'message' in error) {
              this.message.m10 = String(error.message);
            }
          }
        }
      );
    }
    else this.message.m10 = "Nu aţi completat toate câmpurile necesare.";
  }

  DeleteStocPart() {
    console.log(this.ProdusForm.value);
    this.message.m11 = '';
    if (this.ProdusForm.value.IdStoc != null && this.ProdusForm.value.IdStoc.trim() !== '') {
      let stoc = this.ProdusForm.value.IdStoc;
      this.adminService.DelStoc(stoc).subscribe(
        data => {
          this.message.m11 = String(data.message);
        },
        response => {
          this.message.m11 = "Eroare necunoscută";
          if (response && typeof response === 'object' && 'error' in response) {
            let error = response.error;
            if (error && typeof error === 'object' && 'message' in error) {
              this.message.m11 = String(error.message);
            }
          }
        }
      );
    }
    else this.message.m11 = "Nu aţi completat toate câmpurile necesare.";
  }

  DeleteDiscount() {
    console.log(this.ProdusForm.value);
    this.message.m12 = '';
    if (this.DiscountForm.value.Promo_Code != null && this.DiscountForm.value.Promo_Code.trim() !== '') {
      let promo = this.DiscountForm.value.Promo_Code;
      this.adminService.DelDisc(promo).subscribe(
        data => {
          this.message.m12 = String(data.message);
        },
        response => {
          this.message.m12 = "Eroare necunoscută";
          if (response && typeof response === 'object' && 'error' in response) {
            let error = response.error;
            if (error && typeof error === 'object' && 'message' in error) {
              this.message.m12 = String(error.message);
            }
          }
        }
      );
    }
    else this.message.m12 = "Nu aţi completat toate câmpurile necesare.";
  }

  produse: any;

  GetProdusStoc() {
    this.message.m13 = '';
    this.unloadProdus();
    this.adminService.GetProdStoc().subscribe(
      data => {
        this.produse = data;
        this.message.m13 = '';
        this.loadProdus();
      });
    if (!this.produse) {
      this.message.m13 = "Nu există produse în baza de date.";
    }
    console.log(this.produse);
  }

  isProdusLoaded = false;

  loadProdus() {
    this.isProdusLoaded = true;
  }

  unloadProdus() {
    this.isProdusLoaded = false;
  }

  discounts: any;

  GetDiscount() {
    this.unloadDiscount();
    this.message.m14 = '';
    this.adminService.GetDisc().subscribe(
      data => {
        this.discounts = data;
        this.message.m14 = '';
        this.loadDiscount();
      });
    if (!this.discounts) {
      this.message.m14 = "Nu există coduri promoţionale în baza de date.";
    }
    console.log(this.discounts);
  }

  isDiscountLoaded = false;

  loadDiscount() {
    this.isDiscountLoaded = true;
  }

  unloadDiscount() {
    this.isDiscountLoaded = false;
  }
}
