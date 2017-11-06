using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace B4.PE2.PilleS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LapTimerPage : ContentPage
    {
        Label lblClock;
        Label lblSplit;
        StackLayout loggerLayout = new StackLayout
        {
            Padding = 10,
        };
        Stopwatch stopwatch, splitTime;
        TimeSpan ts, tsSplit;
        bool isRunning = false;
        double pageWidth;
        int lapCounter;

        public LapTimerPage()
        {
            InitializeComponent();

            stopwatch = new Stopwatch();
            splitTime = new Stopwatch();
            lapCounter = 0;

            lblClock = new Label
            {
                FormattedText = FormatLabel(0, 0, 0, 0),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };

            lblSplit = new Label
            {
                Text = "SPLIT TIME",
                HorizontalOptions = LayoutOptions.Center,
                FontSize=Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                TextColor=Color.LightSalmon
            };

            // create the Start/Stop button and attach Clicked_handler
            Button btnStartStop = new Button
            {
                Text = "Start/Stop",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions=LayoutOptions.FillAndExpand
            };
            btnStartStop.Clicked += OnButtonStartStopClicked;

            // create the Clear button and attach Clicked_handler
            Button btnClear = new Button
            {
                Text = "Clear",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };
            btnClear.Clicked += OnBtnClearClicked;

            // create the Lap Timer button and attach Clicked_handler
            Button btnLap = new Button
            {
                Text = "Lap Split",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions=LayoutOptions.FillAndExpand
            };
            btnLap.Clicked += OnBtnLapClicked;

            // Assemble the page
            Content = new StackLayout
            {
                Children = {
                    new Label {
                        Text = "Welcome to myLapTimer",
                        FontSize= Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                        HorizontalOptions=LayoutOptions.Center
                    },
                    lblClock,
                    lblSplit,
                    new StackLayout
                    {
                        Orientation= StackOrientation.Horizontal,
                        Spacing=15,
                        Children =
                        {
                            btnStartStop,
                            btnLap
                        }
                    },
                    btnClear,
                    new ScrollView {
                        VerticalOptions=LayoutOptions.FillAndExpand,
                        Content = loggerLayout
                    }
                }
            };

            // To make sure the page size has been calculated => handle the SizeChanged EventHander
            // Else the Page's height/width keeps returning -1.
            SizeChanged += OnTimerPageSizeChanged;
        }

        private void OnBtnLapClicked(object sender, EventArgs e)
        {
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            string elapsedSplitTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", tsSplit.Hours, tsSplit.Minutes, tsSplit.Seconds, tsSplit.Milliseconds / 10);

            if (lapCounter==0)
            {
                loggerLayout.Children.Add(new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Children =
                        {
                            new Label{Text="Lap" },
                            new Label{Text="Interval   ", HorizontalOptions=LayoutOptions.CenterAndExpand},
                            new Label{Text="Total Time", HorizontalOptions = LayoutOptions.End}
                        }
                });
                loggerLayout.Children.Add(new BoxView
                    {
                        Color=Color.LightSalmon,
                        HeightRequest = 1
                    }
                );
            }
            lapCounter++;

            // Add label to scrollable StackLayout
            loggerLayout.Children.Add(new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                        {
                            new Label{Text="# " + lapCounter },
                            new Label{Text=elapsedSplitTime, HorizontalOptions=LayoutOptions.CenterAndExpand, TextColor=Color.LightSalmon},
                            new Label{Text=elapsedTime, HorizontalOptions = LayoutOptions.End}
                        }
            });

            // Restart SplitTimer
            splitTime.Restart();
        }

        private void OnBtnClearClicked(object sender, EventArgs e)
        {
            stopwatch.Reset();
            splitTime.Reset();
            lblClock.FormattedText = FormatLabel(0, 0, 0, 0);
            loggerLayout.Children.Clear();
            lblSplit.Text = "SPLIT TIME";
            lapCounter = 0;
            isRunning = false;
        }

        private FormattedString FormatLabel(int hr, int min, int sec, int ms)
        {
            // use PointSize 28 (UWP) or 22 (android/iOS) for ms
            int pointSize = 0;
            switch (Device.RuntimePlatform)
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
                formattedString.Spans.Add(new Span { Text = "." + ms.ToString("00"), ForegroundColor = Color.LightGray, FontSize = pointSize * 160 / 72 });
            }
            return formattedString;
        }

        private void OnButtonStartStopClicked(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                // Start Timer
                stopwatch.Start();
                splitTime.Start();
                isRunning = true;

                Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
                {
                    // Get the elapsed time as a TimeSpan value.
                    ts = stopwatch.Elapsed;
                    tsSplit = splitTime.Elapsed;
                    lblClock.FormattedText = FormatLabel(ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                    lblSplit.Text= String.Format("{0:00}:{1:00}:{2:00}.{3:00}", tsSplit.Hours, tsSplit.Minutes, tsSplit.Seconds, tsSplit.Milliseconds / 10);
                    return true;
                });
            }
            else
            {
                // Pauze Timer
                stopwatch.Stop();
                splitTime.Stop();
                ts = stopwatch.Elapsed;
                string timeOfPauze = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                isRunning = false;
                loggerLayout.Children.Add(new Label
                {
                    TextColor = Color.DarkGray,
                    Text = "Timer pauzed/stopped at " + timeOfPauze
                });
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