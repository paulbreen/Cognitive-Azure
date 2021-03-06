﻿
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Services.Entities.JSON
{
    public static class JSONHelper
    {
        public static T FromJson<T>(string json) => JsonConvert.DeserializeObject<T>(json, Converter.Settings);

        public static string ToJson<T>(T obj) => JsonConvert.SerializeObject(obj, Converter.Settings);

        internal class Converter
        {
            public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
                Converters = { new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal } },
            };
        }
    }
}