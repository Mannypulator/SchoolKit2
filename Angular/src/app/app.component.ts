import { Component } from '@angular/core';
import { NgProgress } from '@ngx-progressbar/core';
import { ProgressbarService } from './shared/services/progressbar.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent { 
  
  constructor(private progressService: ProgressbarService,private progress:NgProgress){}
  title = 'School Kit';
  ngOnInit(): void {
    this.progressService.progressRef = this.progress.ref('progressBar');
    this.progressService.setTitle("login");
  }
}
