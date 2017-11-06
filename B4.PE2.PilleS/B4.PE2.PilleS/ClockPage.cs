using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace B4.PE2.PilleS
{
	public class ClockPage : ContentPage
	{
        Label clockLabel;

        public ClockPage ()
		{
            clockLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            Content = clockLabel;

            SizeChanged += OnClockPage_SizeChanged;

            // Start the clock
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                clockLabel.Text = DateTime.Now.ToString("HH:mm:ss");
                return true;
            });
        }

        private void OnClockPage_SizeChanged(object sender, EventArgs e)
        {
            // Get View whose size is changing
            //View view = (View)sender;

            // +- 8 characters "12:34:56 AM" => 1/4 of page width
            clockLabel.FontSize = this.Width / 4;
        }

        

    }
}