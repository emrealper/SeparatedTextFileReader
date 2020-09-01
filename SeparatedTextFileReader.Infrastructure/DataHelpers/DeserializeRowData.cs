using System.Collections.Generic;
using Newtonsoft.Json;
using SeparatedTextFileReader.Application.Common.Interfaces;
using SeparatedTextFileReader.Domain.Common;

namespace SeparatedTextFileReader.Infrastructure.DataHelpers
{
    public class DeserializeRowData<TE> : IDeserializeRowData<TE>
        where TE : class, IEntity

    {
        private readonly JsonSerializerSettings _settings;


        public DeserializeRowData()
        {
            _settings = new JsonSerializerSettings();
            _settings.Formatting = Formatting.Indented;
        }


        public TE Deserialize(Dictionary<int, string> properties,
            Dictionary<int, string> values,
            Dictionary<string, string> propertyMapping)
        {
            _settings.ContractResolver = new ContractResolver(propertyMapping);

            var propsValuesDict = new Dictionary<string, string>();

            foreach (var entry in properties)
                if (!propsValuesDict.ContainsKey(entry.Value))
                    propsValuesDict.Add(entry.Value, values[entry.Key]);


            var serializedPropsValue = JsonConvert.SerializeObject(propsValuesDict);
            var deSerializedObject = JsonConvert.DeserializeObject<TE>(serializedPropsValue, _settings);

            return deSerializedObject;
        }
    }
}