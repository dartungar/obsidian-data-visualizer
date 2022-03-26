import { Component, OnInit } from '@angular/core';
import { Alert, AlertType } from '../notification.service';
import { NotificationService } from '../notification.service';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css'],
})
export class NotificationComponent implements OnInit {
  constructor(public _service: NotificationService) {}

  ngOnInit(): void {}

  onAlertClose(): void {
    this._service.PopAlert();
  }

  getCurrentAlert(): Alert | undefined {
    return this._service.alerts.length > 0
      ? this._service.alerts[this._service.alerts.length - 1]
      : undefined;
  }

  getClass(): string {
    switch (this.getCurrentAlert()?.type) {
      case AlertType.success:
        return 'is-success';
      case AlertType.error:
        return 'is-danger';
      case AlertType.info:
        return 'is-info';
      default:
        return '';
    }
  }
}
