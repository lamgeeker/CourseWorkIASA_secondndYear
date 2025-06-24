using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json;
using Planes;

namespace FileManager
{

    public static class PlaneSerializer
    {
        private static string filePath = @"C:\\Users\\Dream Machines\\Documents\\mCompany.txt";

        public static void Save(HashSet<Plane> newPlanes)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                IncludeFields = true
            };

            HashSet<Plane> existingPlanes = Load();

            foreach (var plane in newPlanes)
            {
                existingPlanes.Add(plane); // HashSet автоматично уникає дублікатів
            }

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                string json = JsonSerializer.Serialize(existingPlanes, options);
                writer.WriteLine(json);
            }
        }

        public static HashSet<Plane> Load()
        {
            if (!File.Exists(filePath))
                return new HashSet<Plane>();
            string json = File.ReadAllText(filePath);
            var options = new JsonSerializerOptions
            {
                IncludeFields = true
            };
            return JsonSerializer.Deserialize<HashSet<Plane>>(json, options) ?? new HashSet<Plane>();
        }
    }
}