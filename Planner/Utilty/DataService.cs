using Newtonsoft.Json;
using Planner.Models;
using System.Collections.ObjectModel;
using System.IO;

namespace Planner.Utilty
{
    public class DataService
    {
        private readonly string PATH;
        public DataService(string PathToFile)
        {
            PATH = PathToFile;
        }
        public ObservableCollection<Folder> LoadData()
        {
            bool FileExists = File.Exists(PATH);
            if (!FileExists)
            {
                File.CreateText(PATH).Dispose();
                return new ObservableCollection<Folder>();
            }
            using(StreamReader reader = File.OpenText(PATH))
            {
                string Data = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<ObservableCollection<Folder>>(Data);
            }
        }
        public void SaveData(ObservableCollection<Folder> collection)
        {
            using (StreamWriter WriterThread = File.CreateText(PATH))
            {
                string TextToWrite = JsonConvert.SerializeObject(collection);
                WriterThread.Write(TextToWrite);
            }
        }

    }
}
