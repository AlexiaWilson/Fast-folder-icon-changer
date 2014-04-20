using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Friendly_folder_icon_customization
{
    class ReferenceCounter
    {
        public event EventHandler<ReferenceEventArgs> ReferenceCountIsZero;

        private Dictionary<string, int> _references;
        private string _referenceDataFile;

        public ReferenceCounter(string appData)
        {
            _references = new Dictionary<string, int>();
            _referenceDataFile = appData + "data.dat";

            LoadData();
        }

        // Reads a file and loads it into the dictionary class
        private void LoadData()
        {
            if (File.Exists(_referenceDataFile))
            {
                var fileData = String.Join("", File.ReadAllLines(_referenceDataFile));

                foreach (var entry in Regex.Matches(fileData, @"(\w|\d)+:\d+"))
                {
                    var tokens = entry.ToString().Split(':');
                    var Key = tokens[0];
                    var Value = int.Parse(tokens[1]);
                    _references[Key] = Value;
                }
            }
            else
            {
                File.Create(_referenceDataFile);
            }
        }

        // Saves the dictionary class to a file
        private void SaveData()
        {
            var data = new List<string>();

            foreach (var pair in _references)
            {
                data.Add(String.Format("{0}:{1},", pair.Key, pair.Value));
            }

            File.WriteAllLines(_referenceDataFile, data);
        }

        public void Increment(string fileName)
        {
            var key = _hashString(fileName);
            if (_references.ContainsKey(key))
            {
                _references[key] += 1;
            }
            else
            {
                _references[key] = 1;
            }

            SaveData();
        }

        public void Decrement(string fileName)
        {
            var key = _hashString(fileName);
            if (_references.ContainsKey(key))
            {
                _references[key] -= 1;
                if (_references[key] <= 0)
                {
                    _raiseRefEvent(fileName);
                    _references.Remove(key);
                }
            }

            SaveData();
        }

        private string _hashString(string input)
        {
            using (var md5 = MD5.Create())
            {
                return BitConverter.ToString(md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(input))).Replace("-", "");
            }
        }

        private void _raiseRefEvent(string fileName)
        {
            var e = new ReferenceEventArgs { fileName = fileName };
            EventHandler<ReferenceEventArgs> handler = ReferenceCountIsZero;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }

    public class ReferenceEventArgs : EventArgs
    {
        public string fileName { get; set; }
    }
}
