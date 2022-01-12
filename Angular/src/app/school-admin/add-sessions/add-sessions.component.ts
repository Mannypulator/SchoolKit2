import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-add-sessions',
  templateUrl: './add-sessions.component.html',
  styleUrls: ['./add-sessions.component.css'],
  
})


export class AddSessionsComponent implements OnInit {
  

  constructor(public dialogRef: MatDialogRef<AddSessionsComponent>) { }

  sessionTitle! : string;

  ngOnInit(): void {
    
  }

  dialogClose(){
    this.dialogRef.close(this.sessionTitle);
  }



}
