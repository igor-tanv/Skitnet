import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IBrand } from '../shared/models/brands';
import { IPagination, Pagination } from '../shared/models/pagination';
import { IType } from '../shared/models/productType';
import { map } from 'rxjs/operators'
import { ShopParams } from '../shared/models/shopParams';
import { IProduct } from '../shared/models/product';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  baseUrl = 'https://localhost:5001/api/';
  products: IProduct[] = [];
  brands: IBrand[] = [];
  types: IType[] = [];
  pagination = new Pagination();
  shopParams = new ShopParams();
  

  constructor(private http: HttpClient) { }

  getProducts(shopParams: ShopParams) {
    
    const { brandId, typeId, sort, pageNumber } = shopParams

    let params = new HttpParams();

    if (brandId !== 0) params = params.append('brandId', brandId.toString())
    if (typeId !== 0 ) params = params.append('typeId', typeId.toString())
    if (shopParams.search) params = params.append('search', shopParams.search)
    
    params = params.append('sort', sort)
    params = params.append('pageIndex', shopParams.pageNumber.toString())
    params = params.append('pageIndex', shopParams.pageSize.toString())
    
    return this.http.get<IPagination>(this.baseUrl + 'products', { observe: 'response', params })
      .pipe(
        map((response: any) => {
          this.products = response.body.data
          return response.body
        })
      )
  }

  getProduct(id: number) {
    const product = this.products.find(p => p.id === id)
    if (product) return of(product)
    return this.http.get<IProduct>(this.baseUrl + `products/${id}`)
  }

  getBrands() {
    if (this.brands.length > 0) return of(this.brands);
    return this.http.get<IBrand[]>(this.baseUrl + 'products/brands').pipe(
      map(response => {
        this.brands = response;
        return response;
      })
    );
  }

  getTypes() {
    if (this.types.length > 0) return of(this.types);
    return this.http.get<IType[]>(this.baseUrl + 'products/types').pipe(
      map(response => {
        this.types = response;
        return response;
      })
    );
  }


}
