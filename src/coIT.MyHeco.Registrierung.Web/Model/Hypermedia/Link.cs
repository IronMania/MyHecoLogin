using System.Collections.Generic;

namespace coIT.MyHeco.Registrierung.Web.Model.Hypermedia
{
    public class Link
    {
        public Link(string href, params string[] links)
        {
            Href = href;
            Rel = new List<string>(links);
        }

        public IList<string> Rel { get; private set; }
        public string Href { get; private set; }
    }
}