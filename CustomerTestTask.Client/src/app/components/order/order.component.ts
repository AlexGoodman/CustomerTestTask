import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { OrderItem } from 'src/app/items/api/order.item';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss']
})
export class OrderComponent implements OnInit {

  @Input() name: string = '';  
  @Output() addItem: EventEmitter<OrderItem> = new EventEmitter<OrderItem>();
  @Output() removeItem: EventEmitter<string> = new EventEmitter<string>();
  public operatorSelect: FormControl = new FormControl('');
  public operatorList: string[] = OrderItem.OPERATOR_LIST

  constructor() {}

  ngOnInit(): void {
    this.operatorSelect.valueChanges.subscribe(value => {      
      if (value === '') {
        this.removeItem.emit(this.name);
      } else {
        this.addItem.emit(new OrderItem(this.name, value));
      }
    });
  }
}
