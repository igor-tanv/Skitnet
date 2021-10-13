import { Component, OnInit } from '@angular/core';
import { IBrand } from '../models/brands';
import { IProduct } from '../models/product';
import { IType } from '../models/productType';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

  constructor(private shopService: ShopService) { }

  products!: IProduct[]
  brands!: IBrand[] 
  types!: IType[]

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
    
  }

  getProducts() {
    this.shopService.getProducts().subscribe(response => {
      this.products = response.data
    }, error => {
      console.log(error)
    })
  }

  getBrands() {
    this.shopService.getBrands().subscribe(response => {
      this.brands = response
    }, error => {
      console.log(error)
    })
  }

  getTypes() {
    this.shopService.getTypes().subscribe(response => {
      this.types = response
    }, error => {
      console.log(error)
    })
  }

}
