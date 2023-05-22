import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { SwApiPerson } from 'src/app/models/sw-api-person.model';
import { SwApiService } from 'src/app/services/sw-api/sw-api.service';

@Component({
  selector: 'app-standalone',
  templateUrl: './standalone.component.html',
  styleUrls: ['./standalone.component.scss'],
  // tu definiujemy standalone
  standalone: true,
  // jak w module:
  providers: [],
  imports: [CommonModule]
})
export class StandaloneComponent implements OnInit {

  protected luke$!: Observable<SwApiPerson>
  constructor(private readonly swApi: SwApiService) {}

  ngOnInit(): void {
    this.luke$ = this.swApi.getPerson(1)
  }

}
