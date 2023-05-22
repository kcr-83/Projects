import { SwApiPerson } from "./sw-api-person.model"

export type SwApiPeople = {
  count: number,
  next: string | null,
  previous: string | null,
  results: SwApiPerson[]
}