export interface Chart {
  type: ChartType;
  fields: string[];
}

export enum ChartType {
  line = 'line',
  barVertical = 'barVertical',
  barHorizontal = 'barHorizontal',
  area = 'area',
  pie = 'pie',
  bubble = 'bubble',
  treeMap = 'treeMap',
  heatMap = 'heatMap',
}
