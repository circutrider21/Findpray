using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Findpray
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public string Fetch(string url) 
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetStringAsync(new Uri(url)).Result;

                return response;
            }
        }

        public static String PMT(String to_convert) {
            string[] times = to_convert.Split(':');
            bool am = false;
            int hours = int.Parse(times[0]);
            int minutes = int.Parse(times[1]);
            if (hours > 12)
            {
                hours -= 12;
                am = false;
            }
            else {
                am = true;
            }
            string ender = am ? "AM" : "PM";
            string to_return = hours + ":" + minutes + " " + ender;
            return to_return;
        }

        public void Submit_Click(object sender, RoutedEventArgs e)
        {
            string url = "http://api.aladhan.com/v1/timingsByCity?city=" + CityText.Text + "&country=" + CountryText.Text + "&method=2";
            string response = Fetch(url);
            JObject data = JObject.Parse(response);
            FajrText.Content = (string)data["data"]["timings"]["fajr"];
            DhuhrText.Content = (string)data["data"]["timings"]["dhuhr"];
            AsrText.Content = (string)data["data"]["timings"]["asr"];
            MaghribText.Content = (string)data["data"]["timings"]["maghrib"];
            IshaText.Content = (string)data["data"]["timings"]["isha"];
        }

    }
}
