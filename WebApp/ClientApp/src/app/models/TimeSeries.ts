import { DataPoint } from '../models/DataPoint';

export interface TimeSeries {
  name: string;
  entries: DataPoint[];
}

export interface TimeSeriesNgxCharts {
  name: string;
  series: DataPoint[];
}

export function TimeSeriesToNgxFormat(ts: TimeSeries) {
  return { name: ts.name, series: ts.entries }
}
