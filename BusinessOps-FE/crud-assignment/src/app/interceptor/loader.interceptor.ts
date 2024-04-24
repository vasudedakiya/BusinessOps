import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, catchError, finalize, map, tap } from 'rxjs';
import { HttpService } from '../service/http.service';

@Injectable()
export class LoaderInterceptor implements HttpInterceptor {

  constructor(private _httpService: HttpService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    this._httpService.isLoading = true;

    return next.handle(request).pipe(
      finalize(() => {
        this._httpService.isLoading = false;
      })
    );
  }
}
