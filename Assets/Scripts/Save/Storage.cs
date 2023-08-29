using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace TestOverMobile.SaveSystem
{
    public sealed class Storage
    {
        private string _filePath;
        private BinaryFormatter _formatter;

        public Storage()
        {
            var directory = Application.persistentDataPath + "/saveble/";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            _filePath = directory + "SaveData.save";
            InitBinaryFormatter();
        }

        private void InitBinaryFormatter()
        {
            _formatter = new BinaryFormatter();
            var selector = new SurrogateSelector();

            var nameData = new CardsSerialization();

            selector.AddSurrogate(typeof(List<PlayerCard>), new StreamingContext(StreamingContextStates.All), nameData);

            _formatter.SurrogateSelector = selector;
        }

        public void Save(object saveData)
        {
            Debug.Log($"Storage Save");
            var file = File.Create(_filePath);
            _formatter.Serialize(file, saveData);
            file.Close();
        }

        public object Load(object saveData)
        {
            Debug.Log($"Storage Load");
            if (!File.Exists(_filePath))
            {
                if (saveData != null)
                    Save(saveData);
                return saveData;
            }

            var file = File.Open(_filePath, FileMode.Open);
            var savedData = _formatter.Deserialize(file); //1
            file.Close();
            return savedData;
        }
    }
}
