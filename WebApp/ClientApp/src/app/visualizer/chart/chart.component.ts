import { Component, Inject, Input, OnInit } from '@angular/core';
import { BackendService } from 'src/app/data-loader/backend.service';
import { TimeSeries } from 'src/app/models/TimeSeries';
import { NotificationService } from 'src/app/notifications/notification.service';

enum ChartType {
  lineChart,
  barChart,
  areaChart,
  pieChart,
  bubbleChart,
  treeMap,
}

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.css'],
})
export class ChartComponent implements OnInit {
  @Input() fieldNames: string[] = [];
  @Input() chartType: ChartType = ChartType.lineChart;
  chartData: any[] = []; // FIX ME
  view: any = [700, 300];
  xAxisLabel: string = 'Year';
  yAxisLabel: string = 'Population';

  constructor(
    private backend: BackendService,
    private notifications: NotificationService
  ) {}

  ngOnInit(): void {
    this.initData(this.fieldNames);
  }

  initData(fieldNames: string[]): void {
    this.chartData = [];
    console.log(fieldNames);
    fieldNames.forEach((field) => {
      try {
        this.backend.loadTimeSeries(field).subscribe((ts) => {
          console.log(ts);
          console.log(Array.from(ts.entries));
          var data = {
            name: ts.name,
            series: ts.entries.map((e) => {
              return {
                name: e.name,
                value: e.values[0],
              };
            }),
          };
          this.chartData.push(data);
          console.log(this.chartData);
        });
      } catch (error) {
        this.notifications.ErrorAlert(`Error loading data for chart: ${error}`);
      }
    });
    //console.log(this.chartData);
  }
}
