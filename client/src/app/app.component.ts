import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IPagination } from './models/pagination';
import { IProduct } from './models/product';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  
  title = 'client';
  products: IProduct[] = []
  
  constructor(private http: HttpClient) { }
  ngOnInit(): void {
    this.getUsers()
  }

  getUsers() {
    this.http.get<IPagination>('https://localhost:5001/api/products?pageSize=50').subscribe(
      (res: IPagination) => {
      this.products = res.data
      console.log(res.data)
      }, error => {
        console.log(error)
    })
  }
}
