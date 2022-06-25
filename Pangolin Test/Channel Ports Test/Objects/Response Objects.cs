using Pangolin;
using ChannelPorts.Pangolin.UI_Constants;

namespace Channel_Ports_Test
{
    public class Responses
    {
        static Responses Response;
        public Responses() { }
        public Responses(Responses response)
        {
            Message = response.Message;
            DateTime = response.DateTime;
            Sender = response.Sender;
        }

        public string Message { get; set; }
        public string DateTime { get; set; }
        public string Sender { get; set; }

        public static Responses AdminResponse => Response ?? (Response = new Responses
        {
            Message = "Response Message",
            DateTime = "01/07/2021",
            Sender = "Geeks Admin",
        });
    }
}