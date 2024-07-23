using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace Mini_App.Helpers.Enums;

public enum ClassType
{
    [JsonConverter(typeof(StringEnumConverter))]
    FrontEnd =15,
    BackEnd=20
}
