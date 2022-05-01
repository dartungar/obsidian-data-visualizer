import { DataSeriesEntry } from './DataPoint';

export interface DataSeries {
  name: string;
  series: DataSeriesEntry[];
}
