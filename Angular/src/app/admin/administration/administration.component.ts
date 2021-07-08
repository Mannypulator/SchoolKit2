import { Component, OnInit } from '@angular/core';
import { MediaChange, MediaObserver } from '@angular/flex-layout';
import { Subscription } from 'rxjs';
import { TitleService } from 'src/app/shared/services/title.service';

@Component({
  selector: 'app-administration',
  templateUrl: './administration.component.html',
  styleUrls: ['./administration.component.css']
})
export class AdministrationComponent implements OnInit {
  mediaSub!: Subscription;
  openState!: boolean;
  drawerMode!: string;
  alias!: string;

  constructor(private titleService: TitleService, private mediaObserver: MediaObserver) { }

  ngOnInit(): void {
    this.titleService.setTitle('Administration');
    this.mediaSub = this.mediaObserver.media$.subscribe((result:MediaChange)=> {
      
      this.alias = result.mqAlias;
      
    })
  }


}
