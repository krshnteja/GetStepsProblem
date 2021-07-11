using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace GetStepsProblem
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Starting the thing...");
            Dictionary<int, int> userData = readFiles();
            writeToCSV(userData);
            Console.WriteLine("Done, see 'C:/Users/home/Documents/JSON Files/OutFile.csv'");
        }

        private static void writeToCSV(Dictionary<int, int> userData)
        {

            try
            {
                String csv = "user,total_steps," + Environment.NewLine + String.Join(
                Environment.NewLine,
                userData.Select(d => $"{d.Key.ToString()},{d.Value.ToString()},"));

                System.IO.File.WriteAllText("C:/Users/home/Documents/JSON Files/OutFile.csv", csv);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"writeToCSV Exception: {ex.Message}");
            }
        }

        static Dictionary<int, int> readFiles()
        {
            List<UserData> userData = new List<UserData> { };
            Dictionary<int, int> myDict = new Dictionary<int, int>();

            try
            {
                foreach (string fileName in Directory.GetFiles("C:/Users/home/Documents/JSON Files", "*.json"))
                {
                    var jsonData = System.IO.File.ReadAllText(fileName);
                    userData = JsonConvert.DeserializeObject<List<UserData>>(jsonData).ToList();
                    foreach (UserData data in userData)
                    {
                        if (myDict.ContainsKey(data.user))
                        {
                            myDict[data.user] += data.steps;
                        }
                        else
                        {
                            myDict.Add(data.user, data.steps);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"readFiles Exception: {ex.Message}");
            }

            return myDict;
        }
    }
}
