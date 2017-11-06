using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
//ensures the PCL has access to this class.
[assembly: Xamarin.Forms.Dependency(typeof(B4.PE2.PilleS.Droid.Services.MessageServiceDroid))]

namespace B4.PE2.PilleS.Droid.Services
{
    public class MessageServiceDroid : IMessageService 
    {
        public string GetWelcomeMessage()
        {
            return "running on Android";
        }
    }
}