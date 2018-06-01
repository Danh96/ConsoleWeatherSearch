using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WeatherSearch_2
{
    public class Coord
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Main
    {
        public double temp { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
    }

    public class Wind
    {
        public double speed { get; set; }
        public int deg { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class Sys
    {
        public int type { get; set; }
        public int id { get; set; }
        public double message { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    public class RootObject
    {
        public Coord coord { get; set; }
        public List<Weather> weather { get; set; }
        public Main main { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public Sys sys { get; set; }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Please enter the city you would like to know the weather for.");
            Console.WriteLine();

            string Http = "http://api.openweathermap.org/data/2.5/weather?q=";
            string City = Console.ReadLine();
            string Unit = "&units=metric";
            string APIKey = "&APPID=311845e21ad1119ea1dfdc7666274f01";

            var URL = Http + City + Unit + APIKey;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream WeatherJson = response.GetResponseStream();
                StreamReader objReader = new StreamReader(WeatherJson);
                string WeatherInfo = objReader.ReadToEnd();

                var obj = JsonConvert.DeserializeObject<RootObject>(WeatherInfo);

                foreach (Weather i in obj.weather)
                {
                    Console.WriteLine();
                    Console.Write("Weather: " + i.main + " - ");
                    Console.Write(i.description);
                    Console.WriteLine();
                }

                Console.WriteLine("Temperature: " + obj.main.temp + " °C");
                Console.WriteLine("WindSpeed: " + obj.main.temp + " Mps");
            }
            catch
            {
                Console.WriteLine();
                Console.WriteLine("Search failed, please check that you have entered a valid City and have \nan internet connection.");
            }
        }
    }
}
