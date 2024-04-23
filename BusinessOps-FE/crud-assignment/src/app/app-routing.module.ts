import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NavigationEnum } from './enum/navigation';
import { CompanyComponent } from './components/company/company.component';
import { CreateCompanyComponent } from './components/company/create-company/create-company.component';
import { DepartmentComponent } from './components/department/department.component';
import { EmployeeComponent } from './components/employee/employee.component';
import { CreateEmployeeComponent } from './components/employee/create-employee/create-employee.component';

const routes: Routes = [
  {
    path: NavigationEnum.COMPANY,
    component: CompanyComponent
  },
  {
    path: [NavigationEnum.COMPANY, NavigationEnum.CREATE].join('/'),
    component: CreateCompanyComponent
  },
  {
    path: [NavigationEnum.COMPANY, NavigationEnum.UPDATE, ':id'].join('/'),
    component: CreateCompanyComponent
  },
  {
    path: NavigationEnum.DEPARTMENT,
    component: DepartmentComponent
  },
  {
    path: NavigationEnum.EMPLOYEE,
    component: EmployeeComponent
  },
  {
    path: [NavigationEnum.EMPLOYEE, NavigationEnum.CREATE].join('/'),
    component: CreateEmployeeComponent
  },
  {
    path: [NavigationEnum.EMPLOYEE, NavigationEnum.UPDATE, ':id'].join('/'),
    component: CreateEmployeeComponent
  },
  {
    path: '**',
    redirectTo: NavigationEnum.COMPANY,
    pathMatch: 'full'
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
