import { Injectable } from '@angular/core';

enum AlertType {
  info,
  success,
  error,
}

class Alert {
  constructor(public message: string, public type: AlertType) {}
}

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  public alerts: Alert[] = [];

  constructor() {}

  public InfoAlert(message: string): void {
    this.Alert(message, AlertType.info);
  }

  public SuccessAlert(message: string): void {
    this.Alert(message, AlertType.success);
  }

  public ErrorAlert(message: string): void {
    this.Alert(message, AlertType.error);
  }

  private Alert(message: string, type: AlertType): void {
    this.alerts.push(new Alert(message, type));
  }

  public PopAlert(): void {
    this.alerts.pop();
  }
}
