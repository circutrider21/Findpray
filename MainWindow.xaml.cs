using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Squirrel;
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
            Update();
        }
        static async Task Update() 
        {
            using (var mgr = UpdateManager.GitHubUpdateManager("https://github.com/circutrider21/Findpray"))
            {
                await mgr.Result.UpdateApp();
            }
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
            string real_minute = minutes == 0 ? "00" : minutes.ToString();
            string to_return = hours + ":" + real_minute + " " + ender;
            return to_return;
        }

        public void Submit_Click(object sender, RoutedEventArgs e)
        {
            string url = "http://api.aladhan.com/v1/timingsByCity?city=" + CityText.Text + "&country=" + CountryText.Text + "&method=2";
            string response = Fetch(url);
            JObject data = JObject.Parse(response);
            FajrText.Content = "Fajr: " + PMT((string)data["data"]["timings"]["Fajr"]);
            DhuhrText.Content = "Dhuhr: " + PMT((string)data["data"]["timings"]["Dhuhr"]);
            AsrText.Content = "Asr: " + PMT((string)data["data"]["timings"]["Asr"]);
            MaghribText.Content = "Maghrib: " + PMT((string)data["data"]["timings"]["Maghrib"]);
            IshaText.Content = "Isha: " + PMT((string)data["data"]["timings"]["Isha"]);
        }

    }
}
