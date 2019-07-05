using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Wikiled.Invoices.Yaml.Data
{
    public class InvoiceFieldsConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(IEnumerable<InvoiceFields>));
        }

        public override bool CanWrite => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var result = new List<InvoiceFields>();
            while (reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.StartObject)
                {
                    reader.Read();
                }

                if (reader.TokenType == JsonToken.PropertyName)
                {
                    var field = new InvoiceFields();
                    field.Key = reader.Value.ToString();
                    reader.Read();
                    if (reader.TokenType == JsonToken.StartArray)
                    {
                        var values = new List<string>();
                        reader.Read();
                        while (reader.TokenType != JsonToken.EndArray)
                        {
                            values.Add(NormalizeValue(reader.Value.ToString()));
                            reader.Read();
                        }

                        field.Values = values.ToArray();
                    }
                    else
                    {
                        field.Values = new[] { NormalizeValue(reader.Value.ToString()) };
                    }
                    
                    result.Add(field);
                }
                else
                {
                    throw new InvalidOperationException();
                }

                reader.Read();
            }

            return result.ToArray();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException("There is no writing implemantation");
        }

        private string NormalizeValue(string value)
        {
            if (value.StartsWith("static_"))
            {
                value = value.Replace("static_", string.Empty);
            }

            return value;
        }
    }
}
