import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DataLoaderComponent } from './data-loader/data-loader.component';
import { NotificationService } from '../notifications/notification.service';
import { DataLoaderFormComponent } from './data-loader-form/data-loader-form.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [DataLoaderComponent, DataLoaderFormComponent],
  imports: [CommonModule, ReactiveFormsModule],
  exports: [DataLoaderComponent],
})
export class DataLoaderModule {}
