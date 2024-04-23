import { Component, OnInit } from '@angular/core';
import { Employee } from '../../../models/company.model';
import { HttpService } from '../../service/http.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrl: './employee.component.css'
})
export class EmployeeComponent implements OnInit {

  employees: Employee[] = []
  constructor(public router: Router, private _apiService: HttpService) { }

  ngOnInit(): void {
    this._apiService.getEmployees().subscribe({
      next: (res: Employee[]) => {
        this.employees.push(...res);
      },
      error: (err) => {
        console.log("CompanyComponent ~ this.httpService.getCompany ~ err:", err)
      }
    })
  }


}
