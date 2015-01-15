using System.IO;
using Newtonsoft.Json;

namespace GitStache
{
    public class SaveConfiguration
    {
        private const string FILENAME = "settings.json";

        public string CurrentRepositoryFilepath { get; set; }

        public void SaveToFile()
        {
            var json = JsonConvert.SerializeObject(this);
            File.WriteAllText(Directory.GetCurrentDirectory() + "\\" + FILENAME, json);
        }

        public SaveConfiguration ReadFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "\\" + FILENAME;
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                var configFromFile = JsonConvert.DeserializeObject<SaveConfiguration>(json);
                this.CurrentRepositoryFilepath = configFromFile.CurrentRepositoryFilepath;
            }

            return this;
        }
    }
}