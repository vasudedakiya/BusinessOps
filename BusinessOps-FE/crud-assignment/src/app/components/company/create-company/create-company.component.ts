import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpService } from '../../../service/http.service';
import { Company, Department, UpsertCompany } from '../../../../models/company.model';

@Component({
  selector: 'app-create-company',
  templateUrl: './create-company.component.html',
  styleUrl: './create-company.component.css'
})
export class CreateCompanyComponent implements OnInit {

  companyForm = new FormGroup({
    name: new FormControl('', [Validators.required]),
    department: new FormControl([0], [Validators.required])
  })

  formType = 'create';
  companyId: number = 0
  departmentList: Department[] = []

  constructor(public router: Router,
    public avtivatedRoute: ActivatedRoute,
    private _apiService: HttpService
  ) { }

  ngOnInit(): void {

    this.getDepartment()

    this.avtivatedRoute.params.subscribe((param) => {
      if (param['id']) {
        this.formType = 'update';
        this.companyId = param['id'];
        this._apiService.getCompanyById(param['id']).subscribe({
          next: (res: Company) => {
            this.companyForm.get('name')?.setValue(res.companyName)
            let departmentIds: number[] = res.departments?.map(x => x.id) || [];
            this.companyForm.get('department')?.setValue(departmentIds)
          }
        })
      }
    })
  }

  getDepartment() {
    this._apiService.getDepartments().subscribe({
      next: (res: Department[]) => {
        this.departmentList.push(...res);
      },
      error: (err) => {
        console.log("err", err);
      }
    })
  }

  createCompany() {
    let name = this.companyForm.get('name')?.value?.trim();
    if (!name || !this.companyForm.valid) return;

    const departments = this.companyForm.get('department')?.value || [];
    const departmentArray: number[] = departments.filter(departmentId => departmentId !== 0);

    const companyData: UpsertCompany = {
      id: this.companyId,
      companyName: name,
      departmentIds: departmentArray
    };
    console.log("Runnnnn");
    this._apiService.upsertCompany(companyData).subscribe({
      next: (res: Company) => {
        this.router.navigate(['/company'])
      },
      error: (err) => {
        console.log(err);
        this.router.navigate(['/company'])
      }
    });
  }
}
