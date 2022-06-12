import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { BackendService } from 'src/app/data-loader/backend.service';
import { ChartsService } from '../charts.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {
  showAddChartModal = false;
  constructor(public chartService: ChartsService) {}

  ngOnInit(): void {}

  toggleShowAddChartModal(): void {
    this.showAddChartModal = !this.showAddChartModal;
    console.log('showAddChartModal:', this.showAddChartModal);
  }
}
