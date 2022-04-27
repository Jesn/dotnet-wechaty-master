using Newtonsoft.Json;
namespace Wechaty.Module.Filebox
{
    public class FileBoxJsonObjectUuid: FileBoxJsonObject
    {
        [JsonProperty("boxType")]
        public override FileBoxType BoxType => FileBoxType.Uuid;

        [JsonProperty("uuid")]
        public string UUID { get; set; }
    }
}
