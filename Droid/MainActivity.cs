using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using System.Collections.Generic;
using System.Linq;
using Android.Locations;
using Android.Util;

using System.Threading.Tasks;
using System.Text;
using System.Threading;

using NetTopologySuite;
using NetTopologySuite.IO;
using GeoAPI;

using NetTopologySuite.CoordinateSystems;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;

namespace WalkAround.Droid
{
	// ConfigurationChanges: 
	//		to stop re-creating this activity when screee size or 
	// 		orientation is changed.
	[Activity (
		Label = "WalkAround.Droid", 
		Icon = "@drawable/icon", MainLauncher = true, 
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation) 
	]
	public class MainActivity : 
		global::Xamarin.Forms.Platform.Android.FormsApplicationActivity, 
		ILocationListener
	{
		public WalkAround.MainContent MainPage {
			get {
				return (WalkAround.MainContent)WalkAround.App.Current.MainPage;
			}
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			var x = this.GetStation ();

			global::Xamarin.Forms.Forms.Init (this, bundle);

			LoadApplication (new App ());

			MainPage.LongitudeLabel.Text = "Longitude";
			MainPage.LatitudeLabel.Text = "Latitiude";

			InitializeLocationManager();

			// 
			new Timer((o) => RunOnUiThread(() =>{
				MainPage.TimeLabel.Text = DateTime.Now.ToString("T");   
			}), null , 0, 1000);
		}

		Location _currentLocation;
		LocationManager _locationManager;
		string _locationProvider;
		static readonly string TAG = "X:" + typeof (MainActivity).Name;

		void InitializeLocationManager()
		{
			_locationManager = (LocationManager) GetSystemService(LocationService);

			Criteria criteriaForLocationService = new Criteria
			{
				Accuracy = Accuracy.Fine
			};
			IList<string> acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);

			if (acceptableLocationProviders.Any())
			{
				_locationProvider = acceptableLocationProviders.First();

				this.OnLocationChanged (_locationManager.GetLastKnownLocation (_locationProvider));
			}
			else
			{
				_locationProvider = string.Empty;
			}
			Log.Debug(TAG, "Using " + _locationProvider + ".");
		}


		/// <summary>
		/// Raises the resume event.
		/// </summary>
		protected override void OnResume ()
		{
			base.OnResume ();
			_locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);

		}

		/// <Docs>Called as part of the activity lifecycle when an activity is going into
		///  the background, but has not (yet) been killed.</Docs>
		/// <summary>
		/// Raises the pause event.
		/// </summary>
		protected override void OnPause()
		{
			base.OnPause();
			_locationManager.RemoveUpdates(this);
		}

		async Task<Address> ReverseGeocodeCurrentLocation()
		{
			Geocoder geocoder = new Geocoder(this);
	
			IList<Address> addressList =
				await geocoder.GetFromLocationAsync(
					_currentLocation.Latitude, 
					_currentLocation.Longitude, 10);

			Address address = addressList.FirstOrDefault();
			return address;
		}

		void DisplayAddress(Address address)
		{
			var label = MainPage.AddressLabel;

			if (address != null)
			{
				StringBuilder deviceAddress = new StringBuilder();
				for (int i = 0; i < address.MaxAddressLineIndex; i++)
				{
					deviceAddress.AppendLine(address.GetAddressLine(i));
				}
				// Remove the last comma from the end of the address.
				label.Text = deviceAddress.ToString();
			}
			else
			{
				label.Text = "Unable to determine the address. Try again in a few minutes.";
			}
		}

		public async void OnLocationChanged(Location location)
		{
			var label = MainPage.LongitudeLabel;

			_currentLocation = location;
			if (_currentLocation == null)
			{
				label.Text = "Unable to determine your location. Try again in a short while.";
			}
			else
			{
				
				label.Text = string.Format("{0:f6},{1:f6}", _currentLocation.Latitude, _currentLocation.Longitude);
				Address address = await ReverseGeocodeCurrentLocation();
				DisplayAddress(address);
			}

			// JTS(Java Topology Suite)
			var service = NtsGeometryServices.Instance;
			var gf = service.CreateGeometryFactory();
			var reader = new GeoJsonReader ();

			var json = @"{""type"": ""Point"", ""coordinates"": [-122.402, 37.7976983333333]}";
//			var obj = reader.Read<Point> (json);
		}

		public void OnProviderDisabled(string provider) {}
		public void OnProviderEnabled(string provider) {}
		public void OnStatusChanged(string provider, Availability status, Bundle extras)
		{
			Log.Debug(TAG, "{0}, {1}", provider, status);
		}

		public WalkAround.Station GetStation()
		{
			var json = @"
{
  ""id"": 3,
  ""name"": ""\u795e\u5357\u90f5\u4fbf\u5c40"",
  ""location"": {
    ""type"": ""Point"",
    ""coordinates"": [139.70062405375, 35.661950715018975]
  },  
  ""area"": {
    ""type"": ""Polygon"",
    ""coordinates"": [[[139.70072405375, 35.661950715018975], [139.70072213227803, 35.661931205986775], [139.70071644170326, 35.661912446675736], [139.70070720071124, 35.66189515799567], [139.70069476442814, 35.66188000434086], [139.7006796107733, 35.661867568057744], [139.70066232209325, 35.66185832706572], [139.7006435627822, 35.661852636490934], [139.70062405375, 35.66185071501897], [139.7006045447178, 35.661852636490934], [139.70058578540676, 35.66185832706572], [139.7005684967267, 35.661867568057744], [139.70055334307187, 35.66188000434086], [139.70054090678877, 35.66189515799567], [139.70053166579675, 35.661912446675736], [139.70052597522198, 35.661931205986775], [139.70052405375, 35.661950715018975], [139.70052597522198, 35.661970224051174], [139.70053166579675, 35.661988983362214], [139.70054090678877, 35.66200627204228], [139.70055334307187, 35.66202142569709], [139.7005684967267, 35.662033861980206], [139.70058578540676, 35.66204310297223], [139.7006045447178, 35.662048793547015], [139.70062405375, 35.66205071501898], [139.7006435627822, 35.662048793547015], [139.70066232209325, 35.66204310297223], [139.7006796107733, 35.662033861980206], [139.70069476442814, 35.66202142569709], [139.70070720071124, 35.66200627204228], [139.70071644170326, 35.661988983362214], [139.70072213227803, 35.661970224051174], [139.70072405375, 35.661950715018975]]]
  }
}
";
			return WalkAround.Station.Deserialize (json);
		}
	}
}

