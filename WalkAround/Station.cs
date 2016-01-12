using System;

using NetTopologySuite.IO.Converters;
using NetTopologySuite.Geometries;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WalkAround
{
	public class Station
	{
		public int id {get;set; }
		public string name {get;set;}

		[JsonConverter(typeof(GeometryConverter))]
		public Point location {get;set;}

		[JsonConverter(typeof(GeometryConverter))]
		public Polygon area {get;set;}

		public static Station Deserialize(string json)
		{
			return JsonConvert.DeserializeObject<Station> (json);
		}
	}
}

