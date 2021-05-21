import { Injectable } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FilterItem } from '../items/api/filter.item';
import { OrderItem } from '../items/api/order.item';

@Injectable()
export class UrlQueryService {

  constructor(
    private _route: ActivatedRoute,
    private _router: Router
  ) { }

  public getPage(): number {
    return Number(this._route.snapshot.queryParamMap.get('page'));
  }

  public getFilterItems(): FilterItem[] {
    return this._route.snapshot.queryParamMap.getAll('filterItems').map(FilterItem.fromJson);
  }

  public getOrderItems(): OrderItem[] {
    return this._route.snapshot.queryParamMap.getAll('orderItems').map(OrderItem.fromJson);
  }

  public updatePageFilterOrder(
    page: number, 
    filterItems: FilterItem[], 
    orderItems: OrderItem[]
  ): void {
    const queryParams: any = {};
    
    if (page > 0) {
      queryParams.page = page;
    }

    if (filterItems.length > 0) {
      queryParams.filterItems = filterItems.map(item => item.json());
    }

    if (orderItems.length > 0) {
      queryParams.orderItems = orderItems.map(item => item.json());
    }

    this._router.navigate([], {relativeTo: this._route, queryParams});
  }
}
