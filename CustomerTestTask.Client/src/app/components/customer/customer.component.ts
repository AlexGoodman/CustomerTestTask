import { Component, OnInit } from '@angular/core';
import { FilterItem } from 'src/app/items/api/filter.item';
import { OrderItem } from 'src/app/items/api/order.item';
import { CustomerItem } from 'src/app/items/customer.item';
import { ItemListResource } from 'src/app/resources/item-list.resource';
import { CustomerService } from 'src/app/services/customer.service';
import { UrlQueryService } from 'src/app/services/url-query.service';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  providers: [CustomerService],
  styleUrls: ['./customer.component.scss']
})
export class CustomerComponent implements OnInit {

  public itemList: CustomerItem[] = [];
  public totalCount: number = 0;
  public limit: number = 10;
  public offset: number = 0;
  private filterItems: FilterItem[] = [];
  private orderItems: OrderItem[] = [];

  constructor(
    private _customerService: CustomerService,
    private _urlQueryService: UrlQueryService
  ) {
    // http://localhost:4200/?page=1&filterItems=%7B%22name%22:%22Name%22,%22customOperator%22:%22contains%22,%22value%22:%22f%22%7D&filterItems=%7B%22name%22:%22Age%22,%22customOperator%22:%22%3E%22,%22value%22:%2210%22%7D&filterItems=%7B%22name%22:%22Id%22,%22customOperator%22:%22%3E%22,%22value%22:%22150%22%7D&orderItems=%7B%22name%22:%22Age%22,%22customOperator%22:%22desc%22%7D&orderItems=%7B%22name%22:%22Name%22,%22customOperator%22:%22asc%22%7D&orderItems=%7B%22name%22:%22Id%22,%22customOperator%22:%22desc%22%7D
  
    this.offset = this._urlQueryService.getPage() * this.limit;
    this.filterItems = this._urlQueryService.getFilterItems();
    this.orderItems = this._urlQueryService.getOrderItems();       
  }

  ngOnInit(): void {    
    this.updateList();
  }

  updateList(): void {
    this._urlQueryService.updatePageFilterOrder(
      this.offset / this.limit, 
      this.filterItems, 
      this.orderItems
    );

    this._customerService.list(
      this.limit,
      this.offset,
      this.filterItems,
      this.orderItems      
    ).subscribe((data: ItemListResource<CustomerItem>) => {
      this.totalCount = data.totalCount;
      this.itemList = data.itemList;
      if (this.offset > 0 && this.totalCount <= this.offset) {
        this.offset = 0;        
        if (this.totalCount > 0) {
          this.updateList();
        }        
      }
    });
  }

  public changePage(changer: number): void {
    this.offset += changer * this.limit;
    this.updateList();    
  }
  
  public changeOrder(orderItem: OrderItem|null, name: string): void {
    this.orderItems = this.orderItems.filter(item => item.name !== name);

    if (orderItem !== null) {
      this.orderItems.push(orderItem);
    }
    
    this.updateList();
  }

  public changeFilter(filterItem: FilterItem|null, name: string): void {
    this.filterItems = this.filterItems.filter(item => item.name !== name);

    if (filterItem !== null) {
      this.filterItems.push(filterItem);
    }
    
    this.updateList();
  }

  public getFilter(name: string): FilterItem|null {
    return this.filterItems.filter(item => item.name === name)[0] ?? null;
  }

  public getOrder(name: string): OrderItem|null {
    return this.orderItems.filter(item => item.name === name)[0] ?? null;
  }
}
