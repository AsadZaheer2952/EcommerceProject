using DocumentFormat.OpenXml.Drawing.Charts;
using Ecommerce.HangFire;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangfireController : ControllerBase
    {
        Cronjob _cronjob = new Cronjob();

        [HttpPost("welcome")]
        public IActionResult Welcome(string username)
        {
            var jobId = BackgroundJob.Enqueue(() => _cronjob.SendMail(username));
            return Ok($"Job ID: {jobId}. Welcome mail sent to the user!");

        }
        [HttpPost("Delayed")]
        public IActionResult Delayed(string username)
        {
            var jobId = BackgroundJob.Schedule(() => _cronjob.SendDelayeMail(username),TimeSpan.FromMinutes(1));
            return Ok($"Job ID: {jobId}. Schedule (mail will be sent after 1 min)");

        }
        [HttpPost("invoice")]
        public IActionResult SendinVoice(string username)
        {
            RecurringJob.AddOrUpdate(() => _cronjob.SendinVoice(username),Cron.Monthly);
            return Ok($"RecurringJob Schedule (monthly) for {username}");

        }
        [HttpPost("unsubscribed")]
        public IActionResult Unsubscribed(string username)
        {
            var jobId = BackgroundJob.Enqueue(() => _cronjob.Unsubscribed(username));
            BackgroundJob.ContinueJobWith(jobId, () => Console.WriteLine($"Job ID: {jobId},Confirmation mail send to {username}"));

            return Ok($"Unsubscribed");
        }

    }
}