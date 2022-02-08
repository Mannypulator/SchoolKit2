import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'result-list',
  templateUrl: './result-list.component.html',
  styleUrls: ['./result-list.component.css']
})
export class ResultListComponent implements OnInit {

  constructor() { }

  @Input() students: any =[];
  ngOnInit(): void {
  }

}
