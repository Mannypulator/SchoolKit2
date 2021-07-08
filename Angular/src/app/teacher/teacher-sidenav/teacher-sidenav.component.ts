import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'teacher-sidenav',
  templateUrl: './teacher-sidenav.component.html',
  styleUrls: ['./teacher-sidenav.component.css']
})
export class TeacherSidenavComponent implements OnInit {
  @Output() public sidenavClose = new EventEmitter();

  
  constructor() { }
  
  ngOnInit(): void {}

   onSidenavClose(){
    setTimeout(() => {
      this.sidenavClose.emit();
    }, 180);
  }
}
