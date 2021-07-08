import { Injectable } from '@angular/core';
import { NgProgressRef } from '@ngx-progressbar/core';

@Injectable({
  providedIn: 'root',
})
export class ProgressbarService {
  progressRef!: NgProgressRef;
  defaultColor: string = '#007bff';
  successColor: string = '#13b955';
  failureColor: string = '#fc3939';
  currentColor: string = this.defaultColor;

  title: string = "header";
  constructor() {}

  startLoading() {
    this.currentColor = this.defaultColor;
    this.progressRef.start();
  }

  completeLoading() {
    this.progressRef.complete();
  }

  setSuccess() {
    this.currentColor = this.successColor;
  }

  setFailure() {
    this.currentColor = this.failureColor;
  }

  setTitle(title: string){
    this.title = title;
  }
}
