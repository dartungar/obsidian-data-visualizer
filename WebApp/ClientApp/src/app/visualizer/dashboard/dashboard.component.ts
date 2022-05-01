import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { BackendService } from 'src/app/data-loader/backend.service';
import { DataSeries } from 'src/app/models/DataSeries';
import { ChartType } from '../../models/Chart';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {
  dataSets: DataSeries[] = [];
  chartTypes = Object.keys(ChartType);
  charts: any[] = [];
  fieldNameForm: FormGroup;
  showAddChartModal = false;

  constructor(public backend: BackendService, private fb: FormBuilder) {
    console.log('chart types:', this.chartTypes);
    this.fieldNameForm = this.fb.group({
      fieldName: ['choose field(-s)'],
      chartType: ['choose chart type'],
    });
  }

  ngOnInit(): void {}

  onSubmit(): void {
    var fields = this.fieldNameForm.get('fieldName')?.value;
    var chartType = this.fieldNameForm.get('chartType')?.value;
    console.log(fields, chartType);
    this.charts.push({ fields, chartType });
    console.log(this.charts);
    this.showAddChartModal = false;
  }

  toggleShowAddChartModal(): void {
    this.showAddChartModal = !this.showAddChartModal;
    console.log('showAddChartModal:', this.showAddChartModal);
  }
}
