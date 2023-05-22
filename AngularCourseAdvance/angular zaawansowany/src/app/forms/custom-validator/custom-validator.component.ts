import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { isValidSSLUrlValidator } from './is-valid-ssl-url.validator';
import { newsletterValidator } from './newsletter.validator';

export type ContactForm = {
  email: FormControl<string>,
  rodo: FormControl<boolean>,
  phone: FormControl<string>
}

@Component({
  selector: 'app-custom-validator',
  templateUrl: './custom-validator.component.html',
  styleUrls: ['./custom-validator.component.scss']
})
export class CustomValidatorComponent implements OnInit {

  url!: FormControl<string | null>
  // typowany FG
  contactForm!: FormGroup<ContactForm>
  constructor() {}

  ngOnInit(): void {
    // customowy walidator
    this.url = new FormControl('', [Validators.required, isValidSSLUrlValidator])
    // nie przejdzie już w ts-ie - typ
    // this.url.setValue(123)
    // nie zwaliduje na runtime - walidator
    // this.url.setValue('http://onet.pl')

    // walidatory pojedynczych pól
    this.contactForm = new FormGroup<ContactForm>({
      email: new FormControl('', { validators: [Validators.required, Validators.pattern('.+\@.+')], nonNullable: true }),
      rodo: new FormControl(false, { validators: [Validators.requiredTrue], nonNullable: true }),
      phone: new FormControl()
    })
    // lub walidator bazujący na wielu polach
    // this.contactForm = new FormGroup<ContactForm>({
    //   email: new FormControl(),
    //   rodo: new FormControl(false, { nonNullable: true }),
    //   phone: new FormControl()
    // },
    //   { validators: newsletterValidator })

    // pobranie wartości - snapshot
    // const formValue = this.newsletterForm.value
    // const formRowValue = this.newsletterForm.getRawValue()
  }

}
