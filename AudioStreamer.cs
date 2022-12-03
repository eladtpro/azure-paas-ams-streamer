﻿using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Management.Media.Models;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;

namespace RadioArchive
{
    public class AudioStreamer
    {
        [Timeout("10:00:00")]
        [FunctionName("AudioStreamer")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest request,
            [Blob("data/{Query.name}", FileAccess.Read, Connection = "Settings:AzureInputStorage")] BlobClient blob,
            [DurableClient] IDurableOrchestrationClient starter,
            IOptions<Settings> options, 
            IStreamingLocatorGenerator generator, 
            ILogger<AudioStreamer> logger)
        {
            Settings settings = options.Value;
            logger.LogInformation("[AudioStreamer] C# HTTP trigger function processed a request.");
            logger.LogInformation($"[AudioStreamer] CreateMediaServicesClientAsync token: {settings}, Blob name {blob.Name}, blob length {blob}");

            IDictionary<string, StreamingPath> urls = new Dictionary<string, StreamingPath>();
            BlobProperties props = await blob.GetPropertiesAsync();

            if (ContentType.Audio == props.ContentType.ResolveType()){
                LocatorContext locator = new LocatorContext(blob.Name, props);
                string result = await starter.StartNewAsync<LocatorContext>(nameof(StreamingLocatorGenerator), locator);
            }

            logger.LogInformation($"[AudioStreamer] urls: {urls}");

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(urls), Encoding.UTF8, "application/json")
            };
        }
    }
}

