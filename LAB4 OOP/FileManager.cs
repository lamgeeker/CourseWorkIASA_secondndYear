
using System.Text.Json;
using Planes;

namespace FileManager
{

    public static class PlaneSerializer
    {
        private static string filePath = @"C:\\Users\\Dream Machines\\Documents\\mCompany.txt";

        public static void Save(HashSet<Plane> planes)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                IncludeFields = true
            };
            using (StreamWriter rw = new StreamWriter(filePath))
            {
                string json = JsonSerializer.Serialize(planes, options);
                rw.WriteLine(json);
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