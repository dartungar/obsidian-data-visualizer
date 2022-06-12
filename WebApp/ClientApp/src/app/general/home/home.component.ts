import { Component } from '@angular/core';
import { BackendService } from '../../data-loader/backend.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  constructor(public backend: BackendService) { }

  ngOnInit() {
    if (!this.backend.dataIsLoaded) {
      this.backend.tryLoadProcessedDataFromLocalStorage();
    }
  }
}
