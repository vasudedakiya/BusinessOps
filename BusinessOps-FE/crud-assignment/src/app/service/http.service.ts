import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Company, Department, Employee, UpsertCompany } from '../../models/company.model';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  apiUrl = 'https://localhost:7113/api/';
  public isLoading = false;

  constructor(private http: HttpClient) { }

  getCompany(): Observable<Company[]> {
    return this.http.get<Company[]>(this.apiUrl + 'Company/GetAllCompanies');
  }

  getCompanyById(id: number): Observable<Company> {
    return this.http.get<Company>(this.apiUrl + `Company/GetCompanyById?companyId=${id}`);
  }

  upsertCompany(req: UpsertCompany): Observable<Company> {
    return this.http.post<Company>((this.apiUrl + 'Company/UpsertCompany'), req);
  }

  deleteCompany(companyId: number): Observable<boolean> {
    return this.http.post<boolean>(this.apiUrl + `Company/DeleteCompany?id=${companyId}`, null);
  }

  getDepartments(): Observable<Department[]> {
    return this.http.get<Department[]>(this.apiUrl + 'Department/GetAllDepartments');
  }

  getDepartmentById(id: number): Observable<Department> {
    return this.http.get<Department>(this.apiUrl + `Department/GetDepartmentById?departmentId=${id}`);
  }

  upsertDepartment(req: Department): Observable<Department> {
    return this.http.post<Department>((this.apiUrl + 'Department/UpsertDepartment'), req);
  }

  deleteDepartment(departmentId: number): Observable<boolean> {
    return this.http.post<boolean>(this.apiUrl + `Department/DeleteDepartment?id=${departmentId}`, null);
  }

  getEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>(this.apiUrl + 'Employee/GetAllEmployees');
  }

  getEmployeesById(id: number): Observable<Employee> {
    return this.http.get<Employee>(this.apiUrl + `Employee/GetEmployeeById?employeeId=${id}`);
  }

  upsertEmployee(req: Employee): Observable<Employee> {
    return this.http.post<Employee>((this.apiUrl + 'Employee/UpsertEmployee'), req);
  }

  deleteEmployee(employeeId: number): Observable<boolean> {
    return this.http.post<boolean>((this.apiUrl + `Employee/DeleteEmployee?id=${employeeId}`), null);
  }

  getDepartmentByCompanyId(id: number): Observable<Department[]> {
    return this.http.get<Department[]>(this.apiUrl + `Department/GetDepartmentByCompanyId?companyId=${id}`);
  }

}
