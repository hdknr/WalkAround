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
		public Label AddressLabel = new Label(){
			XAlign = TextAlignment.Center, FontSize=30};

		public Label TimeLabel = new Label(){
			XAlign = TextAlignment.Center, FontSize=30, TextColor=Color.Red};
		
		public MainContent ()
		{
			Content = new StackLayout { 
				Children = {
					new Label { Text = "Hello ContentPage" }
					,LongitudeLabel
					,LatitudeLabel
					,AddressLabel
					,TimeLabel
				}
			};
					
		}
	}
}