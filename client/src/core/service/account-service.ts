import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { tap } from 'rxjs';
import { LoginCreds, RegisterCreds, User } from '../../types/user';

@Injectable({
  providedIn: 'root',
})
export class AccountService {

  private http = inject(HttpClient);
  currentUser = signal<User | null>(null);

  baseurl ="https://localhost:7188/api/"

  register(creds: RegisterCreds)
  {
    return this.http.post<User>(this.baseurl + 'account/register', creds).pipe(tap(user => {
      if (user) { 
         //use helper method here
       this.setCurrentUser(user)
      }
    })
  )
  }
    
  

  login(creds :LoginCreds)
  {
    return this.http.post<User>(this.baseurl + 'account/login', creds).pipe(tap(user => {
      if (user) { 
        //use helper method here
       this.setCurrentUser(user)}
    } ))
      
  }


  //helper method
  setCurrentUser(user: User)
  {
    localStorage.setItem('user', JSON.stringify(user));
        this.currentUser.set(user);
  }

  logout()
  {
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }
  
}
