import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpService } from '../../service/http.service';
import { Department } from '../../../models/company.model';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.css']
})
export class DepartmentComponent implements OnInit {

  deptForm = new FormGroup({
    dept_name: new FormControl('', [Validators.required]),
  });

  btnName = 'Create';
  departmentId: number = 0;
  departmentList: Department[] = [];

  constructor(public router: Router, private _apiService: HttpService) { }

  ngOnInit(): void {
    this.getAllDepartments();
  }

  getAllDepartments() {
    this._apiService.getDepartments().subscribe({
      next: (res: Department[]) => {
        this.departmentList = res;
      },
      error(err) {
        console.log("err", err);
      }
    });
  }

  update(id: number, name: string) {
    this.btnName = 'Update';
    this.deptForm.get('dept_name')?.setValue(name);
    this.departmentId = id;
  }

  cancel() {
    this.btnName = 'Create';
    this.deptForm.get('dept_name')?.setValue('');
    this.departmentId = 0;
  }

  createDepartment() {
    let name = this.deptForm.get('dept_name')?.value?.trim();
    if (typeof name === 'string') {
      this.deptForm.get('dept_name')?.setValue(name);
    }
    if (!this.deptForm.valid) return;

    const dept_info: Department = {
      id: this.departmentId,
      departmentName: name || ''
    }
    this._apiService.upsertDepartment(dept_info).subscribe({
      next: (res: Department) => {
        this.getAllDepartments();
        this.cancel();
        this.deptForm.reset();
      },
      error(err) {
        console.log("err", err);
      }
    });
  }

  delete(id: number) {
    if (confirm('Are you sure you want to delete this department?')) {
      this._apiService.deleteDepartment(id).subscribe({
        next: () => {
          this.getAllDepartments();
        },
        error(err) {
          console.log("err", err);
        }
      });
    }
  }
}
