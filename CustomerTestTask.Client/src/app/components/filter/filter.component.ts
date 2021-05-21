import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { FilterItem } from 'src/app/items/api/filter.item';
import { debounceTime } from 'rxjs/operators';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.scss']
})
export class FilterComponent implements OnInit {

  @Input() name: string = '';      
  @Input() filterType: string = 'number';      
  @Input() outerFilterItem: FilterItem|null = null;      
  @Output() addItem: EventEmitter<FilterItem> = new EventEmitter<FilterItem>();
  @Output() removeItem: EventEmitter<string> = new EventEmitter<string>();
  public operatorList: string[] = FilterItem.OPERATOR_LIST;
  public operatorSelect!: FormControl;
  public filterInput!: FormControl;
  
  constructor() {}

  ngOnInit(): void {
    this.operatorList = this.filterType === 'number'
      ? FilterItem.OPERATOR_LIST.filter(o => o !== 'contains')
      : ['contains', '==', '!='];
    
    this.operatorSelect = new FormControl(this.outerFilterItem ? this.outerFilterItem.customOperator : this.operatorList[0]);
    this.filterInput = new FormControl(this.outerFilterItem ? this.outerFilterItem.value : '');

    this.operatorSelect.valueChanges.subscribe(() => {      
      if (this.filterInput.value !== '') {
        this.addFilter();
      }
    }); 
    
    this.filterInput.valueChanges
      .pipe(debounceTime(1500))
      .subscribe(value => {      
        if (value === '') {
          this.removeItem.emit(this.name);
        } else {
          this.addFilter();
        }
      });
  }

  private addFilter(): void {    
    this.addItem.emit(new FilterItem(
      this.name, 
      this.operatorSelect.value, 
      this.filterInput.value
    )); 
  }
}

