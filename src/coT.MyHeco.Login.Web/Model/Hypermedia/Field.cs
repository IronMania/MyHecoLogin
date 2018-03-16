using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace coT.MyHeco.Login.Web.Model.Hypermedia
{
    public class Field
    {
        /// <summary>
        ///     See http://www.w3.org/TR/html5/single-page.html#the-input-element
        /// </summary>
        public enum InputType
        {
            Hidden,
            Email,
            Password,
            Text,
            Number,
            Search
        }

        public Field(string name, InputType type)
        {
            Name = name;
            Type = type;
        }
        public Field(string name, InputType type, string value)
        {
            Name = name;
            Type = type;
            Value = value;
        }

        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public InputType Type { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }
    }
}