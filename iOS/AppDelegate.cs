﻿using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using System.Threading;
using System.Threading.Tasks;

namespace WalkAround.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public WalkAround.MainContent MainPage { 
			get { return (WalkAround.MainContent)WalkAround.App.Current.MainPage; } }
		
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

			LoadApplication (new App ());

			var station = this.GetStation ();
				
//			var area = station.Area;


			new Thread(new ThreadStart(() => {
				InvokeOnMainThread (() => {
					MainPage.TimeLabel.Text = DateTime.Now.ToString("T");   
				});
			})).Start();


			return base.FinishedLaunching (app, options);
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

