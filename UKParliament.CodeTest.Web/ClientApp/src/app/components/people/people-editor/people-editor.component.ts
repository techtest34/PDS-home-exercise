import { Component, inject, input, output, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule, NgFor } from '@angular/common';
import { PersonViewModel } from '../../../models/person-view-model';
import { DepartmentViewModel } from '../../../models/department-view-model';
import { PersonService } from '../../../services/person.service';
import { DepartmentService } from '../../../services/department.service';

@Component({
  selector: 'people-editor',
  templateUrl: './people-editor.component.html',
  standalone: true,
  imports: [ReactiveFormsModule, NgFor, CommonModule]
})

export class PeopleEditorComponent implements OnInit {
  person = input<PersonViewModel | null>(null);
  formActioned = output<void>();

  departments: DepartmentViewModel[] = [];

  peopleForm = inject(FormBuilder).group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    dateOfBirth: [<Date | null>null, Validators.required],
    departmentId: [0, Validators.min(1)]
  });

  constructor(private personService: PersonService, private departmentService: DepartmentService) { }

  ngOnInit(): void {
    this.getDepartments();
  }

  ngOnChanges(): void {
    if (this.person() !== null) {
      this.peopleForm.patchValue(this.person() as PersonViewModel)
    }
  }

  onSubmit(): void {
    if (this.peopleForm.invalid) {
      this.peopleForm.markAllAsTouched();
      return;
    }

    const body = this.peopleForm.value as PersonViewModel
    const personId = this.person()?.id

    if (personId) {
      body.id = personId
      this.personService.update(personId, body).subscribe(() => this.clear())
    } else {
      this.personService.create(body).subscribe(() => this.clear())
    }
  }

  getDepartments(): void {
    this.departmentService
      .getDepartments()
      .subscribe(departments => this.departments = departments)
  }

  clear(): void {
    this.formActioned.emit()
    this.peopleForm.reset({ departmentId: 0 });
  }

  displayError(field: string): boolean {
    const formField = this.peopleForm.get(field)
    return (formField?.invalid && (formField?.dirty || formField?.touched)) === true
  }
}
