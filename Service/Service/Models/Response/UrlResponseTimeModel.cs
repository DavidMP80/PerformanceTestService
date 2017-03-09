namespace Service.Models.Response
{
    using System.Collections.Generic;

    public class UrlResponseTimeModel
    {

        public UrlResponseTimeModel()
        {
            
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public int Repetition { get; set; }

        public double ResponseTimeAverage { get; set; }

        public ICollection<int> ResponseTimes { get; set; }
    }
}