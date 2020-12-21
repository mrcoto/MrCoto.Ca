namespace MrCoto.Ca.Application.Common.ClientDetection.Data
{
    public class Location
    {
       public string CountryName { get; set; }

       private string _regionName;
       public string RegionName
       {
           get => _regionName;
           set => _regionName = value.Replace(" Region", "");
       }

       public string CityName { get; set; }
       public double Latitude { get; set; }
       public double Longitude { get; set; }
    }
}