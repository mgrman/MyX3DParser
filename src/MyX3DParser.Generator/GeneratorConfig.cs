using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MyX3DParser.Run
{
    public class GeneratorConfig
    {
        [JsonConstructor]
        public GeneratorConfig(string? unifiedObjectModel, string? targetFolder, DataTypeBackingLibrary? dataTypes)
        {
            this.unifiedObjectModel = unifiedObjectModel;
            this.targetFolder = targetFolder;
            this.dataTypes = dataTypes ?? DataTypeBackingLibrary.Custom;
        }

        public string? unifiedObjectModel { get; }
        public string? targetFolder { get; }
        public DataTypeBackingLibrary? dataTypes { get; }
    }
    public enum DataTypeBackingLibrary
    {
        Custom = 0,
        Unity = 1,
        Numerics = 2
    }
}
