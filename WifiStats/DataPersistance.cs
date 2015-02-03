using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace WifiStats
{
    public class DataPersistance
    {
        private static DataPersistance _instance = null;
        public static DataPersistance Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new DataPersistance(); 
                }

                return _instance;
            }
        }

        private DataPersistance()
        {
        }

        /**
         * This method allows to serialize to an xml file a scan
         * with as a title the number of milliseconds since epoch
         * */
        public void SaveScan(Scan s)
        {
            DataContractSerializer ds = new DataContractSerializer(typeof(Scan));

            if(!Directory.Exists("scans"))
            {
                Directory.CreateDirectory("scans");
            }

            using (Stream stream = File.Create("scans\\"+(long)(s.Date.ToUniversalTime().Subtract(
                new DateTime(1, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                ).TotalMilliseconds) + ".xml"))
            {
                ds.WriteObject(stream, s);
            }

            Console.WriteLine("Sauvegarde terminée.");
        }

        public Scan LoadScan(String path)
        {
            
            DataContractSerializer ds = new DataContractSerializer(typeof(Scan));
            Scan scan;

            using (Stream s = File.OpenRead(path))
            {
                scan = (Scan)ds.ReadObject(s);
            }

            return scan;
        }

        public Dictionary<DateTime, String> ListScans
        {
            get
            {
                Dictionary<DateTime, String> _listScans = new Dictionary<DateTime, string>();
                foreach (string s in Directory.GetFiles("scans"))
                {
                    _listScans.Add(GetDateFromFileName(s), s);
                }
                
                return _listScans;
            }
        }

        /**
         * Allows to return a DateTime from our file names
         * */
        private DateTime GetDateFromFileName(string s)
        {
            string date = s.Split('.')[0];
            //date == "scans\\1422435777594"
            string msString = date.Split('\\')[1];
            long ms = Convert.ToInt64(msString) * 10000;

            return new DateTime(ms);
        }

    }
}
