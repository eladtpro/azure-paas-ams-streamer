﻿using System;
namespace RadioArchive
{
    public class Settings : ISettings
    {
        public const string MediaSettings = "MediaSettings";

        public string   AzureWebJobsStorage             { get; set; }
        public string[] Excluded                        { get; set; }
        public bool     AutoProcessStreamingLocator     { get; set; }
        public string   AadClientId                     { get; set; }
        public string   AadEndpoint                     { get; set; }
        public string   AadSecret                       { get; set; }
        public string   AadTenantId                     { get; set; }
        public string   AccountName                     { get; set; }
        public string   ArmAadAudience                  { get; set; }
        public Uri      ArmEndpoint                     { get; set; }
        public string   Location                        { get; set; }
        public string   ResourceGroup                   { get; set; }
        public string   SubscriptionId                  { get; set; }
        public double   AssetExpiryHours                { get; set; }
        public string   DefaultStreamingEndpointName    { get; set; }
        public string   StreamingLocatorScheme          { get; set; }
        public string   AssetStorageAccountName         { get; set; }
        public bool     DeleteJobs                      { get; set; }
        public string   StreamingTransformName          { get; set; }
    }
}
