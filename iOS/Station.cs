using System;

using NetTopologySuite;
using NetTopologySuite.IO;
using NetTopologySuite.IO.Converters;
using NetTopologySuite.Geometries;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

using GeoAPI;

namespace WalkAround.iOS
{
	public class Station
	{
		public int id {get;set; }
		public string name {get;set;}

//		[JsonConverter(typeof(GeometryConverter))]
//		public Point location {get;set;}
//
//		[JsonConverter(typeof(GeometryConverter))]
//		public Polygon area {get;set;}
//
		public JToken location {get;set; }
		public JToken area {get;set; }

		public static Station Deserialize(string json)
		{
			return JsonConvert.DeserializeObject<Station> (json);
		}
			
		[JsonIgnore]
		public Point Location 
			{
				get{
					var reader = new GeoJsonReader ();
				return reader.Read<Point> (location.ToString ());
				}
			}

		[JsonIgnore]
		public Polygon Area 
		{
			get{
				var reader = new GeoJsonReader ();
				return reader.Read<Polygon> (area.ToString ());
			}
		}
	}
}

