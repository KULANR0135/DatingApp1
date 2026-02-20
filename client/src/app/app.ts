import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { lastValueFrom } from 'rxjs';


@Component({
  selector: 'app-root',
  imports: [],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit{
  private Http = inject(HttpClient);
  protected readonly title = 'Dating app';
  protected members = signal<any>([]);

  async ngOnInit() {
    this.members.set(await this.getMembers())
  }

 async getMembers()
 {
  try{

    return lastValueFrom(this.Http.get('https://localhost:7188/api/members'));
  }catch(error)
  {
    console.log(error);
    throw error;
  }
}
    
  
}


