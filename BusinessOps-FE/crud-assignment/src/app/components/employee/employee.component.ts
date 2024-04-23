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
  displayedColumns: string[] = ['employeeName', 'companyName', 'departmentName', 'edit', 'delete'];

  constructor(public router: Router, private _apiService: HttpService) { }

  ngOnInit(): void {
    this.getAllEmployee();
  }

  getAllEmployee() {
    this._apiService.getEmployees().subscribe({
      next: (res: Employee[]) => {
        this.employees = res;
      },
      error: (err) => {
        console.log("CompanyComponent ~ this.httpService.getCompany ~ err:", err)
      }
    })
  }

  deleteEmployee(id: number) {
    if (confirm('Are you sure you want to delete this Employee?')) {
      this._apiService.deleteEmployee(id).subscribe({
        next: () => {
          this.getAllEmployee();
        },
        error(err) {
          console.log("err", err);
        }
      });
    }
  }
}
