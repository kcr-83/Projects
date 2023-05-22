import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Data } from '@angular/router';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-refresh-view',
  templateUrl: './refresh-view.component.html',
  styleUrls: ['./refresh-view.component.scss']
})
export class RefreshViewComponent implements OnInit, OnDestroy {

  protected data$: Observable<Data> | undefined
  constructor(private readonly activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
    this.data$ = this.activatedRoute.data
  }

  ngOnDestroy() {
    console.log('RefreshViewComponent Destroy')
  }
}
