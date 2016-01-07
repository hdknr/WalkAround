using System;
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


			new Thread(new ThreadStart(() => {
				InvokeOnMainThread (() => {
					MainPage.TimeLabel.Text = DateTime.Now.ToString("T");   
				});
			})).Start();


			return base.FinishedLaunching (app, options);
		}
	}
}

