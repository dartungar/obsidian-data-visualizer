using Core;
using System.Text.Json;

Console.WriteLine("Welcome to Obsidian Data Visualizer Console Demo");
Console.WriteLine("enter path for daily notes folder (by default './test-data' will be used");
var pathString = Console.ReadLine();
if (string.IsNullOrEmpty(pathString))
    pathString = "C:\\Users\\dartungar\\source\\repos\\obsidian-data-visualizer\\test-data";
Console.WriteLine($"path: {pathString}");

var service = new CoreService();
service.LoadRawData(pathString);
service.ProcessData();
var testData = service.GetDataSeries("productivity");
Console.WriteLine(JsonSerializer.Serialize(testData));
var shape = service.GetDataShape();
Console.WriteLine(JsonSerializer.Serialize(shape));
Console.ReadLine();