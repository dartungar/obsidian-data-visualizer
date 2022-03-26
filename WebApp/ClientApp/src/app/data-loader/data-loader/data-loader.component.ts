import { Component, OnInit } from '@angular/core';
import { BackendService } from '../backend.service';

@Component({
  selector: 'app-data-loader',
  templateUrl: './data-loader.component.html',
  styleUrls: ['./data-loader.component.css'],
})
export class DataLoaderComponent implements OnInit {
  constructor(private backend: BackendService) {}

  ngOnInit() {
  }

  loadData(): void {
    this.loadDataDefault();
  }

  private loadDataDefault(): void {
    this.backend.loadAndProcessDataFromFolder(
      'C:\\Users\\dartungar\\source\\repos\\obsidian-data-visualizer\\test-data',
      '\\d\\d\\d\\d-\\d\\d-\\d\\d'
    );
  }

  getDataShape(): void {
    var shape = this.backend.getDataShape();
    console.log(shape);
  }
}
