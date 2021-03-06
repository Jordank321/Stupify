﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace SpotifyApi.ResponseTypes
{
    public class AudioAnalysis : BasicModel
    {
        [JsonProperty("bars")]
        public List<AnalysisTimeSlice> Bars { get; set; }

        [JsonProperty("beats")]
        public List<AnalysisTimeSlice> Beats { get; set; }

        [JsonProperty("meta")]
        public AnalysisMeta Meta { get; set; }

        [JsonProperty("sections")]
        public List<AnalysisSection> Sections { get; set; }

        [JsonProperty("segments")]
        public List<AnalysisSegment> Segments { get; set; }

        [JsonProperty("tatums")]
        public List<AnalysisTimeSlice> Tatums { get; set; }

        [JsonProperty("track")]
        public AnalysisTrack Track { get; set; }
    }
}
