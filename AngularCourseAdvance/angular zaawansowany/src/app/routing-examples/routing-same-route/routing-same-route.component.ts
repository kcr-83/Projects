import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { SwApiPeople } from 'src/app/models/sw-api-people.model';

@Component({
  selector: 'app-routing-same-route',
  templateUrl: './routing-same-route.component.html',
  styleUrls: ['./routing-same-route.component.scss']
})
export class RoutingSameRouteComponent implements OnInit {

  params: Params = {}
  params$!: Observable<Params>
  people: SwApiPeople | undefined
  constructor(private readonly activatedRoute: ActivatedRoute, private readonly router: Router, private readonly location: Location) {}

  ngOnInit(): void {
    console.log('RoutingSameRoute.ngOnInit loaded')
    this.params = this.activatedRoute.snapshot.params
    // this.params$ = this.activatedRoute.params
    console.log(this.params)
  }
  // nawigacja za pomocą Router.navigate
  protected navigateTo(route: any[]) {
    this.router.navigate(route)
  }
  // nawigacja za pomocą Location
  protected goBack() {
    // uwaga na import - Angularowy Location jest mylony z window.location
    this.location.back();
  }
}
