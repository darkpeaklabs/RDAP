using DarkPeakLabs.Rdap.Values.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DarkPeakLabs.Rdap
{
    /// <summary>
    /// This data structure represents events that have occurred on an instance of an object class
    /// </summary>
    public class RdapEvent
    {
        /// <summary>
        /// a string denoting the reason for the event
        /// </summary>
        [JsonPropertyName("eventAction")]
        public RdapEventAction? EventAction { get; set; }

        /// <summary>
        /// an optional identifier denoting the actor responsible for the event
        /// </summary>
        [JsonPropertyName("eventActor")]
        public string EventActor { get; set; }

        /// <summary>
        /// a string denoting the reason for the event
        /// </summary>
        [DisplayFormat(DataFormatString = "o")]
        [JsonPropertyName("eventDate")]
        public DateTime EventDate { get; set; }

        /// <summary>
        /// Array of links
        /// </summary>
        [JsonPropertyName("links")]
        public IReadOnlyCollection<RdapLink> Links { get; set; }
    }
}