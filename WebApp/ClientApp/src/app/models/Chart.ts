export interface ChartParams {
  type: ChartType,
  fields: string[]
}

export enum ChartType {
  lineChart = "line",
  barChart = "bar",
  areaChart = "area",
  pieChart = "pie",
  bubbleChart = "bubble",
  treeMapChart = "treeMap",
}
