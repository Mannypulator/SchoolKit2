import { Component, EventEmitter, OnDestroy, OnInit, Output } from '@angular/core';
import { AuthService } from 'src/app/resources/auth.service';


import { TitleService } from '../../services/title.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

@Output() public sidenavToggle = new EventEmitter();

 title: string = ''; 
  constructor(public titleService: TitleService, public authService: AuthService) { }

  ngOnInit(): void {
    this.titleService.getTitle().subscribe(t=> this.title = t)
  }

 
  onToggleSidenav(){
    this.sidenavToggle.emit();
  }
}
