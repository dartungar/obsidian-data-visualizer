import { Injectable } from '@angular/core';
import { DataSeries } from '../models/DataSeries';
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
  public dataAggregate: Object | undefined; // TODO - strict typing for aggregate, like field, unique values / sum of int values, average, etc; computed @ server
  public data: DataSeries[] = [];
  private baseUrl = '/api/data/';

  constructor(
    private http: HttpClient,
    private alertService: NotificationService
  ) {}

  loadAndProcessDataFromFolder(path: string, filenameRegex: string): void {
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
          this.getDataShape(); // убрать отсюда, сделать более системно
          this.dataIsLoaded = true;
          this.data = []; // clear old data
          this.alertService.SuccessAlert('Loaded and processed data');
          this.getDataShape();
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

  loadMultipleDataSeries(fieldNames: string[]): Observable<DataSeries[]> {
    // TODO: clean current data
    return this.http
      .post<DataSeries[]>(
        this.baseUrl + 'timeseries',
        JSON.stringify({ fieldNames }),
        {
          headers: { 'content-type': 'application/json' },
        }
      )
      .pipe(catchError(this.handleError));
  }

  loadDataSeries(fieldName: string): Observable<DataSeries> {
    // delete old data
    this.data = this.data.filter((ts) => ts.name !== fieldName);
    return this.http
      .get<DataSeries>(this.baseUrl + 'timeseries', {
        params: new HttpParams().set('fieldName', fieldName),
      })
      .pipe(catchError(this.handleError));
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
