import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DataLoaderComponent } from './data-loader/data-loader.component';
import { NotificationService } from '../notifications/notification.service';

@NgModule({
  declarations: [DataLoaderComponent],
  imports: [CommonModule],
  exports: [DataLoaderComponent],
})
export class DataLoaderModule {}
