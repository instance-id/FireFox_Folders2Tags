// ----------------------------------------------------------------------------
// -- Project : https://github.com/instance-id/FireFox_Folders2Tags          --
// -- instance.id 2020 | http://github.com/instance-id | http://instance.id  --
// ----------------------------------------------------------------------------

using System.IO;
using Newtonsoft.Json;

namespace instance.id.FTC
{
    public static class Data
    {
        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class Bookmark
        {
            public static Bookmark Create(string Path) =>
                new JsonSerializer().Deserialize<Bookmark>(new JsonTextReader(new StreamReader(File.OpenRead(Path))));

            public static void Save(object bookmark, string Path) =>
                new JsonSerializer()
                    .Serialize(new JsonTextWriter(
                        new StreamWriter(Path) {AutoFlush = true}), bookmark);

            [JsonProperty("guid")] public string Guid { get; set; }
            [JsonProperty("title")] public string Title { get; set; }
            [JsonProperty("index")] public int Index { get; set; }
            [JsonProperty("dateAdded")] public long DateAdded { get; set; }
            [JsonProperty("lastModified")] public long LastModified { get; set; }
            [JsonProperty("id")] public long Id { get; set; }
            [JsonProperty("typeCode")] public int TypeCode { get; set; }

            [JsonProperty("tags")]
            // [JsonConverter(typeof(CustomArrayConverter<string>))]
            public string Tags { get; set; }

            [JsonProperty("iconuri")] public string IconUri { get; set; }
            [JsonProperty("type")] public string Type { get; set; }
            [JsonProperty("uri")] public string Uri { get; set; }
            [JsonProperty("charset")] public string Charset { get; set; }
            [JsonProperty("root")] public string Root { get; set; }
            [JsonProperty("annos")] public Annotations[] Annotation { get; set; }
            [JsonProperty("children")] public Bookmark[] Children { get; set; }
        }

        public class Annotations
        {
            [JsonProperty("name")] public string Name { get; set; }
            [JsonProperty("flags")] public long Flags { get; set; }
            [JsonProperty("expires")] public long Expires { get; set; }
            [JsonProperty("mimeType")] public string MimeType { get; set; }
            [JsonProperty("type")] public long Type { get; set; }
            [JsonProperty("value")] public string Value { get; set; }
        }
    }
}
