import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-routing-dashboard',
  templateUrl: './routing-dashboard.component.html',
  styleUrls: ['./routing-dashboard.component.scss']
})
export class RoutingDashboardComponent implements OnInit {

  constructor(private readonly route: ActivatedRoute) {}

  ngOnInit(): void {
    // console.log(this.route)
  }

}
