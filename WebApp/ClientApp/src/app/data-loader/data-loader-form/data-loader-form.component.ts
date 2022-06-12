import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { BackendService } from '../backend.service';

@Component({
  selector: 'app-data-loader-form',
  templateUrl: './data-loader-form.component.html',
  styleUrls: ['./data-loader-form.component.css'],
})
export class DataLoaderFormComponent implements OnInit {
  pathControl = new FormControl(
    'C:\\Users\\dartungar\\source\\repos\\obsidian-data-visualizer\\test-data'
  );
  filenameRegexControl = new FormControl('\\d\\d\\d\\d-\\d\\d-\\d\\d');

  constructor(public backend: BackendService) {}

  ngOnInit(): void {}

  loadData(): void {
    this.backend.loadAndProcessDataFromFolder(
      this.pathControl.value,
      this.filenameRegexControl.value
    );
  }

  getLoadText(): string {
    return this.backend.dataIsLoaded ? "Reload data" : "Load data";
  }
}
