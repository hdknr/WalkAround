﻿using System;

using NetTopologySuite;
using NetTopologySuite.IO.Converters;
using NetTopologySuite.Geometries;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace WalkAround
{
	public class Station
	{
		public int id {get;set; }
		public string name {get;set;}

		[JsonConverter(typeof(GeosConverter))]
		public Point location {get;set;}

		[JsonConverter(typeof(GeosConverter))]
		public Polygon area {get;set;}

		public static Station Deserialize(string json)
		{
			return JsonConvert.DeserializeObject<Station> (json);
		}
	}
}
