using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Core.Collection {
    public static class JObjectEx {
        public static T ToObject<T>(this JObject obj, string token) {
            JsonSerializer serializer = new JsonSerializer();
            return (T)serializer.Deserialize(new JTokenReader(obj.SelectToken(token)), typeof(T));
        }

        public static object ToObject(this JObject obj, string token, Type type) {
            JsonSerializer serializer = new JsonSerializer() {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Include              
            };
            return serializer.Deserialize(new JTokenReader(obj.SelectToken(token)), type);
        }

    }
}
