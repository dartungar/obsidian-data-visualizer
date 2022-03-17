export interface DataShape {
  fields: FieldShape[];
  dateRange: [Date, Date];
}

export interface FieldShape {
  name: string;
  type: string; // TODO: get proper typing; generics?..
  uniqueValues: string[];
}
