import { Component, OnInit } from "@angular/core";
import { PizzaService } from "../pizza.service";
import { Pizzas } from "../pizzas";

@Component({
  selector: "app-pizza-menu",
  templateUrl: "./pizza-menu.component.html",
  styleUrls: ["./pizza-menu.component.css"]
})
export class PizzaMenuComponent implements OnInit {
  cartButtonToggle: boolean = true;
  pizzasList: Pizzas[] = [];
  selectedPizzaId: number;
  selectedPizzaList = {};
  totalPrice: number = 0;

  constructor(private pizzaService: PizzaService) {
    this.pizzaService.getPizzasList().subscribe(data => {
      this.pizzasList = data;
    });
  }

  ngOnInit() { }

  /* To toggle Add to cart and add buttons */
  cartToggler(pizzaId) {
    this.cartButtonToggle = !this.cartButtonToggle;
    this.selectedPizzaId = pizzaId;
    this.cartButtonToggle = true; // Reset toggler for every Pizza button
  }

  /* Decrement pizza button and Total Price calculation*/
  decrementor(pizzaId) {
    let count = 0;
    let price = this.pizzasList
      .filter(p => p.id === pizzaId)
      .map(pizza => pizza.price)
      .pop();

    if (this.selectedPizzaList[pizzaId]) {
      if (this.selectedPizzaList[pizzaId] > 0) {
        --this.selectedPizzaList[pizzaId];
        this.totalPrice = this.totalPrice - price;
      } else {
        this.selectedPizzaList[pizzaId] = --count;
        this.totalPrice = this.totalPrice - price;
      }
    }
  }

  /* Increment pizza button and Total Price calculation */
  incrementor(pizzaId) {
    let count = 0;
    let price = this.pizzasList
      .filter(p => p.id === pizzaId)
      .map(pizza => pizza.price)
      .pop();

    if (this.selectedPizzaList[pizzaId]) {
      ++this.selectedPizzaList[pizzaId];
      this.totalPrice = this.totalPrice + price;
    } else {
      this.selectedPizzaList[pizzaId] = ++count;
      this.totalPrice = this.totalPrice + price;
    }
  }

}
