import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FilterItem } from '../items/api/filter.item';
import { OrderItem } from '../items/api/order.item';
import { CustomerItem } from '../items/customer.item';
import { ItemListResource } from '../resources/item-list.resource';
import { ApiService } from './api.service';

@Injectable()
export class CustomerService {
  constructor(private apiService: ApiService<CustomerItem>) { }

  public list(    
    limit: number = 0, 
    offset: number = 0, 
    filterItems: FilterItem[] = [], 
    orderItems: OrderItem[] = [] 
  ): Observable<ItemListResource<CustomerItem>> {
    return this.apiService.list(
      'customer/list',
      limit,
      offset,
      filterItems,
      orderItems
    );
  }
}
