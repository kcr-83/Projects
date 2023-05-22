import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export const isValidSSLUrlValidator: ValidatorFn =
  (control: AbstractControl): ValidationErrors | null => {
    const isValidUrl = control.value.startsWith('https');
    return !isValidUrl ? { isValidSSLUrl: { value: control.value } } : null;
  }