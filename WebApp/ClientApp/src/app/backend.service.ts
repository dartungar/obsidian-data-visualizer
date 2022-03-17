import { Injectable } from '@angular/core';
import { TimeSeries } from '../app/models/TimeSeries';
import { DataShape } from './models/DataShape';
import { HttpClient, HttpErrorResponse, HttpParams } from "@angular/common/http";
import { Observable, of, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { MessageService } from './message.service';

@Injectable({
  providedIn: 'root'
})
export class BackendService {
  private dataIsLoaded = false;
  private dataShape: DataShape | undefined;
  private timeSeriesCollection: TimeSeries[] = [];
  private baseUrl = "https://localhost:7073/api/data/";

  constructor(private http: HttpClient, private alertService: MessageService) { }

  // TODO: pass filename regex
  loadAndProcessDataFromFolder(path: string, filenameRegex: string): void {
    this.http.post(this.baseUrl + "load", { path, filenameRegex }, { observe: "response" })
      .pipe(catchError(this.handleError))
      .subscribe(resp => {
        if (resp.ok) {
          this.dataIsLoaded = true;
          this.timeSeriesCollection = []; // clear old data
          this.alertService.SuccessAlert("Loaded and processed data");
        } else {
          this.dataIsLoaded = false;
        }
    })
  }

  getDataShape() {
    return this.dataShape;
  }

  loadDataShape(): void {
    this.http.get<DataShape>(this.baseUrl + "shape").pipe(catchError(this.handleError)).subscribe(ds => this.dataShape = ds);
  }

  getTimeSeries(fieldName: string): TimeSeries  {
    return this.timeSeriesCollection.filter(ts => ts.name === fieldName)[0];
  }

  loadTimeSeries(fieldName: string): void {
    // delete old data
    this.timeSeriesCollection = this.timeSeriesCollection.filter(ts => ts.name !== fieldName);
    this.http.get<TimeSeries>(this.baseUrl + "timeseries", {
      params: new HttpParams().set('fieldName', fieldName)
    }).pipe(catchError(this.handleError))
      .subscribe(ts => this.timeSeriesCollection.push(ts));
  }

  handleError(error: HttpErrorResponse, object: Object) {
    console.log(error.message);
    this.alertService.ErrorAlert(`Error ${error.status} while requesting with ${object}: ${error.message}`)
    return throwError(() => new Error(`Error ${error.status} while requesting with ${object}: ${error.message}`));
  }
}
