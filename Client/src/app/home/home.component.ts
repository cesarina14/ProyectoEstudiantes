import { CommonModule } from '@angular/common';
import { Component, OnInit, signal } from '@angular/core';
import { Router, RouterModule, RouterOutlet } from '@angular/router';
import { CardModule } from 'primeng/card';
import { routes } from '../app.routes';
import { ConfirmationService, MenuItem } from 'primeng/api';


@Component({
  selector: 'app-home-component',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  standalone:false
})


export class HomeComponent implements OnInit 
{
    navRoutes = [
        { path: 'students', title: 'Estudiantes', icon: 'pi pi-users' },
        { path: 'attendence', title: 'Asistencia', icon: 'pi pi-calendar' }
      ];



  constructor(private router: Router
  ) {}
          ngOnInit(): void {

  }




  goTo(path: string) {
    
    this.router.navigate([path]);
  
}
}
