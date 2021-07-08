import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'administration-sidenav',
  templateUrl: './administration-sidenav.component.html',
  styleUrls: ['./administration-sidenav.component.css']
})
export class AdministrationSidenavComponent implements OnInit {
  @Output() public sidenavClose = new EventEmitter();
  constructor() { }

  ngOnInit(): void {
  }

  onSidenavClose():void{
    this.sidenavClose.emit();
  }
}
