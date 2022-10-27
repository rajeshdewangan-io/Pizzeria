import { Pipe, PipeTransform } from '@angular/core';
import { Pizzas } from 'src/app/pizzas';

@Pipe({
  name: 'toppings'
})
export class ToppingsPipe implements PipeTransform {
  /*
    custom pipe to get list of toppings in String format
  */
  transform(pizzaId: number, pizzasList: Pizzas[]): string {
    return pizzasList
      .filter(pizza => pizza.id === pizzaId)
      .map(topping => topping.toppings)[0]
      .map(topName => topName.name)
      .reduce(
        (topList: string = "Toppings are kept secret", topName) =>
          topList + topName + ",",
        "Toppings : "
      );
  }
}
