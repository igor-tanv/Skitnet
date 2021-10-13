import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IBrand } from '../models/brands';
import { IPagination } from '../models/pagination';
import { IType } from '../models/productType';
import { map } from 'rxjs/operators'
import { ShopParams } from '../models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  baseUrl = 'https://localhost:5001/api/';

  

  constructor(private http: HttpClient) { }

  getProducts(shopParams: ShopParams) {
    
    const { brandId, typeId, sort, pageNumber } = shopParams

    let params = new HttpParams();

    if (brandId !== 0) {
      params = params.append('brandId', brandId.toString())
    }

    if (typeId !== 0 ) {
      params = params.append('typeId', typeId.toString())
    }

    
    params = params.append('sort', sort)
    params = params.append('pageIndex', shopParams.pageNumber.toString())
    params = params.append('pageIndex', shopParams.pageSize.toString())
    

    return this.http.get<IPagination>(this.baseUrl + 'products', { observe: 'response', params })
      .pipe(
        map((response: any) => {
          return response.body
        })
      )
  }

  getBrands() {
    return this.http.get<IBrand[]>(this.baseUrl + 'products/brands')
  }

  getTypes() {
    return this.http.get<IType[]>(this.baseUrl + 'products/types')
  }


}
