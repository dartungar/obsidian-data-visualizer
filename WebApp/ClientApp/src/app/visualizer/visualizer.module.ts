import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DataExplorerComponent } from './data-explorer/data-explorer.component';
import { ChartComponent } from './chart/chart.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ReactiveFormsModule } from '@angular/forms';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AddChartModalComponent } from './add-chart-modal/add-chart-modal.component';

@NgModule({
  declarations: [DataExplorerComponent, ChartComponent, DashboardComponent, AddChartModalComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    NgxChartsModule,
    BrowserAnimationsModule,
  ],
})
export class VisualizerModule {}
