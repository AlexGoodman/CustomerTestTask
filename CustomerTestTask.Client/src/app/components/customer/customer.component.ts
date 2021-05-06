import { Component, OnInit } from '@angular/core';
import { FilterItem } from 'src/app/items/api/filter.item';
import { OrderItem } from 'src/app/items/api/order.item';
import { CustomerItem } from 'src/app/items/customer.item';
import { ItemListResource } from 'src/app/resources/item-list.resource';
import { CustomerService } from 'src/app/services/customer.service';

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

  constructor(private customerService: CustomerService) {}

  ngOnInit(): void {
    this.updateList();
  }

  updateList(): void {
    this.customerService.list(
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

  changePage(changer: number): void {
    this.offset += changer * this.limit;
    this.updateList();    
  }
  
  changeOrder(orderItem: OrderItem|null, name: string): void {
    this.orderItems = this.orderItems.filter(item => item.name !== name);

    if (orderItem !== null) {
      this.orderItems.push(orderItem);
    }
    
    this.updateList();
  }

  changeFilter(filterItem: FilterItem|null, name: string): void {
    this.filterItems = this.filterItems.filter(item => item.name !== name);

    if (filterItem !== null) {
      this.filterItems.push(filterItem);
    }
    
    this.updateList();
  }
}
