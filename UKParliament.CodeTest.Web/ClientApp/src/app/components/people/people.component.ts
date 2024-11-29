import { Component, OnInit } from '@angular/core';
import { PersonViewModel } from '../../models/person-view-model';
import { PersonService } from '../../services/person.service';
import { PeopleEditorComponent } from './people-editor/people-editor.component';
import { NgFor, CommonModule } from '@angular/common';

@Component({
  templateUrl: './people.component.html',
  standalone: true,
  imports: [PeopleEditorComponent, NgFor, CommonModule]
})
export class PeopleComponent implements OnInit {
  people: PersonViewModel[] = [];
  selectedPerson: PersonViewModel | null = null;

  constructor(private personService: PersonService) { }

  ngOnInit(): void {
    this.getPeople();
  }

  getPeople(): void {
    this.personService
      .getAll()
      .subscribe(people => this.people = people)
  }

  reset(): void {
    this.selectedPerson = null
    this.getPeople()
  }

  personSelected(person: PersonViewModel): void {
    this.selectedPerson = person
  }
}
