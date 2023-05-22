import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-form-basic',
  templateUrl: './form-basic.component.html',
  styleUrls: ['./form-basic.component.scss']
})
export class FormBasicComponent implements OnInit, OnDestroy {
  loginForm!: FormGroup
  formSub!: Subscription;

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      login: ['', Validators.minLength(5)],
      password: ['', Validators.required],
      passwordRepeat: ['', Validators.required],
      test : ['']
    })

    console.log(this.loginForm)
    this.formSub = this.loginForm
      .valueChanges
      .subscribe(data => {
        console.log(data)
      })

    // pobranie migawki formularza
    // wszystkie włączone kontrolki
    // this.loginForm.value
    // wszystkie pola (również nieaktywne)
    // this.loginForm.getRawValue()
    this.loginForm.get('login')?.valueChanges
      .subscribe(data => console.log("Login change:", data))
  }

  onSubmit() {
    console.log('Form submit', this.loginForm.value)
    // if (this.loginForm.valid) {
    // save action
    // this.loginForm.markAsPristine();
    // this.loginForm.markAsUntouched();
    // }
  }

  ngOnDestroy(): void {
    this.formSub.unsubscribe();
  }

}
