using System.Collections.Generic;
using coIT.MyHeco.Login.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace coIT.MyHeco.Login.Web.Model.Hypermedia
{
    public class Siren
    {
        public IList<string> Class { get; set; }
        public IList<Link> Links { get; set; } 
        public IList<Action> Actions { get; set; } 
        public IDictionary<string,object> Properties { get; set; } 
        public List<Siren> Entities { get; set; }

        private Siren()
        {
            Actions = new List<Action>();
            Entities = new List<Siren>();
            Properties = new Dictionary<string, object>();
            Class = new List<string>();
        }
        public Siren(IUrlHelper urlHelper): this()
        {
            Links = new List<Link>()
            {
                new Link(urlHelper.AbsoluteAction(),"self")
            };

        }
        public Siren(string selfLink) : this()
        {
            Links = new List<Link>()
            {
                new Link(selfLink,"self")
            };

        }
    }
}