import { DataPoint } from '../models/DataPoint';

export interface TimeSeries {
  name: string;
  entries: DataPoint[];
  // TODO: dateRange
}
