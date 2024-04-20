using Newtonsoft.Json;

namespace MauiTemplate2024.Core.Logic;

// TODO: Domain class directly used for data processing goes very against Clean Architecture principles but CA is not part of this template (yet).
// See the native Android of Flutter implementation for the inspiration.

// ReSharper disable once ClassNeverInstantiated.Global
public class Potato
{
    [JsonProperty("id")]
    public string Id { get; init; }

    [JsonProperty("name")]
    public string Name { get; init; } = string.Empty;

    [JsonProperty("description")]
    public string Description { get; init; } = string.Empty;

    [JsonProperty("price")]
    public decimal Price { get; init; }

    [JsonProperty("currency")]
    public string Currency { get; init; } = "EUR";

    [JsonProperty("last_sold")]
    public DateTime LastSold { get; init; }

    [JsonProperty("image_url")]
    public string ImageUrl { get; init; } = string.Empty;


    public bool IsFavorite { get; set; }

    public string PriceDisplay => $"{Price} {Currency}";

    public string LastSoldDisplay => $"Last sold: {LastSold:MMM dd, yyyy}";

    public string IsFavoriteDisplay => IsFavorite ? "This is my favorite variety!" : "This is not my favorite.";
}