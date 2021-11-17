import { Component, EventEmitter, OnInit, Output, ViewEncapsulation } from '@angular/core';
import { AuthService } from 'src/app/resources/auth.service';

@Component({
  selector: 'principal-sidenav-list',
  templateUrl: './principal-sidenav-list.component.html',
  styleUrls: ['./principal-sidenav-list.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class PrincipalSidenavListComponent implements OnInit {
  @Output() public sidenavClose = new EventEmitter();
  constructor(public auth: AuthService) { }
  
  ngOnInit(): void {}

   onSidenavClose(){
    setTimeout(() => {
      this.sidenavClose.emit();
    }, 180);
  }
}
