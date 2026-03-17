import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../core/service/account-service';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';


@Component({
  selector: 'app-nav',
  imports: [FormsModule, RouterLink, RouterLinkActive],
  templateUrl: './nav.html',
  styleUrl: './nav.css',
})
export class Nav {

  protected accountService = inject(AccountService)
  private router = inject(Router);

 protected creds: any  ={};


login() {
  this.accountService.login(this.creds).subscribe({
  next: (result:any) => { 
    this.router.navigateByUrl('/members');
    console.log(result); 
 
  this.creds={};},
  error: (error:any) => alert(error.message) })
  
 }

 logout()
 {
  this.accountService.logout();
      this.router.navigateByUrl('/');
 }

 }
