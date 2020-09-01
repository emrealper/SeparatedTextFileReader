using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace SeparatedTextFileReader.Infrastructure.DataHelpers
{
    public class ContractResolver : DefaultContractResolver
    {
        private Dictionary<string, string> PropertyMappings { get; set; }

        public ContractResolver(Dictionary<string, string> mappings)
        {
            PropertyMappings = mappings;
        }

        protected override string ResolvePropertyName(string propertyName)
        {
            var resolved = PropertyMappings.TryGetValue(propertyName, out var resolvedName);
            return resolved ? resolvedName : base.ResolvePropertyName(propertyName);
        }
    }
}