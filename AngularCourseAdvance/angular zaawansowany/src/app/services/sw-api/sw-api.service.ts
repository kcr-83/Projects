import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SwApiPeople } from 'src/app/models/sw-api-people.model';
import { catchError, of, retry } from 'rxjs'
import { SwApiPerson } from 'src/app/models/sw-api-person.model';
@Injectable({
  providedIn: 'root'
})
export class SwApiService {
  apiUrl = 'https://swapi.dev/api/'

  constructor(private readonly http: HttpClient) {}

  getPeople() {
    //  options = {
    //       headers ?: HttpHeaders | { [header: string]: string | string[]
    //     },
    //     observe ?: 'body' | 'events' | 'response',
    //       params ?: HttpParams | { [param: string]: string | number | boolean | ReadonlyArray<string | number | boolean>
    //   },
    //   reportProgress?: boolean,
    //   responseType?: 'arraybuffer' | 'blob' | 'json' | 'text',
    //   withCredentials?: boolean,
    // }

    const url = this.apiUrl + 'people'
    return this.http.get<SwApiPeople>(url)
    // .pipe(
    //   retry(1),
    //   catchError((err) => {
    //     console.log(err)
    //     return of('Ups !@#$')
    //   }));
  }
  updatePerson(id: number, data: SwApiPerson) {
    const url = this.apiUrl + 'people/' + id
    return this.http.post<SwApiPerson>(url, data)
  }
  getPerson(id: number) {
    const url = this.apiUrl + 'people/' + id
    return this.http.get<SwApiPerson>(url)
  }
  getPLanets() {
    const url = this.apiUrl + 'planets'
    return this.http.get(url)
  }
  getPlanet(id: number) {
    const url = this.apiUrl + 'planets/' + id
    return this.http.get(url)
  }
}