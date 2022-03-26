import { Component, OnInit } from '@angular/core';
import { BackendService } from 'src/app/data-loader/backend.service';
import { DataShape } from 'src/app/models/DataShape';

@Component({
  selector: 'app-data-explorer',
  templateUrl: './data-explorer.component.html',
  styleUrls: ['./data-explorer.component.css'],
})
export class DataExplorerComponent implements OnInit {
  dataShape: DataShape | undefined;

  constructor(public backend: BackendService) {}

  ngOnInit(): void {
    this.getDataShape();
  }

  getDataShape(): void {
    var shape = this.backend.getDataShape();
    if (shape) this.dataShape = shape;
  }
}
