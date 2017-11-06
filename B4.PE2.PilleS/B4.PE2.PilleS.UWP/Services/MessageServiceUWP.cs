using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
[assembly: Xamarin.Forms.Dependency(typeof(B4.PE2.PilleS.UWP.Services.MessageServiceUWP))]
namespace B4.PE2.PilleS.UWP.Services
{
    public class MessageServiceUWP : IMessageService
    {
        public string GetWelcomeMessage()
        {
            return "running on UWP";
        }
    }
}
