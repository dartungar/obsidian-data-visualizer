interface MultiSeries {}

interface Series {
  name: string;
  series: Entry[];
}

interface Entry {
  name: string;
  value: string | number | boolean;
}
