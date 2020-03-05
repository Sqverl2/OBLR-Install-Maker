using Newtonsoft.Json;
using System.Collections.Generic;

namespace OBLRInstall.Data
{
    [JsonObject(MemberSerialization.OptIn)]
    class PackagesConfiguration
    {
        [JsonProperty]
        public string Version { get; set; }

        [JsonProperty]
        public List<InstallationStage> Stages { get; set; }
    }
}
