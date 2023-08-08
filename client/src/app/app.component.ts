import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'The Daiting App';

  users: any;
  constructor(private http: HttpClient, private accountService:AccountService) {}

  ngOnInit() {
    this.getUsers();
    this.setCurrentUser();
  }

  setCurrentUser() {
      const userStr: string | null = localStorage.getItem('user');
      if (!userStr) return;
        const user: User = JSON.parse(userStr);
        this.accountService.setCurrentUser(user);
  }
  
  getUsers() {
    this.http.get('https://localhost:7221/api/users').subscribe(
      (response) => {
        this.users = response;
      },
      (error) => {
        console.log(error);
      });
    }
}
