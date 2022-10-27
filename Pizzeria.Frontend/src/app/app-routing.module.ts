import { NgModule, Component } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { HomeComponent } from "./home/home.component";
import { PizzaMenuComponent } from "./pizza-menu/pizza-menu.component";
import { BuildUrPizzaComponent } from "./build-ur-pizza/build-ur-pizza.component";

const routes: Routes = [
  { path: "pizzaMenu", component: PizzaMenuComponent },
  { path: "buildPizza/:id", component: BuildUrPizzaComponent },
  { path: "**", component: PizzaMenuComponent },
  { path: "", redirectTo: "HomeComponent", pathMatch: "full" }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
export const routingComponents = [
  HomeComponent,
  PizzaMenuComponent,
  BuildUrPizzaComponent
];
