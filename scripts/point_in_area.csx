using System;
using System.IO;
using System.Collections.Generic;

using NetTopologySuite.IO;
using NetTopologySuite.Geometries;

var reader = new GeoJsonReader ();

var area = reader.Read<Polygon> (
    File.ReadAllText("data/shibuya.json"));

var locations = new Dictionary<string, string>{
  {"西日暮里", @"{""type"": ""Point"", ""coordinates"": [139.7691960797771, 35.73323451860732]}" },
  {"ハチ公前", @"{""type"": ""Point"", ""coordinates"": [139.7003236463392, 35.65870361613276]}" },
};

foreach(var location in locations){
    Console.Write(location.Key + ":");
    Console.WriteLine(
        area.Contains(reader.Read<Point> (location.Value)));
}
