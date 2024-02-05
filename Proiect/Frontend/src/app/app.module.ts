import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { MatCardModule } from '@angular/material/card';

import { AppComponent } from './app.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { RegisterComponent } from './reactive-forms/register/register.component';
import { AccountComponent } from './reactive-forms/account/account.component';
import { AdminComponent } from './reactive-forms/admin/admin.component';
import { AuthGuard } from './core/guards/AuthGuard';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatGridListModule } from '@angular/material/grid-list';
import { ImagePreloaderDirective } from './core/directives/image-preloader.directive';
import { ProdusComponent } from './Pagina_Produs/produs.component'

@NgModule({
  declarations: [
    AppComponent,
    FetchDataComponent,
    ImagePreloaderDirective,
    ProdusComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    MatCardModule,
    RouterModule.forRoot([
      { path: 'register', component: RegisterComponent },
      { path: 'account', component: AccountComponent, canActivate: [AuthGuard] },
      { path: 'admin', component: AdminComponent},
      { path: 'fetchData', component: FetchDataComponent },
      { path: 'produs', component: ProdusComponent }
    ]),
    MatPaginatorModule,
    MatGridListModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
