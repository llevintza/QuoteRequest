using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Configuration;
using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace QuoteRequest.Controllers
{
    [RoutePrefix("api/quote")]
    public class QuoteRequestController : ApiController
    {
        [HttpPost]
        [Route("request")]
        public async Task<HttpResponseMessage> RequestQuote()
        {
            var value = await this.Request.Content.ReadAsStringAsync();
            var quoteRequest = JsonConvert.DeserializeObject<Models.QuoteRequest>(value);
            try
            {
                this.SendEmail(quoteRequest);
            }
            catch(Exception ex)
            {
            }
        return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("trades")]
        public HttpResponseMessage GetTrades()
        {
            var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6); ;
            using (var reader = new StreamReader(Path.Combine(location, "trades.csv")))
            {
                var trades = new List<string>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    trades.AddRange(line.Split(','));
                }

                return this.Request.CreateResponse(System.Net.HttpStatusCode.OK, trades.Distinct());
            }
        }

        private void SendEmail(Models.QuoteRequest request)
        {
            using (var smtpClient = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"], Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"])))
            {
                smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["EmailAddress"], ConfigurationManager.AppSettings["EmailPassword"]);
                smtpClient.UseDefaultCredentials = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = false;
                smtpClient.Timeout = 15000;
                var mail = new MailMessage
                {
                    From = new MailAddress(ConfigurationManager.AppSettings["EmailAddress"], "Hackathon")
                };
                mail.To.Add(new MailAddress("wildcard1@beazley.com"));
                mail.Body = JsonConvert.SerializeObject(request);
                smtpClient.Send(mail);
            }
        }
    }
}
