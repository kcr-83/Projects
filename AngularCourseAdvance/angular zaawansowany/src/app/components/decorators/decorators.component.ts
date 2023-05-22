import { AfterViewInit, Component, ElementRef, HostBinding, HostListener, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { MessageComponent } from 'src/app/app-components/message/message.component';

@Component({
  selector: 'app-decorators',
  templateUrl: './decorators.component.html',
  styleUrls: ['./decorators.component.scss']
})
export class DecoratorsComponent implements AfterViewInit {

  @HostBinding('style.backgroundColor') backgroundColor!: string
  @HostBinding('class.valid') isValid = true
  @HostBinding('attr.tooltip') tooltip = 'Domyślny tooltip'

  @HostListener('window:click', ['$event']) windowClick(ev: MouseEvent) {
    console.log('Window click from decoratorsComponent!')
  }
  @HostListener('mousemove', ['$event']) hostMouseMove(ev: MouseEvent) {
    const bias = 150 + Math.min(ev.clientX / 10, 100)
    this.backgroundColor = `rgb(${bias},${bias},${bias})`
  }

  // dostępne dopiero w afterViewInit
  // popularne selektory : #name, dyrektywa, komponent, TemplateRef
  // read: wybór tokena do dostarczenia: ElementRef, ViewContainerRef, 
  @ViewChild('sentenceBox') sentenceBox: MessageComponent | undefined
  @ViewChild('sentenceBox', { read: ElementRef }) sentenceBoxER: ElementRef<MessageComponent> | undefined

  @ViewChildren(MessageComponent) allSentenceBoxes: QueryList<MessageComponent> | undefined

  // dostępne po afterContentInit!
  //@ContentChild/@ContentChildren analogicznie do elementów z content - project

  constructor() {}


  ngAfterViewInit() {
    // PEŁNY dostęp do komponentu dziecka
    console.log("Komponent", this.sentenceBox)
    // dostęp do nativeElement sentenceBox
    console.log("ElementRef.nativeElement", this.sentenceBoxER?.nativeElement)
    // wszystkie MessageComponent w widoku
    console.log(this.allSentenceBoxes)
  }

}
