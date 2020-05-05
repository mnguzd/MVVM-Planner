using Newtonsoft.Json;
using Planner.Models;
using System.Collections.ObjectModel;
using System.IO;

namespace Planner.Utilty
{
    public class DataService
    {
        private readonly FileInfo _dataFile;

        public DataService()
            :this(new FileInfo("FolderData.txt"))
        {
        }

        public DataService(FileInfo sourceToFile)
        {
            _dataFile = sourceToFile;
        }

        public ObservableCollection<Folder> LoadData()
        {
            if (!_dataFile.Exists)
            {
                _dataFile.CreateText().Dispose();
                return new ObservableCollection<Folder>();
            }
            using(StreamReader reader = _dataFile.OpenText())
            {
                string Data = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<ObservableCollection<Folder>>(Data);
            }
        }

        public void SaveData(ObservableCollection<Folder> collection)
        {
            using (StreamWriter WriterThread = _dataFile.CreateText())
            {
                string TextToWrite = JsonConvert.SerializeObject(collection);
                WriterThread.Write(TextToWrite);
            }
        }

    }
}
