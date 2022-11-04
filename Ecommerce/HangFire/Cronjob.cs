using Hangfire;

namespace Ecommerce.HangFire
{
    public class Cronjob
    {
        public void SendMail (string username)
        {
             Console.WriteLine($"Send WelcomeMail :{username}");
            
        }
        public void SendDelayeMail(string username)
        {
            Console.WriteLine($"SendDelayMail :{username}");

        }
        public void SendinVoice(string username)
        {
            Console.WriteLine($"SendInVoiceMail :{username}");

        }
        public void Unsubscribed(string username)
        {
            Console.WriteLine($"Unsubscribed :{username}");

        }
    }
}
