import { Component, signal } from '@angular/core';
import { Router, RouterModule, RouterOutlet } from '@angular/router';
import { CardModule } from 'primeng/card';
import {routes} from '../app/app.routes'
import { CommonModule } from '@angular/common';
import { AppModule } from '../AppModule';
import { MenuItem } from 'primeng/api';
import { MenuModule } from 'primeng/menu';
import { ButtonModule } from 'primeng/button';
import { MenubarModule } from 'primeng/menubar';
import { DrawerModule } from 'primeng/drawer';
import { FormsModule } from '@angular/forms';

@Component({
  standalone: true,
  selector: 'app-root',
  templateUrl: './app.html',
  styleUrls: ['./app.css'],
  host: { ngSkipHydration: '' },
  imports: [RouterModule,MenuModule,ButtonModule,CommonModule,MenuModule,DrawerModule,FormsModule]
})
export class App {
  public MenuVisible = false;
  public MenuItems: MenuItem[] = [];

constructor(private router: Router) {

  
}
  ngOnInit() {
    this.MenuItems = [
      { label: 'Asistencia', icon: 'pi pi-list',command: () => this.goTo('attendence') },
      { label: 'Estudiantes', icon: 'pi pi-users',command: () => this.goTo('students') },
      { label: 'Dashboard', icon: 'pi pi-users',command: () => this.goTo('dashboard') },
      { separator: true },
      { label: 'Home', icon: 'pi pi-home',command: () => this.goTo('home') }
    ];
  }


  goTo(path: string) {
    const currentUrl = this.router.url;
    if (currentUrl === '/' + path) {
      this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
        this.router.navigate([path]);
      });
    } else {
      this.router.navigate([path]);
    }
  }
public toggleMenu() {
  this.MenuVisible = !this.MenuItems;
}
}

