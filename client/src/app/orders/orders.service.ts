import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getOrdersForUser() {
    return this.http.get(this.baseUrl + 'orders');
  }

  getOrderDetail(id: number) {
    return this.http.get(this.baseUrl + 'orders/' + id);
  }
}