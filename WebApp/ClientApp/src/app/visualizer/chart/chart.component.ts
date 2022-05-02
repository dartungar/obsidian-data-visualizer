import { Component, Input, OnInit } from '@angular/core';
import { BackendService } from 'src/app/data-loader/backend.service';
import { DataSeries } from 'src/app/models/DataSeries';
import { NotificationService } from 'src/app/notifications/notification.service';
import { map } from 'rxjs/operators';
import { ChartType, Chart } from '../../models/Chart';
import { ChartsService } from '../charts.service';

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.css'],
})
export class ChartComponent implements OnInit {
  @Input() fieldNames: string[] = [];
  @Input() chartType: ChartType = ChartType.lineChart;
  @Input() index: number = 0;
  params: Chart | undefined;
  chartData: DataSeries[] = [];
  view: any = [700, 300];
  xAxis: boolean = true;
  yAxis: boolean = true;
  xAxisLabel: string = 'Date';
  yAxisLabel: string = 'Points';
  timeline: boolean = true;
  legend = true;
  legendPosition = 'right';

  constructor(
    private backend: BackendService,
    private chartService: ChartsService
  ) {}

  ngOnInit(): void {
    this.initData(this.fieldNames);
  }

  initData(fieldNames: string[]): void {
    this.chartData = [];
    this.backend
      .loadMultipleDataSeries(fieldNames)
      .subscribe((tsCollection) => {
        // TODO: determine whether we need to trim based on axis type (only do this if axis type is date)
        if (this.timeline) {
          this.trimDateTime(tsCollection);
        }
        this.chartData = tsCollection;
        console.log('tsCollection:', this.chartData);
      });
  }

  // trim time from datetime
  trimDateTime(data: DataSeries[]): void {
    data.forEach((d) => {
      d.series.forEach((p) => (p.name = p.name.substring(0, 10))); // crude trimming
    });
  }

  removeChart(): void {
    this.chartService.removeChart(this.index);
  }
}
