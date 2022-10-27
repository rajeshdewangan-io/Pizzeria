import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { tap, map, filter } from "rxjs/operators";
import { Observable, BehaviorSubject } from "rxjs";
import { PizzaOrder, Pizzas } from "./pizzas";

@Injectable({
  providedIn: "root"
})
export class PizzaService {
  // _api_url = "../assets/pizzeria.json";
  _api_host_url = "https://localhost:5001/api/pizzeria";

  /* Behaviour Subject to hold Pizza and Topping orders live throughout the website */
  // toppingOrders = new BehaviorSubject<OrderTopping[]>([]);
  // pizzaOrders = new BehaviorSubject<OrderPizza[]>([]);

  constructor(private http: HttpClient) { }

  getPizzasList(): Observable<Pizzas[]> {
    return this.http
      .get<Pizzas[]>(this._api_host_url)
      .pipe(map(data => data['pizzas']));
  }


  getPizzas(id: number): Observable<Pizzas> {
    let url = "https://localhost:5001/api/pizzeria/" + id
    return this.http
      .get<Pizzas>(url)
      .pipe(map(data => data));
  }

  orderPizza(pizzaOrder: PizzaOrder) {
    let url = "https://localhost:5001/api/pizzeria"
    return this.http
      .post<any>(url, pizzaOrder)
      .pipe(map(data => data));
  }
}
