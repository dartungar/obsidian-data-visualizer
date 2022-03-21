import { Injectable } from '@angular/core';
import { TimeSeries } from '../models/TimeSeries';
import { DataShape } from '../models/DataShape';
import {
  HttpClient,
  HttpErrorResponse,
  HttpParams,
} from '@angular/common/http';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { NotificationService } from '../notifications/notification.service';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class BackendService {
  private dataIsLoaded = false; // TODO: better way to track loading state?
  private dataShape: DataShape | undefined;
  private timeSeriesCollection: TimeSeries[] = [];
  private baseUrl = '/api/data/';

  constructor(
    private http: HttpClient,
    private alertService: NotificationService
  ) {}

  // TODO: pass filename regex
  loadAndProcessDataFromFolder(path: string, filenameRegex: string): void {
    console.log(path, filenameRegex);
    this.http
      .post(
        this.baseUrl + 'load',
        JSON.stringify({ FolderPath: path, FilenameRegex: filenameRegex }),
        {
          observe: 'response',
          headers: { 'content-type': 'application/json' },
        }
      )
      .pipe(catchError(this.handleError))
      .subscribe((resp) => {
        if (resp.ok) {
          this.dataIsLoaded = true;
          this.timeSeriesCollection = []; // clear old data
          this.alertService.SuccessAlert('Loaded and processed data');
        } else {
          this.dataIsLoaded = false;
        }
      });
  }

  getDataShape() {
    return this.dataShape;
  }

  loadDataShape(): void {
    this.http
      .get<DataShape>(this.baseUrl + 'shape')
      .pipe(catchError(this.handleError))
      .subscribe((ds) => (this.dataShape = ds));
  }

  getTimeSeries(fieldName: string): TimeSeries {
    return this.timeSeriesCollection.filter((ts) => ts.name === fieldName)[0];
  }

  loadTimeSeries(fieldName: string): void {
    // delete old data
    this.timeSeriesCollection = this.timeSeriesCollection.filter(
      (ts) => ts.name !== fieldName
    );
    this.http
      .get<TimeSeries>(this.baseUrl + 'timeseries', {
        params: new HttpParams().set('fieldName', fieldName),
      })
      .pipe(catchError(this.handleError))
      .subscribe((ts) => this.timeSeriesCollection.push(ts));
  }

  handleError(error: HttpErrorResponse, object: Object) {
    console.log(error.message);
    this.alertService.ErrorAlert(
      `Error ${error.status} while requesting with ${object}: ${error.message}`
    );
    return throwError(
      () =>
        new Error(
          `Error ${error.status} while requesting with ${object}: ${error.message}`
        )
    );
  }
}
