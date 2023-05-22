import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Data, Params } from '@angular/router';

@Component({
  selector: 'app-routing-params',
  templateUrl: './routing-params.component.html',
  styleUrls: ['./routing-params.component.scss']
})
export class RoutingParamsComponent implements OnInit {

  routeParams: Params | undefined
  parentRouteParams: Params | undefined
  queryParams: Params | undefined
  data: Data | undefined
  fragment = ''

  constructor(private readonly activatedRoute: ActivatedRoute) {
    console.log(activatedRoute)
  }

  ngOnInit(): void {
    const snapshot = this.activatedRoute.snapshot
    this.routeParams = snapshot.params
    this.parentRouteParams = snapshot.parent?.params //itd
    this.queryParams = snapshot.queryParams
    this.fragment = snapshot.fragment || ''
    this.data = snapshot.data
    console.log(this.data)
    // jeśli notorycznie potrzebujesz parametrów rodzica:
    // Router ExtraOptions: { paramsInheritanceStrategy: 'always' }
  }

}
