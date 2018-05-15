﻿using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Services.Entities.JSON;
using Services.Entities.JSON.TextAnalytics;
using Services.Interfaces;

namespace Services.Implementation
{
    public class TextService : ITextService
    {
        public IConfiguration Configuration { get; set; }

        public TextService(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public async Task<double> GetScore(string text)
        {
            var url = Configuration["TextAnalyticsAPI"];
            var key = Configuration["TextAnalyticsKey"];

            var something = $"{{ \"documents\": [ {{ \"language\": \"en\", \"id\": \"1\", \"text\": \"{text}\"}}]}}";

            var byteData = Encoding.UTF8.GetBytes(something);

            using (var content = new ByteArrayContent(byteData))
            {
                var response = await CognitiveServicesHttpClient.HttpResponseMessage(content, url, key);

                if (response.IsSuccessStatusCode)
                {
                    var responseBytes =  await response.Content.ReadAsStringAsync();

                    var result = JSONHelper.FromJson<TextAnalytics>(responseBytes);

                    return result.Documents[0].Score;
                }
            }

            return 0;
        }
    }
}