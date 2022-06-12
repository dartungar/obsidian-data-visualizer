import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { BackendService } from 'src/app/data-loader/backend.service';
import { ChartsService } from '../charts.service';

@Component({
  selector: 'app-add-chart-modal',
  templateUrl: './add-chart-modal.component.html',
  styleUrls: ['./add-chart-modal.component.css'],
})
export class AddChartModalComponent implements OnInit {
  @Output() modalClosed = new EventEmitter<null>();
  fieldNameForm: FormGroup;
  defaultFieldName = this.backend.dataShape?.fields[0];
  defaultChartType = this.chartService.chartTypes[0];

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

  onClose(): void {
    this.modalClosed.emit();
  }

  onSubmit(): void {
    var fields = this.fieldNameForm.get('fieldName')?.value;
    var chartType = this.fieldNameForm.get('chartType')?.value;
    console.log(fields, chartType);
    if (fields == '' || chartType == '') {
      return;
    }
    this.chartService.addChart(chartType, fields);
    this.onClose();
  }
}
