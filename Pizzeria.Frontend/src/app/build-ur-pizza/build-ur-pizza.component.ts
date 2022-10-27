import { Component, Inject, OnInit } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from "@angular/material";
import { ActivatedRoute, Router } from "@angular/router";
import { PizzaService } from "../pizza.service";
import { Ingredient, Pizzas, Size, Sauce, Topping, PizzaOrder } from "../pizzas";

export interface DialogData {
  animal: string;
  name: string;
}
@Component({
  selector: "app-build-ur-pizza",
  templateUrl: "./build-ur-pizza.component.html",
  styleUrls: ["./build-ur-pizza.component.css"]
})
export class BuildUrPizzaComponent implements OnInit {
  id: number;
  private sub: any;
  pizza: Pizzas;
  pizzaSizes: Size[] = [];
  pizzaIngredients: Ingredient[] = [];
  pizzaSauces: Sauce[] = [];
  pizzaToppings: Topping[] = [];
  totalPrice: number;
  mobNumberPattern = "^((\\+91-?)|0)?[0-9]{10}$";
  pizzaOrderForm = new FormGroup({
    size: new FormControl(''),
    sauce: new FormControl(''),
    extraCheese: new FormControl(''),
    toppings: new FormControl(''),
    customerName: new FormControl(''),
    contactNumber: new FormControl('', Validators.pattern(this.mobNumberPattern))
  });

  constructor(private pizzaService: PizzaService, private route: ActivatedRoute, public dialog: MatDialog, private router: Router) {

  }
  ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
      this.id = +params['id'];
      this.pizzaService.getPizzas(this.id).subscribe(data => {
        this.pizza = data;
        this.pizzaSizes = this.pizza.sizes;
        this.pizzaSauces = this.pizza.sauces;
        this.pizzaIngredients = this.pizza.ingredients;
        this.pizzaToppings = this.pizza.toppings;
        this.setTotalPrice();
      });
    });

  }

  /** Get and set the total cost of pizza. */
  setTotalPrice() {
    this.totalPrice = 0;
    let basePrice = this.pizza.price;
    let priceForSize = this.pizzaOrderForm.controls.size.value ? this.pizza.sizes.filter(m => m.id == this.pizzaOrderForm.controls.size.value)[0].price : 0;
    let priceForSauce = this.pizzaOrderForm.controls.sauce.value ? this.pizza.sauces.filter(m => m.id == this.pizzaOrderForm.controls.sauce.value)[0].price : 0;
    let priceForExtraCheese = this.pizzaOrderForm.controls.extraCheese.value == 1 ? 50 : 0;
    let priceForToppings = this.pizzaOrderForm.controls.toppings.value ? this.getPriceForToppings() : 0;
    this.totalPrice = basePrice + priceForSize + priceForExtraCheese + priceForSauce + priceForToppings;
  }
  /** Gets the total cost of all Toppings. */
  getPriceForToppings() {
    return this.pizza.toppings
      .filter(topping =>
        this.pizzaOrderForm.controls.toppings.value.includes(topping.id)
      ).map(t => t.price).reduce((acc, value) => acc + value, 0);
  }

  orderPizza() {
    if (this.pizzaOrderForm.invalid) {
      return;
    }
    var pizzaOr: PizzaOrder = {
      size: this.pizzaOrderForm.controls.size.value,
      sauce: this.pizzaOrderForm.controls.sauce.value,
      extraCheese: this.pizzaOrderForm.controls.extraCheese.value == 1 ? true : false,
      toppings: this.pizzaOrderForm.controls.toppings.value,
      contactNumber: this.pizzaOrderForm.controls.contactNumber.value,
      customerName: this.pizzaOrderForm.controls.customerName.value,
      id: this.id,
      totalPrice: this.totalPrice,
      name: this.pizza.name,
      type: this.pizza.type
    }
    this.pizzaService.orderPizza(pizzaOr).subscribe(data => {
      this.openDialog(data);
    });
  }

  openDialog(orderNumber: number): void {
    const dialogRef = this.dialog.open(OrderPizzaDialog, {
      width: '500px',
      data: { name: this.pizzaOrderForm.controls.customerName.value, orderNumber: orderNumber },
    });

    dialogRef.afterClosed().subscribe(result => {
      this.router.navigate(["/pizzaMenu"]);
    });
  }
}

@Component({
  selector: 'order-pizza-dialog',
  templateUrl: 'order-pizza-dialog.html',
})
export class OrderPizzaDialog {
  constructor(
    public dialogRef: MatDialogRef<OrderPizzaDialog>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
  ) { }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
