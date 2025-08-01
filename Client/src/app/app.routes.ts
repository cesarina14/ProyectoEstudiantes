import { RouterModule, Routes } from '@angular/router';
import { StudentComponent } from '../Student/component/student.component';
import { StudentListComponent } from '../Student/component/component-list/student.component-list';
import { NgModule } from '@angular/core';
import { HomeComponent } from './home/home.component';
import { AttendanceComponent } from './attendence/component/attendence.component';
import { DashboardComponent } from './dashboard/component/dashboard.component';


   export const routes: Routes = [
        { path: '', redirectTo: 'home', pathMatch: 'full' },
        { path: 'home', component: HomeComponent }, 
        { path: 'students', component: StudentListComponent },
        { path: 'attendence', component: AttendanceComponent },
        { path: 'dashboard', component: DashboardComponent },
        
      ];
  
@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
  })
  export class AppRoutingModule { }