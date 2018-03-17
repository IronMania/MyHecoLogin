using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace coIT.MyHeco.Login.Web.Model.Hypermedia
{
    public class Action
    {
        public string Name { get; set; }
        public string Title { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public MethodType Method { get; set; }
        public string Href { get; set; }
        public string Type { get; set; } = "application/x-www-form-urlencoded";
        public IList<Field> Fields { get; set; } = new List<Field>();
        public enum MethodType
        {
            [EnumMember(Value = "POST")]
            Post,
            [EnumMember(Value = "PUT")]
            Put,
            [EnumMember(Value = "DELETE")]
            Delete,
            [EnumMember(Value = "GET")]
            Get,
            [EnumMember(Value = "PATCH")]
            Patch
        }
    }
}