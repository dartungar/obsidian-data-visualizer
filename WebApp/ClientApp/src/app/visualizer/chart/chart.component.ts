import { Component, Inject, Input, OnInit } from '@angular/core';
import { BackendService } from 'src/app/data-loader/backend.service';
import { TimeSeries, TimeSeriesNgxCharts, TimeSeriesToNgxFormat } from 'src/app/models/TimeSeries';
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
  chartData: TimeSeriesNgxCharts[] = []; // FIX ME
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
    console.log("Loaded all data:", this.chartData);
  }

  // TODO: move most of loading logic into BackendService, clean mess up
  initData(fieldNames: string[]): void {
    this.chartData = [];
    this.backend.loadMultipleTipeSeries(fieldNames)
      //.pipe(map<TimeSeries[], TimeSeriesNgxCharts[]>(
      //  tsCollection => tsCollection.map<TimeSeriesNgxCharts>(
      //    ts => TimeSeriesToNgxFormat(ts))
      .subscribe(tsCollection => {
        this.chartData = tsCollection.map(TimeSeriesToNgxFormat);
        console.log("tsCollection:", this.chartData)
      });
  }
}
