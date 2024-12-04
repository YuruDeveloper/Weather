using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using System.Net;
using System.Net.Http;
using System.IO;
using Deedle;
using Newtonsoft;
using Newtonsoft.Json;


public class WeatherDust
{

    private const string ApiKey = "?serviceKey=" + "ApiKey";
    private const string GetTMUrl = "https://apis.data.go.kr/B552584/MsrstnInfoInqireSvc/getTMStdrCrdnt";
    private const string GetNearByMsrstnUrl = "https://apis.data.go.kr/B552584/MsrstnInfoInqireSvc/getNearbyMsrstnList";
    private const string GetDustDataUrl = "https://apis.data.go.kr/B552584/ArpltnInforInqireSvc/getMsrstnAcctoRltmMesureDnsty";
    private const string GetWeatherDataUrl = "http://apis.data.go.kr/1360000/VilageFcstInfoService_2.0/getUltraSrtNcst";
    private const string GetWeatherUrl = "https://apis.data.go.kr/1360000/VilageFcstInfoService_2.0/getVilageFcst";

    private int WeatherX, WeatherY;
    private List<string> DustLocationName = new List<string>();
    private string CsvPath;
    private static HttpClient Client;
    private WeatherDustData Data;
    public WeatherDust(string CsvPath)
    {
        this.CsvPath = CsvPath;
        Client = new HttpClient();
        Init();
        Data = new WeatherDustData();
    }

    private async void Init()
    {

        //Get Position
        var Position = await GetLoocation();
        double Latitude = Position.Coordinate.Latitude;
        double Longitude = Position.Coordinate.Longitude;

        int LatiudeHour = (int)Latitude;
        int LatiudeMinute = (int)((Latitude - (int)Latitude) * 100);
        int LongitudeHour = (int)Longitude;
        int LongitudeMinte = (int)((Longitude - (int)Longitude) * 100);

        //tranform
        var Data = Frame.ReadCsv(CsvPath);
        var SelectedData = Data.Columns[new[] { "3단계", "격자 X", "격자 Y", "경도(시)", "경도(분)", "위도(시)", "위도(분)" }];
        var Filtered = SelectedData.Rows.Where(SelectedData => (SelectedData.Value.GetAs<int>("경도(시)") == LongitudeHour & SelectedData.Value.GetAs<int>("위도(시)") == LatiudeHour));
        var Value = Frame.FromRows(Filtered);
        Filtered = Value.Rows.Where(Value => (Value.Value.GetAs<int>("경도(분)") <= LongitudeMinte + 25 && Value.Value.GetAs<int>("경도(분)") >= LongitudeMinte - 25
                                            && Value.Value.GetAs<int>("위도(분)") <= LatiudeMinute + 25 && Value.Value.GetAs<int>("위도(분)") >= LatiudeMinute - 25));
        Value = Frame.FromRows(Filtered);
        var ResultFirst = Frame.FromRows(Filtered).Rows.FirstValue();

        WeatherX = (int)ResultFirst["격자 X"];
        WeatherY = (int)ResultFirst["격자 Y"];
        string LocationName = ResultFirst["3단계"].ToString();

        //getData
        string Url = GetTMUrl + ApiKey + "&returnType=json" + "&umdName=" + LocationName;

        string Result = await GetResult(Url);
        TMXYItem tmxy = JsonConvert.DeserializeObject<Root<DustBody<TMXYItem>>>(Result).response.body.items[0];

        Url = GetNearByMsrstnUrl + ApiKey + "&returnType=json" + "&tmX=" + tmxy.tmX + "&tmY=" + tmxy.tmY;
        Result = await GetResult(Url);
        List<StationItem> Station = JsonConvert.DeserializeObject<Root<DustBody<StationItem>>>(Result).response.body.items;
        for(int i = 0;i < Station.Count; i++)
        {
            DustLocationName.Add(Station[i].stationName);
        }
    }

    private async Task<Geoposition> GetLoocation()
    {
        Geolocator geolocator = new Geolocator { DesiredAccuracy = PositionAccuracy.High };
        Geoposition position = await geolocator.GetGeopositionAsync();
        return position;
    }

    private async Task<string> GetResult(string Url)
    {
        HttpResponseMessage response = await Client.GetAsync(Url);
        response.EnsureSuccessStatusCode();
        string jsonResponse = await response.Content.ReadAsStringAsync();
        return jsonResponse;
    }

    private async void GetDustData()
    {
        string Url = string.Empty;
        string Result = string.Empty;
        DustItem Dust = new DustItem();
        for (int i = 0; i < DustLocationName.Count; i++)
        {
            Url = GetDustDataUrl + ApiKey + "&returnType=json" + "&dataTerm=DAILY" + "&stationName=" + DustLocationName[i];
            Result = await GetResult(Url);
            
            try
            {
                Dust = JsonConvert.DeserializeObject<Root<DustBody<DustItem>>>(Result).response.body.items[0];
                break;
            }
            catch (Exception ex) {
                continue;
            }
        }
        double DustDouble;
        DustDouble = double.Parse(Dust.pm10Value);
        if (DustDouble <= 50)
        {
            Data.Dust = DustEnum.Good;
        }
        else if (DustDouble <= 100)
        {
            Data.Dust = DustEnum.Nomal;
        }
        else if (DustDouble <= 250)
        {
            Data.Dust = DustEnum.Bad;
        }
        else
        {
            Data.Dust = DustEnum.VeryBad;
        }
    }

    private async void GetWeatherData()
    {
        string Url = string.Empty;
        string Result = string.Empty;
        DateTime CurrentTime = DateTime.Now;
        string Hour =(CurrentTime.Hour - 1) < 10 ? "0" : ""   + (CurrentTime.Hour - 1).ToString();
        Url = GetWeatherDataUrl + ApiKey + "&dataType=json" + "&nx=" + WeatherX + "&ny=" + WeatherY + "&base_date=" + CurrentTime.ToString("yyyyMMdd")+ "&base_time=" + Hour + "00";
        Result = await GetResult(Url);

        List<WeatherDataItem> weatherDataItems = JsonConvert.DeserializeObject<Root<WeatherDataBody>>(Result).response.body.items.item;
        for (int i = 0; i < weatherDataItems.Count; i++) {
            switch (weatherDataItems[i].category) {
                case "T1H":
                    Data.Temperaure = double.Parse(weatherDataItems[i].obsrValue);
                    break;
                case "REH":
                    Data.humidity = double.Parse(weatherDataItems[i].obsrValue);
                    break;
            }
        }
        Url = GetWeatherUrl + ApiKey + "&dataType=json" + "&nx=" + WeatherX + "&ny=" + WeatherY + "&base_date=" + CurrentTime.ToString("yyyyMMdd") + "&base_time=" + Hour + "00";
        Result = await GetResult(Url);
        List<WeatherSkyItem> weatherSkyItems = JsonConvert.DeserializeObject<Root<WeatherSkyBody>>(Result).response.body.items.item;
        for (int i = 0; i < weatherSkyItems.Count; i++) {
            switch (weatherSkyItems[i].category) {
                case "SKY":
                    if (!(Data.Weather == WeatherEnum.Raniny || Data.Weather == WeatherEnum.Snow))
                    {
                        int hour = DateTime.Now.Hour;
                        int j = int.Parse(weatherSkyItems[i].fcstValue);
                        switch (j)
                        {
                            case 1:
                                Data.Weather = (hour >= 18 || hour <= 6) ? WeatherEnum.Moon : WeatherEnum.Sunny;
                                break;
                            case 2:
                                Data.Weather = (hour >= 18 || hour <= 6) ? WeatherEnum.LiitleMoon : WeatherEnum.LittleClude;
                                break;
                            case 3:
                            case 4:
                                Data.Weather = WeatherEnum.RangeClude;
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case "PTY":
                    int k = int.Parse(weatherSkyItems[i].fcstValue);
                    switch (k)
                    {
                        case 1:
                        case 2:
                            Data.Weather = WeatherEnum.Raniny;
                            break;
                        case 3:
                        case 4:
                            Data.Weather = WeatherEnum.Snow;
                            break;
                        default:
                            break;
                    }
                    break;
            }
        }
    }
    public void GetData()
    {
        GetWeatherData();
        GetDustData();
    }

    public WeatherDustData GetWeahterDust()
    {
        return Data;
    }
}
