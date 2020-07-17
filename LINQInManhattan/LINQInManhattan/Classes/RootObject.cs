using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LINQInManhattan.Classes
{
    public class Rootobject
    {
        public string Type { get; set; }
        public Feature[] Features { get; set; }

        /// <summary>
        /// Gets data into single object
        /// </summary>
        /// <param name="path">The path to the json file</param>
        /// <returns>Returns Rootobject object</returns>
        public static Rootobject ReadAll(string path)
        {
            string jsonFromFile;
            using (StreamReader reader = File.OpenText(path))
            {
                jsonFromFile = reader.ReadToEnd();
                var rootObj = JsonConvert.DeserializeObject<Rootobject>(jsonFromFile);
                return rootObj;
            }
        }
    }
}
