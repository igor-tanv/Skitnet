import { Component, OnInit } from '@angular/core';
import { IProduct } from '../models/product';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

  constructor(private shopService: ShopService) { }

  products: IProduct[] = []

  ngOnInit(): void {
    this.shopService.getProducts().subscribe(response => {
      this.products = response.data
    }, error => {
      console.log(error)
    })
  }

}