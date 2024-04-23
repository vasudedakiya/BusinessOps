import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpService } from '../../service/http.service';
import { Company } from '../../../models/company.model';

@Component({
  selector: 'app-company',
  templateUrl: './company.component.html',
  styleUrl: './company.component.css'
})

export class CompanyComponent implements OnInit {
  company: Company[] = []
  constructor(public router: Router, public _apiService: HttpService) { }

  ngOnInit(): void {
    this._apiService.getCompany().subscribe({
      next: (res: Company[]) => {
        this.company.push(...res);
      },
      error: (err) => {
        console.log("CompanyComponent ~ this._apiService.getCompany ~ err:", err)
      }
    })
  }

}
