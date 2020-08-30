using Newtonsoft.Json;
using SeparatedTextFileReader.Application.Common.Interfaces;
using SeparatedTextFileReader.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeparatedTextFileReader.Infrastructure.DataHelpers
{
    public class DeserializeRowData<E> : IDeserializeRowData<E>
          where E : class, IEntity

    {

        private JsonSerializerSettings _settings;



        public DeserializeRowData()
        {
            this._settings = new JsonSerializerSettings();
            this._settings.Formatting = Formatting.Indented;


        }






        public E Deserialize(Dictionary<int, string> properties,
            Dictionary<int, string> values, 
            Dictionary<string, string> propertyMapping)
        {
            this._settings.ContractResolver = new ContractResolver(propertyMapping);

            Dictionary<string, string> propsValuesDict = new Dictionary<string, string>();

            foreach (KeyValuePair<int, string> entry in properties)
            {

                if (!propsValuesDict.ContainsKey(entry.Value))
                    propsValuesDict.Add(entry.Value, values[entry.Key]);

            }



            string serializedPropsValue = JsonConvert.SerializeObject(propsValuesDict);
            var deSerializedObject = JsonConvert.DeserializeObject<E>(serializedPropsValue, this._settings);

            return deSerializedObject;
        }

    }
}
