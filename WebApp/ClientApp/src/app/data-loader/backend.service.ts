import { Injectable } from '@angular/core';
import { TimeSeries } from '../models/TimeSeries';
import { DataShape } from '../models/DataShape';
import {
  HttpClient,
  HttpErrorResponse,
  HttpParams,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { NotificationService } from '../notifications/notification.service';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class BackendService {
  public isLoading = false;
  public dataIsLoaded = false; // TODO: better way to track loading state?
  public dataShape: DataShape | undefined;
  private timeSeriesCollection: TimeSeries[] = [];
  private baseUrl = '/api/data/';

  constructor(
    private http: HttpClient,
    private alertService: NotificationService
  ) {}

  // TODO: pass filename regex
  loadAndProcessDataFromFolder(path: string, filenameRegex: string): void {
    console.log(path, filenameRegex);
    this.isLoading = true;
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
        this.isLoading = false;
      });
  }

  // todo: check if data loaded
  getDataShape() {
    if (!this.dataIsLoaded) {
      this.alertService.ErrorAlert(
        `To get data shape, data must be loaded first.`
      );
      return;
    }

    if (!this.dataShape) {
      this.isLoading = true;
      this.loadDataShape().subscribe((data) => {
        this.dataShape = data;
        this.isLoading = false;
      });
    }

    return this.dataShape;
  }

  // TODO: check if data loaded
  private loadDataShape(): Observable<DataShape> {
    return this.http
      .get<DataShape>(this.baseUrl + 'shape')
      .pipe(catchError(this.handleError));
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
