// components.module.ts
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; // si usas ngModel
import { StudentComponent } from './Student/component/student.component';
import { TableModule } from 'primeng/table';
import { PaginatorModule } from 'primeng/paginator';
import {DialogModule} from 'primeng/dialog';
import {CardModule} from 'primeng/card'
import { App } from './app/app';
import { HomeComponent } from './app/home/home.component';
import { RouterModule, RouterOutlet } from '@angular/router';
import { AppRoutingModule } from './app/app.routes';
import { ButtonModule } from 'primeng/button';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ConfirmationService } from 'primeng/api';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { MenuModule } from 'primeng/menu';
import { MenubarModule } from 'primeng/menubar';
import { DashboardComponent } from './app/dashboard/component/dashboard.component';
import { ChartModule } from 'primeng/chart';
@NgModule({
  declarations: 
  [HomeComponent
  ],
  imports: [
    ConfirmDialogModule,
    FormsModule,
    TableModule,
    PaginatorModule,
    DialogModule,
    CardModule,
    RouterModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserModule,
    ButtonModule,
    MenuModule,
    MenubarModule,
    BrowserAnimationsModule
    


  ],
  providers: [ConfirmationService]
})
export class AppModule {}
