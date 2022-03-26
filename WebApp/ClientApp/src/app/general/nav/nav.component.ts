import { Component, OnInit } from '@angular/core';
import { BackendService } from 'src/app/data-loader/backend.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent implements OnInit {
  constructor(public backend: BackendService) {}

  ngOnInit(): void {}
}
