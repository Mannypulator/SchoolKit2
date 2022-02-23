import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'enumToArray'
})
export class EnumToArrayPipe implements PipeTransform {

  transform(data: Object) {
    const values = Object.values(data);
    return values.slice(values.length / 2);
  }
}
