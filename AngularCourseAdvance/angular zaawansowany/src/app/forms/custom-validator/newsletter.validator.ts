import { AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn } from "@angular/forms";
import { ContactForm } from "./custom-validator.component";

// control jest naszą FG/FR/FA
export const newsletterValidator: ValidatorFn =
  (control: AbstractControl<ContactForm>): ValidationErrors | null => {
    // const isValidNewsletter = control.get<keyof NewsletterForm>('remail')?.value && control.get('rodo')?.value
    const email = (control as FormGroup).get('email')
    const rodo = (control as FormGroup).get('rodo')
    const phone = (control as FormGroup).get('phone')

    const isEmailValid = /.+\@.+/.test(email?.value)
    const isRodoValid = rodo?.value
    const isPhoneValid = phone?.value

    return (isEmailValid && isRodoValid) || isPhoneValid
      ? null
      : { contact: { value: 'podaj telefon lub email i rodo' } }
  }
