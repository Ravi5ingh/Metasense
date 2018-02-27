using Encog.Neural.Networks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metasense.Infrastructure.Tabular;
using Metasense.Tabular;

namespace Metasense
{
    /// <summary>
    /// The static store of objects
    /// </summary>
    public static class ObjectStore
    {
        /// <summary>
        /// The internal object store
        /// </summary>
        private static Dictionary<string, object> store = new Dictionary<string, object>();

        /// <summary>
        /// The mapping that tell you what the name should be suffixed with for all recognized objects
        /// </summary>
        private static Dictionary<object, string> objectSuffixMappings = new Dictionary<object, string>
        {
            {typeof(BasicNetwork), "NNT"},
            {typeof(Table), "TBL"},
            {typeof(TimeSeries), "TSR"},
            {typeof(SQLConnection), "SQL"}
        };


        /// <summary>
        /// Add an item to the object store keyed on the given name (A object type specific suffix will be added)
        /// </summary>
        /// <param name="name">The key</param>
        /// <param name="obj">The object</param>
        /// <returns></returns>
        public static string Add(string name, object obj)
        {
            var suffix = string.Empty;
            if (!objectSuffixMappings.TryGetValue(obj.GetType(), out suffix))
            {
                suffix = "OBJ";
            }
            var retVal = name + "::" + suffix + ";" + GetTimeStamp();
            if (store.ContainsKey(retVal))
            {
                store.Remove(retVal);
            }
            store.Add(retVal, obj);
            return retVal;
        }

        /// <summary>
        /// Get an object from the object store
        /// </summary>
        /// <param name="name">The key name</param>
        /// <returns>The object</returns>
        public static object Get(string name)
        {
            return store[name];
        }

        private static string GetTimeStamp()
        {
            var now = DateTime.Now;
            return $"{now.Hour}:{now.Minute}:{now.Second}";
        }
    }
}
