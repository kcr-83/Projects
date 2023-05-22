import { ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { sentences } from './sentences';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.scss'],
})
export class MessageComponent implements OnInit {
  @Input() title: string = ''
  @Input() sentencesCount: number | undefined
  @Output() action = new EventEmitter<string>();

  message: string = ''
  list: any[] | undefined
  showButtons = false

  protected sentences: string[] | undefined
  constructor(private readonly activatedRoute: ActivatedRoute, private readonly cd: ChangeDetectorRef) {}

  ngOnInit(): void {
    if (this.activatedRoute.snapshot.data['title']) {
      this.title = this.activatedRoute.snapshot.data['title']
    }
    if (this.activatedRoute.snapshot.data['message']) {
      this.message = this.activatedRoute.snapshot.data['message']
    }

    if (this.activatedRoute.snapshot.data['list']) {
      this.list = this.activatedRoute.snapshot.data['list']
    }
    if (this.activatedRoute.snapshot.data['list']) {
      this.showButtons = !!this.activatedRoute.snapshot.data['showButtons']
    }
    if (this.activatedRoute.snapshot.data['sentencesCount']) {
      this.sentencesCount = this.activatedRoute.snapshot.data['sentencesCount']
    }
    if (this.sentencesCount) {
      this.sentences = this.getRandomSentences(this.sentencesCount)
    }
  }
  getRandomSentences(count: number) {
    const sentencesArray: string[] = []
    for (let i = 0; i < count; i++) {
      const index = Math.floor(Math.random() * sentences.length)
      sentencesArray.push(sentences[index])
    }
    return sentencesArray;
  }
  onAction(action: string) {
    this.action.emit(action)
  }
}
