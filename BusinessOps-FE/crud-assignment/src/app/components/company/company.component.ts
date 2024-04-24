import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpService } from '../../service/http.service';
import { MatTableDataSource } from '@angular/material/table';
import { Company } from '../../../models/company.model';

@Component({
  selector: 'app-company',
  templateUrl: './company.component.html',
  styleUrls: ['./company.component.css']
})
export class CompanyComponent implements OnInit {
  dataSource: MatTableDataSource<Company>;
  displayedColumns: string[] = ['companyName', 'departmentNames', 'manage'];

  constructor(private router: Router, private _apiService: HttpService) {
    this.dataSource = new MatTableDataSource<Company>();
  }

  ngOnInit(): void {
    this._apiService.getCompany().subscribe({
      next: (res: Company[]) => {
        this.dataSource.data = res;
      },
      error: (err) => {
        console.log("CompanyComponent ~ this._apiService.getCompany ~ err:", err)
      }
    });

  }

  navigateToCreateCompany(): void {
    this.router.navigate(['/company', 'create']);
  }

  getDepartmentNames(departments: any[]): string {
    return departments.map(dept => dept.departmentName).join(', ');
  }
}
