using System.Text.Json;
using Core;

Console.WriteLine("Welcome to Obsidian Data Visualizer Console Demo");
Console.WriteLine("enter path for daily notes folder (by default './test-data' will be used");
var pathString = Console.ReadLine() ?? "test-data";
Console.WriteLine($"path: {pathString}");

var service = new CoreService();
service.InitDataProvider(pathString);
service.GetRawData();
service.ProcessData();
var testData = service.GetTimeSeries("productivity");
Console.WriteLine(JsonSerializer.Serialize(testData));
var shape = service.GetDataShape();
Console.WriteLine(JsonSerializer.Serialize(shape));
Console.ReadLine();