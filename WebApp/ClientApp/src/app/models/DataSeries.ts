import { DataPoint } from './DataPoint';

export interface DataSeries {
  name: string;
  series: DataPoint[];
}
