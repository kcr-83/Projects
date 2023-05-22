import { Component, OnInit } from '@angular/core';
import { interval, Subject, timer } from 'rxjs';

@Component({
  selector: 'app-rxjs',
  templateUrl: './rxjs-dashboard.component.html',
  styleUrls: ['./rxjs-dashboard.component.scss']
})
export class RxjsDashboardComponent implements OnInit {
  constructor() {}

  someSubject = new Subject<string>();
  ngOnInit(): void {
    this.someSubject.next('pierwszy emit z inputa')
    timer(2000).subscribe(() => this.someSubject.next('drugi emit z inputa'))
  }
}
