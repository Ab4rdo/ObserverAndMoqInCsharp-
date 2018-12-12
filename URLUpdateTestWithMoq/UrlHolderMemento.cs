using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace URLUpdateTestWithMoq
{
    [Serializable]
    public class UrlHolderMemento
    {
        public static readonly string ExportFileName = "UrlHolder.memento";

        private Dictionary<string, DateTime> _urlsWithDates;

        /// <summary>
        ///     Constructs a memento from arguments given to constructor.
        /// </summary>
        /// <param name="dictionary"></param>
        public UrlHolderMemento(Dictionary<string, DateTime> dictionary)
        {
            UrlsWithDates = dictionary;
        }

        /// <summary>
        ///     Constructs a Memento from a given file.
        /// </summary>
        /// <param name="fileName"></param>
        public UrlHolderMemento(string fileName)
        {
            ReadStateFromFile(fileName);
        }

        public Dictionary<string, DateTime> UrlsWithDates
        {
            get => _urlsWithDates;
            set
            {
                _urlsWithDates = new Dictionary<string, DateTime>(value);
                WriteStateToFile(ExportFileName);
            }
        }

        private void WriteStateToFile(string fileName)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(fileName,
                FileMode.Create,
                FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, this);
            stream.Close();
        }

        private void ReadStateFromFile(string fileName)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(fileName,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read);
            var restoredUrlHolder = (UrlHolderMemento) formatter.Deserialize(stream);
            stream.Close();
            _urlsWithDates = restoredUrlHolder._urlsWithDates;
        }
    }
}