using System;

using Xamarin.Forms;

namespace WalkAround
{
	public class MainContent : ContentPage
	{
		public Label LongitudeLabel = new Label(){
			XAlign = TextAlignment.Center, FontSize=30};
		public Label LatitudeLabel = new Label(){
			XAlign = TextAlignment.Center, FontSize=30};
		
		public MainContent ()
		{
			Content = new StackLayout { 
				Children = {
					new Label { Text = "Hello ContentPage" }
					,LongitudeLabel
					,LatitudeLabel

				}
			};
		}
	}
}


