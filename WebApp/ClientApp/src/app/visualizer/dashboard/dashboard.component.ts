import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { BackendService } from 'src/app/data-loader/backend.service';
import { DataSeries } from 'src/app/models/DataSeries';
import { ChartsService } from '../charts.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {
  // dataSets: DataSeries[] = [];
  fieldNameForm: FormGroup;
  showAddChartModal = false;
  defaultFieldName = 'choose field(-s)';
  defaultChartType = 'choose chart type';

  constructor(
    public backend: BackendService,
    public chartService: ChartsService,
    private fb: FormBuilder
  ) {
    this.fieldNameForm = this.fb.group({
      fieldName: this.defaultFieldName,
      chartType: this.defaultChartType,
    });
  }

  ngOnInit(): void {}

  onSubmit(): void {
    var fields = this.fieldNameForm.get('fieldName')?.value;
    var chartType = this.fieldNameForm.get('chartType')?.value;
    console.log(fields, chartType);
    if (
      fields == '' ||
      fields == this.defaultFieldName ||
      chartType == '' ||
      chartType == this.defaultChartType
    ) {
      return;
    }
    this.chartService.addChart(chartType, fields);
    this.showAddChartModal = false;
  }

  toggleShowAddChartModal(): void {
    this.showAddChartModal = !this.showAddChartModal;
    console.log('showAddChartModal:', this.showAddChartModal);
  }
}
