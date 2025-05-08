using System.Text;

namespace RestWithASPNETUdemy.Hypermedia
{
    public class HyperMediaLink
    {
        public string Rel { get; set; }

        private string href;
        private readonly object _lock = new object();

        public string Href
        {
            get
            {
                lock (_lock)
                {
                    return href.Replace("%2F", "/");
                }
            }
            set
            {
                lock (_lock)
                {
                    href = value;
                }
            }
        }
        public string Type { get; set; }
        public string Action { get; set; }
    }
}

