import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DataExplorerComponent } from './data-explorer/data-explorer.component';
import { ChartComponent } from './chart/chart.component';
import { DashboardComponent } from './dashboard/dashboard.component';



@NgModule({
  declarations: [
    DataExplorerComponent,
    ChartComponent,
    DashboardComponent
  ],
  imports: [
    CommonModule
  ]
})
export class VisualizerModule { }
