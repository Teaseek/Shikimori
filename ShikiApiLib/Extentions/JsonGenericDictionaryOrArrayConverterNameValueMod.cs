using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Newtonsoft.Json;

namespace ShikiApiLib
{
    internal class JsonGenericDictionaryOrArrayConverterNameValueMod : JsonConverter
    {
        // поскольку у нас в JSON'е не key/value, а name/value, нужен специальный класс
        // для десериализации одного элемента массива
        class NameValuePair<N, V>
        {
            public N name { get; set; }
            public V value { get; set; }
        }

        public override bool CanConvert(Type objectType)
        {
            return GetDictionaryKeyValueTypes(objectType).Count() == 1;
        }

        public override bool CanWrite { get { return false; } }

        object ReadJsonGeneric<TKey, TValue>(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var tokenType = reader.TokenType;

            var dict = existingValue as IDictionary<TKey, TValue>;
            if (dict == null)
            {
                var contract = serializer.ContractResolver.ResolveContract(objectType);
                dict = (IDictionary<TKey, TValue>)contract.DefaultCreator();
            }

            if (tokenType == JsonToken.StartArray)
            {
                var pairs = new JsonSerializer().Deserialize<NameValuePair<TKey, TValue>[]>(reader);
                if (pairs == null)
                {
                    return existingValue;
                }
                foreach (var pair in pairs)
                {
                    dict.Add(pair.name, pair.value);
                }
            }
            else if (tokenType == JsonToken.StartObject)
            {
                // Using "Populate()" avoids infinite recursion.
                // https://github.com/JamesNK/Newtonsoft.Json/blob/ee170dc5510bb3ffd35fc1b0d986f34e33c51ab9/Src/Newtonsoft.Json/Converters/CustomCreationConverter.cs
                serializer.Populate(reader, dict);
            }
            return dict;
        }

        public override object ReadJson(
                JsonReader reader, Type objectType, object existingValue,
                JsonSerializer serializer)
        {
            // Throws an exception if not exactly one.
            var keyValueTypes = GetDictionaryKeyValueTypes(objectType).Single();

            var method = GetType().GetTypeInfo().GetDeclaredMethod("ReadJsonGeneric");
            var genericMethod = method.MakeGenericMethod(new[] { keyValueTypes.Key, keyValueTypes.Value });
            return genericMethod.Invoke(this, new object[] { reader, objectType, existingValue, serializer });
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }

        static IEnumerable<KeyValuePair<Type, Type>> GetDictionaryKeyValueTypes(Type type)
        {
            foreach (Type intType in GetInterfacesAndSelf(type))
            {
                if (intType.GetTypeInfo().IsGenericType && intType.GetGenericTypeDefinition() == typeof(IDictionary<,>))
                {
                    var args = intType.GetTypeInfo().GenericTypeArguments;
                    if (args.Length == 2)
                    {
                        yield return new KeyValuePair<Type, Type>(args[0], args[1]);
                    }
                }
            }
        }

        static IEnumerable<Type> GetInterfacesAndSelf(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException();
            }
            if (type.GetTypeInfo().IsInterface)
            {
                return new[] { type }.Concat(type.GetTypeInfo().ImplementedInterfaces);
            }
            else
            {
                return type.GetTypeInfo().ImplementedInterfaces;
            }
        }
    }
}