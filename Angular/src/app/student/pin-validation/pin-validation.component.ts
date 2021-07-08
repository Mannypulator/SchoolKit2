import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-pin-validation',
  templateUrl: './pin-validation.component.html',
  styleUrls: ['./pin-validation.component.css']
})
export class PinValidationComponent implements OnInit {

  classes: string[]=["jss1", "jss2"];
terms: string[] = ["1st term", "2nd term"];
sessions: string[]= ["1st session", "2nd session"];
  constructor() { }

  ngOnInit(): void {
  }

}
