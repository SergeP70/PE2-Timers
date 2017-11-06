using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace B4.PE2.PilleS
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AboutPage : ContentPage
	{
		public AboutPage ()
		{
			InitializeComponent ();


            Content = new Label
            {
                FormattedText = new FormattedString
                {
                    Spans =
                    {
                        new Span { Text= "PE2 Timer and Form", FontAttributes=FontAttributes.Bold, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))},
                        new Span { Text= "\u000A(Dropbox opdracht Programmeren 5)", FontAttributes=FontAttributes.Bold, FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label))},
                        new Span { Text = Environment.NewLine},
                        new Span { Text = "\u000A\u2003There was nothing so " },
                        new Span { Text = "very", FontAttributes = FontAttributes.Italic },
                        new Span { Text = " remarkable in that; nor did Alice " + "think it so " },
                        new Span { Text = "very", FontAttributes = FontAttributes.Italic },
                        new Span { Text = " much out of the way to hear the " + 
                                            "Rabbit say to itself \u2018Oh " + 
                                            "dear! Oh dear! I shall be too late!" + 
                                            "\u2019 (when she thought it over " + 
                                            "afterwards, it occurred to her that " + 
                                            "she ought to have wondered at this, " + 
                                            "but at the time it all seemed quite " + 
                                            "natural); but, when the Rabbit actually " },
                        new Span { Text = "took a watch out of its waistcoat-pocket", FontAttributes = FontAttributes.Italic },
                        new Span { Text = ", and looked at it, and then hurried on, " + 
                                            "Alice started to her feet, for it flashed " + 
                                            "across her mind that she had never before " + 
                                            "seen a rabbit with either a waistcoat-" + 
                                            "pocket, or a watch to take out of it, " + 
                                            "and, burning with curiosity, she ran " + 
                                            "across the field after it, and was just " + 
                                            "in time to see it pop down a large " + 
                                            "rabbit-hold under the hedge." }
                    }
                },
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                BackgroundColor= Color.LightGoldenrodYellow,
                TextColor = Color.DarkBlue,
            };
		}
	}
}