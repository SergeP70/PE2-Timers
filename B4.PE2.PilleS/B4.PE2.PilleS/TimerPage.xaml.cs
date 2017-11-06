using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace B4.PE2.PilleS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimerPage : ContentPage
    {
        Label lblClock;
        Stopwatch stopwatch;
        TimeSpan ts;
        bool isRunning = false;
        double pageWidth;

        public TimerPage()
        {
            InitializeComponent();
            stopwatch = new Stopwatch();

            lblClock = new Label
            {
                FormattedText = FormatLabel(0, 0, 0, 0),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };

            // create the Start/Stop button and attach Clicked_handler
            Button btnStartStop = new Button
            {
                Text = "Start/Stop",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                VerticalOptions=LayoutOptions.End
            };
            btnStartStop.Clicked += OnButtonStartStopClicked;

            // create the Clear button and attach Clicked_handler
            Button btnClear = new Button
            {
                Text = "Clear",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                VerticalOptions=LayoutOptions.End
            };
            btnClear.Clicked += OnBtnClearClicked;

            // Assemble the page
            Content = new StackLayout
            {
                Children = {
                    new Label {
                        Text = "Welcome to myTimer",
                        FontSize= Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                        HorizontalOptions=LayoutOptions.Center
                    },
                    lblClock,
                    btnStartStop,
                    btnClear
                }
            };

            // To make sure the page size has been calculated => handle the SizeChanged EventHander
            // Else the Page's height/width keeps returning -1.
            SizeChanged += OnTimerPageSizeChanged;

        }

        private void OnBtnClearClicked(object sender, EventArgs e)
        {
            stopwatch.Reset();
            lblClock.FormattedText = FormatLabel(0, 0, 0, 0);
            isRunning = false;
        }

        private FormattedString FormatLabel(int hr, int min, int sec, int ms)
        {
            // use PointSize 28 (UWP) or 22 (android/iOS) for milliseconds
            int pointSize =0;
            switch(Device.RuntimePlatform)
            {
                case Device.UWP:
                    pointSize = 28;
                    break;
                default:
                    pointSize = 22;
                    break;
            }

            // We return a nice stopwatch string with milliseconds in light-gray and smaller fontsize
            FormattedString formattedString = new FormattedString();
            formattedString.Spans.Add(new Span { Text = hr.ToString("00") + ":" });
            formattedString.Spans.Add(new Span { Text = min.ToString("00") + ":" });
            formattedString.Spans.Add(new Span { Text = sec.ToString("00") });
            if (pageWidth > 0)
            {
                formattedString.Spans.Add(new Span { Text = "." + ms.ToString("00"), ForegroundColor = Color.LightGray, FontSize = pageWidth / 8 });
            }
            else
            {
                // use PointSize 28 (UWP) or 22 (android/iOS) for ms when pageWidth is still not calculated (before page is built)
                // fontsize = resolution (160) * PointSize (28) / Points to the inch (72)
                formattedString.Spans.Add(new Span { Text = "." + ms.ToString("00"), ForegroundColor = Color.LightGray, FontSize = pointSize * 160 / 72});
            }
            return formattedString;
        }

        private void OnButtonStartStopClicked(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                // Start Timer
                stopwatch.Start();
                isRunning = true;
                
                Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
                {
                    // Get the elapsed time as a TimeSpan value.
                    ts = stopwatch.Elapsed;
                    lblClock.FormattedText = FormatLabel(ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                    return true;
                });
            }
            else
            {
                // Pauze Timer
                stopwatch.Stop();
                isRunning = false;
            }
        }

        private void OnTimerPageSizeChanged(object sender, EventArgs e)
        {
            pageWidth = this.Width;
            // Scale the font size to the page width
            // 12 characters are displayed by setting the font size to one-sixth of the page width
            lblClock.FontSize = pageWidth / 6;

        }
    }
}