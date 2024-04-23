export interface Company {
    id: number;
    companyName: string;
    departments?: Department[]
}

export interface Department {
    id: number;
    departmentName: string;
}

export interface UpsertCompany {
    id: number;
    companyName: string;
    departmentIds: number[];
}

export interface Employee {
    id: number;
    employeeName: string;
    companyId: number;
    companyName: string;
    departmentId: number;
    departmentName: string
}