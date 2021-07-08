import { Component, OnInit } from '@angular/core';
import { TitleService } from 'src/app/shared/services/title.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  links : string[] = ["English", "Mathematics"];
  constructor(private titleService: TitleService) { }

  ngOnInit(): void {
    this.titleService.setTitle("Profile");
  }

}
