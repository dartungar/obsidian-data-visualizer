export interface DataShape {
  fields: FieldShape[];
  dateRange: [Date, Date];
}

export interface FieldShape {
  name: string;
  valueType: string; // TODO: get proper typing; generics?..
  uniqueValues: string[];
}
