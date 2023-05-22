import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-custom-form-control',
  templateUrl: './custom-form-control.component.html',
  styleUrls: ['./custom-form-control.component.scss']
})
export class CustomFormControlComponent implements OnInit {

  newUserRegister!: FormGroup
  constructor() {}

  ngOnInit(): void {
    this.newUserRegister = new FormGroup({
      date: new FormControl(),
      user: new FormControl()
    })
    this.newUserRegister.valueChanges.subscribe(console.log)
  }

  toggleForm() {
    this.newUserRegister.disabled
      ? this.newUserRegister.enable()
      : this.newUserRegister.disable()
  }
}
