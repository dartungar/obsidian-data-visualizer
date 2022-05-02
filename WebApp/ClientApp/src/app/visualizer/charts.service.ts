import { Injectable } from '@angular/core';
import { ChartType } from '../models/Chart';
import { Chart } from '../models/Chart';
import { NotificationService } from '../notifications/notification.service';

@Injectable({
  providedIn: 'root',
})
export class ChartsService {
  chartTypes = Object.keys(ChartType);
  charts: Chart[] = [];

  constructor(public alertService: NotificationService) {}

  addChart(chartType: string, fields: string[]): void {
    try {
      var type = chartType as ChartType;
      this.charts.push({ type, fields });
    } catch (error) {
      this.alertService.ErrorAlert(
        `Error adding chart with type ${chartType} : ${error}`
      );
    }
  }

  removeChart(index: number): void {
    if (index > this.charts.length) {
      this.alertService.ErrorAlert('Error removing chart: index too high');
    }
    this.charts = this.charts.filter((c, i) => i !== index);
  }
}
