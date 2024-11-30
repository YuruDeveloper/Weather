﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Root<T>
{
    public Response<T> response { get; set; }
}


public class Response<T> 
{
    public T body { get; set; }
    public Header header { get; set; }
}

public class Header
{
    public string resultMsg { get; set; }
    public string resultCode { get; set; }
}


public class DustBody<T>
{
    public int totalCount { get; set; }
    public List<T> items { get; set; }
    public int pageNo { get; set; }
    public int numOfRows { get; set; }
}

public class WeatherBody {
    public string dataType { get; set; }
    public WeatherItems items { get; set; }
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
public class WeatherItems
{
    public List<WeatherItem> item { get; set; }
}
public class WeatherItem
{
    public string baseDate { get; set; }
    public string baseTime { get; set; }
    public string category { get; set; }
    public int nx { get; set; }
    public  int ny { get; set; }
    public string obsrValue { get; set; }
}