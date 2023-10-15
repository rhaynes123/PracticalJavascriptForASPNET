using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BillsTracker.Dtos
{
    public record BillDto
    {
        [JsonPropertyName("name")]
        public string? Name { get; init; }
        [JsonPropertyName("amount")]
        public decimal? Amount { get; init; }
        [JsonPropertyName("paid")]
        public bool? Paid { get; init; }
    }
}

