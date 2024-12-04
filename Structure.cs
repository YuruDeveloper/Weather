using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Root<T>{
    public Response<T> response { get; set; }
}


public class Response<T>
{
    public Header header { get; set; }
    public T body { get; set; }
}
public class Header
{
    public string resultCode { get; set; }
    public string resultMsg { get; set; }
}


public class DustBody<T>
{
    public int totalCount { get; set; }
    public List<T> items { get; set; }
    public int pageNo { get; set; }
    public int numOfRows { get; set; }
}

public class WeatherSkyBody {
    public string dataType { get; set; }
    public WeatherSkyItems items { get; set; }
}
public class WeatherDataBody {
    public string dataType { get; set; }
    public WeatherDataItems items { get; set; }
    public int pageNo { get; set; }
    public int numOfRows { get; set; }
    public int totalCount { get; set; }
}

public class TMXYItem
{
    public string sggName { get; set; }
    public string umdName { get; set; }
    public string tmX { get; set; }
    public string tmY { get; set; }
    public string sidoName { get; set; }
}
public class StationItem
{
    public string stationCode { get; set; }
    public double tm { get; set; }
    public string addr { get; set; }
    public string stationName { get; set; }
}
public class DustItem
{
    public string dataTime { get; set; }
    public string so2Grade { get; set; }
    public string coFlag { get; set; }
    public string khaiValue { get; set; }
    public string so2Value { get; set; }
    public string coValue { get; set; }
    public string pm10Flag { get; set; }
    public string pm10Value { get; set; }
    public string o3Grade { get; set; }
    public string khaiGrade { get; set; }
    public string no2Flag { get; set; }
    public string no2Grade { get; set; }
    public string o3Flag { get; set; }
}
public class WeatherDataItems {
    public List<WeatherDataItem> item;
}
public class WeatherSkyItems
{
    public List<WeatherSkyItem> item;
}
public class WeatherDataItem
{
    public string baseDate { get; set; }
    public string baseTime { get; set; }
    public string category { get; set; }
    public string nx { get; set; }
    public string ny { get; set; }
    public string obsrValue { get; set; }
}
    public class WeatherSkyItem
{
    public string baseDate { get; set; }
    public string baseTime { get; set; }
    public string category { get; set; }
    public string fcstDate { get; set; }
    public string fcstTime { get; set; }
    public string fcstValue { get; set; }
    public int nx { get; set; }
    public int ny { get; set; }
}

public class WeatherDustData { 
    public WeatherEnum Weather {  get; set; }
    public DustEnum Dust { get; set; }

    public double Temperaure { get; set; }
    public double humidity { get; set; }
}

public enum WeatherEnum { 
    Sunny , LittleClude , RangeClude , Raniny, Snow , Moon , LiitleMoon
}
public enum DustEnum { 
    VeryBad, Bad , Nomal , Good
}