using System;
using System.Collections.Generic;
using System.IO;

namespace Utils
{
    public class MapeoItemsReader
    {
        public Dictionary<string, string> Read(string filePath)
        {
            string line;
            string[] aux;
            Dictionary<string, string> mapeoItemsValues = new Dictionary<string, string>();

            if (!File.Exists(filePath))
            {
                return null;
            }
            
            // Read the file and display it line by line.
            var file = new StreamReader(filePath);
            while ((line = file.ReadLine()) != null)
            {
                if (line[0] != '#')
                {
                    aux = line.Split('=');
                    mapeoItemsValues.Add(aux[0], aux[1]);
                }
            }
            file.Close();

            return mapeoItemsValues;
        }
    }
}
