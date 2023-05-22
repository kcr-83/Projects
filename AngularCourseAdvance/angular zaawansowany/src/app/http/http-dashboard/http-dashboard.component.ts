import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/services/api/api.service';
import { UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-http-dashboard',
  templateUrl: './http-dashboard.component.html',
  styleUrls: ['./http-dashboard.component.scss']
})
export class HttpDashboardComponent {

  constructor(private readonly user: UserService, private readonly api: ApiService) {}

  getToken() {
    this.user.loadToken(10).subscribe(token => console.log('New token: ' + token))
  }
  deleteToken() {
    this.user.deleteToken()
  }
  getProtected(number: 1 | 2 | 3) {
    this.api.getProtected(number).subscribe(data => console.log(`Protected ${number} data: ${data}`))
  }
}
