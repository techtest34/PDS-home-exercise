import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PersonViewModel } from '../models/person-view-model';

@Injectable({
  providedIn: 'root'
})
export class PersonService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getById(id: number): Observable<PersonViewModel> {
    return this.http.get<PersonViewModel>(this.baseUrl + `api/people/${id}`)
  }

  getAll(): Observable<PersonViewModel[]> {
    return this.http.get<PersonViewModel[]>(this.baseUrl + `api/people/`)
  }

  savePerson(body: PersonViewModel, personId?: number): Observable<Object> {
    if (personId) {
      body.id = personId
      return this.http.put(this.baseUrl + `api/people/${personId}`, body)
    }

    return this.http.post(this.baseUrl + `api/people`, body);
  }
}
