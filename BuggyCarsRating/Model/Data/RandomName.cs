using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace RandomNameGen
{
    /// <summary>
    /// RandomName class, used to generate a random name.
    /// </summary>
    public class RandomName
    {
        /// <summary>
        /// Class for holding the lists of names from names.json
        /// </summary>
        class NameList
        {
            public string[] first { get; set; }
            public string[] last { get; set; }

            public NameList()
            {
                first = new string[] { };
                last = new string[] { };
            }
        }

        Random rand;
        List<string> First;
        List<string> Last;

        /// <summary>
        /// Initialises a new instance of the RandomName class.
        /// </summary>
        /// <param name="rand">A Random that is used to pick names</param>
        public RandomName(Random rand)
        {
            this.rand = rand;
            NameList l = new NameList();

            JsonSerializer serializer = new JsonSerializer();

            using (StreamReader reader = new StreamReader("names.json"))
            using (JsonReader jreader = new JsonTextReader(reader))
            {
                l = serializer.Deserialize<NameList>(jreader);
            }

            First = new List<string>(l.first);
            Last = new List<string>(l.last);
        }

        public string GenerateFirstname()
        {
            string first = First[rand.Next(First.Count)];

            return first;
        }
        public string GenerateLastname()
        {
            string last = Last[rand.Next(Last.Count)];

            return last;
        }

    }

}
