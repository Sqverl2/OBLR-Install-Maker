using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Windows;

namespace OBLRInstall.Data
{
    [JsonObject(MemberSerialization.OptIn)]
    internal class InstallationStep : DependencyObject
    {
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string FileName { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Description { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string DestinationFolder { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string SuggestedSourcePath { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Include { get; set; } = new List<string>();

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Ignore { get; set; } = new List<string>();

        [JsonProperty]
        [JsonConverter(typeof(StringEnumConverter))]
        public StepType Operation { get; set; }

        public static DependencyProperty CurrentStateProperty = DependencyProperty.Register("CurrentState", typeof(State), typeof(InstallationStep), new PropertyMetadata(State.MISSING));

        public State CurrentState
        {
            get => (State)GetValue(CurrentStateProperty);
            set => SetValue(CurrentStateProperty, value);
        }

        public enum State
        {
            MISSING,
            WRONG_LOCATION,
            READY,
            COMPLETED,
            FAILED,
        }
    }
}
