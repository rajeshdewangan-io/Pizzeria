import { Pipe, PipeTransform } from "@angular/core";
import { Pizzas } from "src/app/pizzas";

@Pipe({
  name: "ingredients"
})
export class IngredientsPipe implements PipeTransform {
  /*
    custom pipe to get list of ingredients in String format
  */
  transform(pizzaId: number, pizzasList: Pizzas[]): string {
    return pizzasList
      .filter(pizza => pizza.id === pizzaId)
      .map(ingredient => ingredient.ingredients)[0]
      .map(ingName => ingName.name)
      .reduce(
        (ingList: string = "Ingredients are kept secret", ingName) =>
          ingList + ingName + ",",
        "Ingredients : "
      );
  }
}
