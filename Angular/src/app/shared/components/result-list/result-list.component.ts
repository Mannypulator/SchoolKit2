import { Component, Input, OnInit } from '@angular/core';
import { AuthService } from 'src/app/resources/auth.service';

@Component({
  selector: 'result-list',
  templateUrl: './result-list.component.html',
  styleUrls: ['./result-list.component.css']
})
export class ResultListComponent implements OnInit {

  constructor(public auth: AuthService) { }

  @Input() students: any =[];
  ngOnInit(): void {
  }

}
