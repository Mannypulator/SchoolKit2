import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'space'
})
export class SpacePipe implements PipeTransform {

  transform(value: string): string {
    var re = /_/gi;
    var newStr = value.replace(re, " ");
    return newStr;
  }

}
