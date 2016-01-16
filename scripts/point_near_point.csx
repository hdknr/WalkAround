using System;
using System.IO;
using System.Collections.Generic;

using NetTopologySuite.IO;
using NetTopologySuite.Geometries;

var reader = new GeoJsonReader ();

// Shibuya 109
var p109 = reader.Read<Point>(
    @"{""type"": ""Point"", ""coordinates"": [139.6987858465003569 , 35.6594736339830973]}"
);

// Sendagaya
var sendagaya = reader.Read<Point>(
    @"{""type"": ""Point"", ""coordinates"": [139.70678078023693, 35.674680601838666]}"
);


// http://stackoverflow.com/questions/1253499/simple-calculations-for-working-with-lat-lon-km-distance
// Latitude: 1 deg = 110.574 km
// Longitude: 1 deg = 111.320*cos(latitude) km

var d1km = 1.0 / 110.574;
var a109 = p109.Buffer(d1km);
// Console.WriteLine(a109);         // POLYGON

var locations = new Dictionary<string, string>{
  {"西日暮里", @"{""type"": ""Point"", ""coordinates"": [139.7691960797771, 35.73323451860732]}" },
  {"ハチ公前", @"{""type"": ""Point"", ""coordinates"": [139.7003236463392, 35.65870361613276]}" },
};

foreach(var location in locations){
    var loc = reader.Read<Point>(location.Value);
    Console.WriteLine("From Shibuya 109:{0} {1}",
        location.Key,
        a109.Contains(loc));
}

var area = sendagaya.Buffer(d1km) ;
foreach(var location in locations){
    var loc = reader.Read<Point>(location.Value);
    Console.WriteLine("From Sendagaya :{0} {1}",
        location.Key,
        area.Contains(loc));
}
