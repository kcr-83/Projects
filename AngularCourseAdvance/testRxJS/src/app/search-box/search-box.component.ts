import { AfterViewInit, Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { debounceTime, scan, filter, fromEvent, map, Subscription, tap } from 'rxjs';

@Component({
  selector: 'app-search-box',
  templateUrl: './search-box.component.html',
  styleUrls: ['./search-box.component.css']
})
export class SearchBoxComponent implements OnInit, AfterViewInit, OnDestroy {

  sub = new Subscription();
  searchHistory: string[] = [];
  
  @ViewChild('searchBox') searchBox: ElementRef<HTMLInputElement> | undefined;
  constructor() { }
  ngOnDestroy(): void {
    throw new Error('Method not implemented.');
  }

  ngAfterViewInit(): void {
    if (!this.searchBox) return;
    this.sub = fromEvent<InputEvent>(this.searchBox.nativeElement, 'input')
      .pipe(
        debounceTime(300),
        map(ev => `${(ev.target as HTMLInputElement).value}`),
        filter(val => val !==''),
        // scan(
        //   (acc: SearchData, val: string)=>
        //   {
        //     acc.history.push(val)
        //     acc.current = val
        //     return acc
        //   },
        //   {
        //     history: [],  current: ''
        //   }
        // ),
        tap(str => this.searchHistory.push(str))
      )
      .subscribe(
        str =>{
            console.log(str);
            console.log('Histosia wyszukan: ' + this.searchHistory.join(', '));
        }
      )
  }

  ngOnInit(): void {
    this.sub.unsubscribe();

  }

}
