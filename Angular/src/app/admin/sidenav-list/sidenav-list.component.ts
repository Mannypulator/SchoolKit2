import { Component, EventEmitter, OnInit, Output, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatAccordion, MatExpansionPanel } from '@angular/material/expansion';

@Component({
  selector: 'app-sidenav-list',
  templateUrl: './sidenav-list.component.html',
  styleUrls: ['./sidenav-list.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class SidenavListComponent implements OnInit {
  @ViewChild('expansionPanel') public Panel! : MatExpansionPanel
  @Output() public sidenavClose = new EventEmitter();
  constructor() { }
  
  ngOnInit(): void {
    
  }

   onSidenavClose(){
    this.Panel.close();
    setTimeout(() => {
      this.sidenavClose.emit();
    }, 180);
    
  }

  

}
