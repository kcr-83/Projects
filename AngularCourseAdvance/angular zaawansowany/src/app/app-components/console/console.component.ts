import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-console',
  templateUrl: './console.component.html',
  styleUrls: ['./console.component.scss']
})
export class ConsoleComponent implements OnInit {
  protected logs: any[] = []

  constructor() {}

  ngOnInit(): void {
    this.wrapConsoleLog()
  }

  private wrapConsoleLog() {
    const consoleLog = console.log; // eslint-disable-line no-console
    // eslint-disable-next-line no-console
    console.log = (...args) => {
      consoleLog(...args) // eslint-disable-line no-console
      this.logs = [...this.logs, args]
    }
    // inny sposób:
    // eslint-disable no-console, no-alert
    // console.log(123)
    // alert(123)
    // eslint-enable
  }
}
