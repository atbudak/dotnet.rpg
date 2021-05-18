using System.Text.Json.Serialization;

namespace dotnet.rpg.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RPGClass
    {
        Knight,
        Mage,
        Cleric
    }
}