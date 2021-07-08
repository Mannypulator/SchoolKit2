import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'principal-sidenav-list',
  templateUrl: './principal-sidenav-list.component.html',
  styleUrls: ['./principal-sidenav-list.component.css']
})
export class PrincipalSidenavListComponent implements OnInit {
  @Output() public sidenavClose = new EventEmitter();
  constructor() { }
  
  ngOnInit(): void {}

   onSidenavClose(){
    setTimeout(() => {
      this.sidenavClose.emit();
    }, 180);
  }
}
