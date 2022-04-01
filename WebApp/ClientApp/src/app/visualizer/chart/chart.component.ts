import { Component, Input, OnInit } from '@angular/core';
import { BackendService } from 'src/app/data-loader/backend.service';
import { DataSeries } from 'src/app/models/DataSeries';
import { NotificationService } from 'src/app/notifications/notification.service';
import { map } from "rxjs/operators";

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
  chartData: DataSeries[] = []; // FIX ME
  view: any = [700, 300];
  xAxis: boolean = true;
  yAxis: boolean = true;
  xAxisLabel: string = 'Date';
  yAxisLabel: string = 'Points';
  timeline: boolean = true;

  constructor(
    private backend: BackendService,
    private notifications: NotificationService
  ) {}

  ngOnInit(): void {
    this.initData(this.fieldNames);
  }

  // TODO: move most of loading logic into BackendService, clean mess up
  initData(fieldNames: string[]): void {
    this.chartData = [];
    this.backend.loadMultipleDataSeries(fieldNames)
      .subscribe(tsCollection => {
        this.chartData = tsCollection;
        console.log("tsCollection:", this.chartData)
      });
  }
}
