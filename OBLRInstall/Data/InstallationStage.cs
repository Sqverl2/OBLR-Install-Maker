using Newtonsoft.Json;
using System.Collections.Generic;

namespace OBLRInstall.Data
{
    [JsonObject(MemberSerialization.OptIn)]
    class InstallationStage
    {
        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty]
        public List<InstallationStep> Steps { get; set; }
    }
}
