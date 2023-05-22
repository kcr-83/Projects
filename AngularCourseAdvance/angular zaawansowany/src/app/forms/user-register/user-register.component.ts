import { Component, OnDestroy, OnInit } from '@angular/core';
import { AbstractControl, ControlValueAccessor, FormBuilder, FormControl, FormGroup, NG_VALIDATORS, NG_VALUE_ACCESSOR, ValidationErrors, Validator, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { UserRegister } from './user-register.model';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: UserRegisterComponent
    },
    {
      provide: NG_VALIDATORS,
      multi: true,
      useExisting: UserRegisterComponent
    }
  ]
})
export class UserRegisterComponent implements OnInit, OnDestroy, ControlValueAccessor, Validator {

  protected userForm!: FormGroup<UserRegister>

  protected onChange!: <T>(userData: T) => void
  protected onTouched!: () => void

  valueSub!: Subscription

  constructor(private readonly fb: FormBuilder) {}
  validate(control: AbstractControl<any, any>): ValidationErrors | null {
    // a cooo...
    return null
  }
  ngOnInit(): void {
    this.userForm = this.fb.group<UserRegister>({
      name: new FormControl('', { validators: [Validators.required], nonNullable: true }),
      lastname: new FormControl('', { validators: [Validators.required], nonNullable: true }),
      password: new FormControl('', { validators: [Validators.required], nonNullable: true }),
      email: new FormControl('', { validators: [Validators.required], nonNullable: true }),
      age: new FormControl()
    })
    this.valueSub = this.userForm.valueChanges.subscribe(val => this.onChange(val))
  }

  // z ControlValueAccessor
  writeValue(userRegisterData: any): void {
    this.userForm.setValue(userRegisterData)
  }

  // z ControlValueAccessor
  // powiadomienie wyżej o zmianie wartości w custom form control
  registerOnChange(onChange: any): void {
    this.onChange = onChange
  }

  // z ControlValueAccessor
  // powiadomienie wyżej o touched na custom form control
  registerOnTouched(onTouched: any): void {
    this.onTouched = onTouched
  }

  markAsTouched() {
    if (this.userForm.untouched) {
      this.onTouched()
    }
  }

  // z ControlValueAccessor
  setDisabledState?(isDisabled: boolean) {
    isDisabled ? this.userForm.disable() : this.userForm.enable()
  }

  ngOnDestroy() {
    this.valueSub.unsubscribe();
  }

}
