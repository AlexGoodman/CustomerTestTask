import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CustomerComponent } from './components/customer/customer.component';
import { ApiService } from './services/api.service';
import { OrderComponent } from './components/order/order.component';
import { FilterComponent } from './components/filter/filter.component';
import { UrlQueryService } from './services/url-query.service';

@NgModule({
  declarations: [
    AppComponent,
    CustomerComponent,
    OrderComponent,
    FilterComponent
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule
  ],
  providers: [ApiService, UrlQueryService],
  bootstrap: [AppComponent]
})
export class AppModule { }
