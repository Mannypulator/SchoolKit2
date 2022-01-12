import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'student-sidenav',
  templateUrl: './student-sidenav.component.html',
  styleUrls: ['./student-sidenav.component.css']
})
export class StudentSidenavComponent implements OnInit {
  @Output() public sidenavClose = new EventEmitter();

  
  constructor() { }
  
  ngOnInit(): void {}

   onSidenavClose(){
    setTimeout(() => {
      this.sidenavClose.emit();
    }, 180);
  }
}