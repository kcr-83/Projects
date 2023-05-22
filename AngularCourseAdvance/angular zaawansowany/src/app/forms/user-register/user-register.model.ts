import { FormControl } from "@angular/forms";

export type UserRegister = {
  name: FormControl<string>;
  lastname: FormControl<string>;
  email: FormControl<string>;
  age: FormControl<number | null>;
  password: FormControl<string>;
}