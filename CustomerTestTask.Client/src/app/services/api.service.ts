import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { FilterItem } from '../items/api/filter.item';
import { OrderItem } from '../items/api/order.item';
import { ItemListResource } from '../resources/item-list.resource';

@Injectable()
export class ApiService<TItem> {

  constructor(private http: HttpClient) { }

  public list(
    route: string,
    limit: number = 0, 
    offset: number = 0, 
    filterItems: FilterItem[] = [], 
    orderItems: OrderItem[] = [] 
  ): Observable<ItemListResource<TItem>> {
    let params = new HttpParams()
      .set('limit', limit.toString())      
      .set('offset', offset.toString());
          
    filterItems.forEach(item => {
      params = params.append("filterItems", item.json());
    });
    
    orderItems.forEach(item => {
      params = params.append("orderItems", item.json());
    });

    return this.http.get<ItemListResource<TItem>>(`${environment.apiUrl}${route}`, {params});
  }
}
