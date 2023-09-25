using System.Text.Json.Serialization;

namespace OWNA.ECommerce.Application.Shared.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrderStatus
{
    Pending,
    Processing,
    Shipped,
    Delivered
}