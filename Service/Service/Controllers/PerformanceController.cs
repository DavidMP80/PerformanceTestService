
namespace Performance.Rest.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.NetworkInformation;
    using System.Text;
    using System.Web.Http;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    using Service.Models.Request;
    using Service.Models.Response;

    public class PerformanceController : ApiController
    {
        [HttpPost]
        public string GetResponseTime(UrlRequestModel request)
        {
            var responses = this.GetResponseTimes(request);
            
            var response = JsonConvert.SerializeObject(responses, new JsonSerializerSettings
                            {
                                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                                DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind,
                                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                                DateParseHandling = DateParseHandling.None,
                                StringEscapeHandling = StringEscapeHandling.EscapeHtml
                            });
            
            return response;
        }

        private UrlResponseTimeModel GetResponseTimes(UrlRequestModel request)
        {
            var response = new UrlResponseTimeModel();

            response.Id = request.Id;
            response.Name = request.Name;
            response.Url = request.Url;
            response.Repetition = request.Repetition;

            response.ResponseTimes = new List<int>();

            for (int rep = 0; rep < request.Repetition; rep++)
            {
                response.ResponseTimes.Add(this.GetResponseTimes(request.Url));                
            }

            response.ResponseTimeAverage = response.ResponseTimes.Average();

            return response;
        }

        private int GetResponseTimes(string url)
        {
            var pingSender = new Ping();
            
            // Create a buffer of 32 bytes of data to be transmitted.
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);

            // Set options for transmission:
            // The data can go through 64 gateways or routers
            // before it is destroyed, and the data packet
            // cannot be fragmented.
            PingOptions options = new PingOptions(64, true);
            
            int response = 0;

            try
            {
                var reply = pingSender.Send(url, 5000, buffer, options);
                
                if (reply.Status == IPStatus.Success)
                {
                    response = int.Parse(reply.RoundtripTime.ToString());
                }                
            }
            catch (Exception ex)
            {                
                //throw ;
            }

            return response;
            
        }

        private void PingSender_PingCompleted(object sender, PingCompletedEventArgs e)
        {
            
        }
    }
}