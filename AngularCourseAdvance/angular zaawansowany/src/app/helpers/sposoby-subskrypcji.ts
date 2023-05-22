import { Observable, Observer } from "rxjs";

// callbacki na zdarzenia observabla
const subNext = (data: string) => console.log('Nowy obcy na pokładzie: ' + data)
const subErr = (err: Error) => console.error('Houston, mamy problem: ' + err)
const subComplete = () => console.log('Obcy powróci!')

// observable
const nostromo = new Observable<string>()

// observer
// obserwator jest Partialem, nie musi zawierać wszystkich właściwości 
const crew: Partial<Observer<string>> = {
  next: subNext,
  error: subErr,
  // complete: subComplete
};

// subskrypcja - pomost observable<->obserwator
const action = nostromo.subscribe(crew)
// lub 
const action2 = nostromo.subscribe(subNext)
// lub (deprecated w RxJS 7) 
const action3 = nostromo.subscribe(subNext, subErr, subComplete)


// po skorzystaniu - unsubscribe
action.unsubscribe();
action2.unsubscribe();
action3.unsubscribe();