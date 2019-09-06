using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
namespace Gdp_problem
{
    public class Program
    {


        private static string convertJsonPath = "../../../../data/convertcsv.json";
        private static string outputPath = "../../../../../actual_output.json";



        private static Dictionary<string, string> Dict()
        {
            var dict = new Dictionary<string, string>();
            if(File.Exists(convertJsonPath))
            {
                using (StreamReader file = File.OpenText(convertJsonPath))
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JToken o2 = JToken.ReadFrom(reader);

                    foreach(var i in o2)
                    {
                        dict.Add((string)i["Country"], (string)i["Continent"]);
                    }

                }
            }

          

            return dict;
        }
        public static string FileNot()
        {
            return "not found the file";
        }

        public static void GdpSol()
        {
            List<string[]> s = new List<string[]>();
            using (var r = new StreamReader("../../../../data/datafile.csv"))
            {

                while (!r.EndOfStream)
                {
                    string line = r.ReadLine();
                    var res = (line.Replace("\"", "")).Split(',');
                    s.Add(res);
                    /*Console.WriteLine(res);*/
                }

              /*  foreach (var items in s)s
                {

                    Console.WriteLine($"{items[0]}  {items[4]} {items[7]}");

                }*/
                r.Close();
                var abc = Dict();
               // Console.WriteLine(abc);
                var result = new Dictionary<string, Dictionary<string,decimal>> ();
                foreach(var x in s)
                {
                    
                   if(abc.ContainsKey(x[0]))
                    {
                        string continent = abc[x[0]];
                        if (result.ContainsKey(continent))
                        {
                            result[continent]["POPULATION_2012"] += Decimal.Parse(x[4]);
                            result[continent]["GDP_2012"] += Decimal.Parse(x[7]);

                        } else
                        {
                            result[continent] = new Dictionary<string, decimal>();
                            result[continent].Add("GDP_2012", Decimal.Parse(x[7]));
                            result[continent].Add("POPULATION_2012", Decimal.Parse(x[4]));
                        }
                    } else
                    {
                    }
                    
                }

               /* foreach(var items in result)
                {
                    Console.WriteLine($"{items.Key}");
                    foreach(var item in items.Value)
                    {
                        Console.WriteLine($"{item.Value} {item.Key}");
                    }
                }*/

                File.WriteAllText(outputPath,
                        JsonConvert.SerializeObject(result));
            }


        }


        static void Main(string[] args)
        {
            GdpSol();
            //Console.WriteLine(Directory.GetCurrentDirectory());
        }
    }
}
