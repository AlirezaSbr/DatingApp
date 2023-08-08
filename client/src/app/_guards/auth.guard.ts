import { ActivatedRouteSnapshot, CanActivate, CanActivateFn, RouterStateSnapshot, UrlTree } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
  })

  export class AuthGuard implements CanActivate {

    constructor(private accountService: AccountService, private roastr: ToastrService){}

  canActivate(): Observable<boolean> {
    return this.accountService.currentUser$.pipe(map(
      (user)=> {
        if (user) return true;
        else {
          this.roastr.error("You shall not Pass!!");
          return false;}
      }
    ))
  }
}