import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { BackendService } from 'src/app/data-loader/backend.service';
import { TimeSeries } from 'src/app/models/TimeSeries';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {
  dataSets: TimeSeries[] = [];
  fieldNameForm: FormGroup;

  constructor(public backend: BackendService, private fb: FormBuilder) {
    this.fieldNameForm = this.fb.group({
      fieldName: [null],
    });
  }

  ngOnInit(): void {}

  onSubmit(): void {
    var fieldName = this.fieldNameForm.get('fieldName')?.value;
    console.log(fieldName);
    this.backend.loadTimeSeries(fieldName).subscribe((ts) => console.log(ts));
  }
}
