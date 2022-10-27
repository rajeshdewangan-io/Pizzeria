export interface Pizzas {
  id: number;
  type: string;
  price: number;
  name: string;
  image: string;
  description: string;
  sizes: Size[];
  ingredients: Ingredient[];
  sauces: Sauce[];
  toppings: Topping[];
}

export interface Size {
  id: number;
  name: string;
  price: number;
}

export interface Topping {
  id: number;
  name: string;
  price: number;
}

export interface Ingredient {
  id: number;
  name: string;
}

export interface Sauce {
  id: number;
  name: string;
  price: number;
}

export interface PizzaOrder {
  id: number;
  type: string;
  totalPrice: number;
  name: string;
  size: number;
  extraCheese: boolean;
  sauce: number;
  toppings: number[];
  customerName: string;
  contactNumber: number;
}