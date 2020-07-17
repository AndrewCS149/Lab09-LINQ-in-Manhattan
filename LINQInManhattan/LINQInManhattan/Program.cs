using System;
using LINQInManhattan.Classes;
using static LINQInManhattan.Classes.Rootobject;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace LINQInManhattan
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "C:\\Users\\andre\\Desktop\\code\\codeFellows\\code401\\labs\\Lab09-LINQ-in-Manhattan\\LINQInManhattan\\data.json";

            Rootobject rootObj = ReadAll(path);
            Feature[] features = rootObj.Features;
            List<Properties> props = new List<Properties>();
            List<Geometry> geo = new List<Geometry>();

            GenerateGeometry(features, geo);
            GenerateProps(features, props);

            // QUERY 1
            var query1 = from hood in props select hood.Neighborhood;
            PrintQuery(query1);

            // QUERY 2
            var query2 = from hood in query1
                         where !hood.Equals(string.Empty)
                         select hood;
            PrintQuery(query2);

            // QUERY 3 
            var query3 = (from hood in query2
                          select hood).Distinct();
            PrintQuery(query3);

            // Query 4
            var query4 = (from hood in props
                          where !hood.Equals(string.Empty)
                          select hood.Neighborhood).Distinct();

            // QUERY 5
            var query5 = props.Select(x => new { x.Neighborhood });
            int totalHoods = 0;
            foreach (var item in query5)
            {
                Console.WriteLine(item.Neighborhood);
                totalHoods++;
            }
            Console.WriteLine($"Total: {totalHoods}");
        }

        /// <summary>
        /// Print out query data
        /// </summary>
        /// <param name="query">The query to print out</param>
        static void PrintQuery(IEnumerable<string> query)
        {
            int count = 0;
            foreach (var item in query)
            {
                Console.WriteLine(item);
                count++;
            }
            Console.WriteLine($"Total: {count}");
            Console.WriteLine("\n===========================================\n");
        }

        /// <summary>
        /// Generates a property object for each feature item in the json data
        /// </summary>
        /// <param name="features">The feature array to iterate through</param>
        /// <param name="props">The properties list to add to</param>
        static void GenerateProps(Feature[] features, List<Properties> props)
        {
            foreach (Feature item in features)
            {
                Properties prop = new Properties()
                {
                    Zip = item.properties.Zip,
                    City = item.properties.City,
                    State = item.properties.State,
                    Address = item.properties.Address,
                    Borough = item.properties.Borough,
                    Neighborhood = item.properties.Neighborhood,
                    County = item.properties.County
                };

                props.Add(prop);
            }
        }

        /// <summary>
        /// Generates a geometry object for each feature item in the json data
        /// </summary>
        /// <param name="features">The feature array to iterate through</param>
        /// <param name="props">The geometry list to add to</param>
        static void GenerateGeometry(Feature[] features, List<Geometry> geo)
        {
            foreach (Feature item in features)
            {
                Geometry geoData = new Geometry()
                {
                    Coordinates = item.geometry.Coordinates,
                    Type = item.geometry.Type
                };

                geo.Add(geoData);
            }
        }
    }
}
