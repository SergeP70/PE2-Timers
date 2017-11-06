using System;
using Xamarin.Forms;

namespace B4.PE2.PilleS
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var msgService = DependencyService.Get<IMessageService>();
            lblRunning.Text = msgService.GetWelcomeMessage();
            lblRunning.FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label));
            lblScreenSize.Text = String.Format($"({Width} \u00D7 {Height})");
            SizeChanged += MainPage_SizeChanged;
            
        }

        private void MainPage_SizeChanged(object sender, EventArgs e)
        {
            lblScreenSize.Text = String.Format($"{Width} \u00D7 {Height}");
        }

        private async void btnTimer_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TimerPage());
        }

        private async void btnLapTimer_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LapTimerPage());
        }

        private async void btnFeedback_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FeedbackPage());
        }

        private async void btnAbout_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutPage());
        }

        private async void btnClock_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ClockPage());
        }
    }
}
