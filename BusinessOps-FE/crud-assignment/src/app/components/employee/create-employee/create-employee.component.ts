import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpService } from '../../../service/http.service';
import { Company, Department, Employee } from '../../../../models/company.model';

@Component({
  selector: 'app-create-employee',
  templateUrl: './create-employee.component.html',
  styleUrl: './create-employee.component.css'
})
export class CreateEmployeeComponent implements OnInit {
  employeeForm = new FormGroup({
    name: new FormControl('', [Validators.required]),
    company: new FormControl(0, [Validators.required]),
    department: new FormControl(0, [Validators.required])
  })

  formType = 'create';
  employeeId: number = 0;
  companyList: Company[] = []
  departmentList: Department[] = []

  constructor(public router: Router,
    public avtivatedRoute: ActivatedRoute,
    private _apiService: HttpService
  ) { }

  ngOnInit(): void {

    this.getCompany()

    this.avtivatedRoute.params.subscribe((param) => {
      if (param['id']) {
        this.formType = 'update';
        this.employeeId = param['id'];
        this._apiService.getEmployeesById(param['id']).subscribe({
          next: (res: Employee) => {
            this.employeeForm.get('name')?.setValue(res.employeeName)
            this.employeeForm.get('company')?.setValue(res.companyId)
            this.employeeForm.get('department')?.setValue(res.departmentId)
            this.getDepartmentByCompanyId(res.companyId);
          }
        })
      }
    })
  }

  getCompany() {
    this._apiService.getCompany().subscribe({
      next: (res: Company[]) => {
        this.companyList.push(...res);
      },
      error: (err) => {
        console.log("err", err);
      }
    })
  }

  createEmployee() {
    let name = this.employeeForm.get('name')?.value?.trim() || '';
    if (!this.employeeForm.valid) return;

    const company = this.employeeForm.get('company')?.value || 0;
    const department = this.employeeForm.get('department')?.value || 0;


    const employeeData: Employee = {
      id: this.employeeId,
      employeeName: name,
      companyId: company,
      departmentId: department,
      companyName: '',
      departmentName: ''
    };

    this._apiService.upsertEmployee(employeeData).subscribe({
      next: (res: Company) => {
        this.router.navigate(['/employee'])
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  getDepartmentByCompanyId(id: number) {
    this.departmentList = [];
    this._apiService.getDepartmentByCompanyId(id).subscribe({
      next: (res: Department[]) => {
        this.departmentList.push(...res);
      }
    })
  }
}