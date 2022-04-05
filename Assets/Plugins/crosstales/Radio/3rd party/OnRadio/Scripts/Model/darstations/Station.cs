using System.Xml.Serialization;
using Crosstales.Radio.Util;

namespace Crosstales.Radio.OnRadio.Model.DARStations
{
   /// <summary>Model of a station.</summary>
   [XmlRoot(ElementName = "station")]
   [System.SerializableAttribute]
   public class Station
   {
      [XmlElement(ElementName = "station_id")] public string Station_id { get; set; }
      [XmlElement(ElementName = "callsign")] public string Callsign { get; set; }
      [XmlElement(ElementName = "dial")] public string Dial { get; set; }
      [XmlElement(ElementName = "band")] public string Band { get; set; }
      [XmlElement(ElementName = "address1")] public string Address1 { get; set; }
      [XmlElement(ElementName = "address2")] public string Address2 { get; set; }
      [XmlElement(ElementName = "city")] public string City { get; set; }
      [XmlElement(ElementName = "state")] public string State { get; set; }
      [XmlElement(ElementName = "country")] public string Country { get; set; }
      [XmlElement(ElementName = "zipcode")] public string Zipcode { get; set; }
      [XmlElement(ElementName = "slogan")] public string Slogan { get; set; }
      [XmlElement(ElementName = "phone")] public string Phone { get; set; }
      [XmlElement(ElementName = "email")] public string Email { get; set; }
      [XmlElement(ElementName = "ubergenre")] public string Ubergenre { get; set; }
      [XmlElement(ElementName = "genre")] public string Genre { get; set; }
      [XmlElement(ElementName = "language")] public string Language { get; set; }
      [XmlElement(ElementName = "websiteurl")] public string Websiteurl { get; set; }
      [XmlElement(ElementName = "imageurl")] public string Imageurl { get; set; }
      [XmlElement(ElementName = "description")] public string Description { get; set; }
      [XmlElement(ElementName = "encoding")] public string Encoding { get; set; }
      [XmlElement(ElementName = "bitrate")] public string Bitrate { get; set; }
      [XmlElement(ElementName = "status")] public string Status { get; set; }

      public override string ToString()
      {
         System.Text.StringBuilder result = new System.Text.StringBuilder();

         result.Append(GetType().Name);
         result.Append(Constants.TEXT_TOSTRING_START);

         result.Append("Station_id='");
         result.Append(Station_id);
         result.Append(Constants.TEXT_TOSTRING_DELIMITER);

         result.Append("Callsign='");
         result.Append(Callsign);
         result.Append(Constants.TEXT_TOSTRING_DELIMITER);

         result.Append("Dial='");
         result.Append(Dial);
         result.Append(Constants.TEXT_TOSTRING_DELIMITER);

         result.Append("Band='");
         result.Append(Band);
         result.Append(Constants.TEXT_TOSTRING_DELIMITER);

         result.Append("Address1='");
         result.Append(Address1);
         result.Append(Constants.TEXT_TOSTRING_DELIMITER);

         result.Append("Address2='");
         result.Append(Address2);
         result.Append(Constants.TEXT_TOSTRING_DELIMITER);

         result.Append("City='");
         result.Append(City);
         result.Append(Constants.TEXT_TOSTRING_DELIMITER);

         result.Append("State='");
         result.Append(State);
         result.Append(Constants.TEXT_TOSTRING_DELIMITER);

         result.Append("Country='");
         result.Append(Country);
         result.Append(Constants.TEXT_TOSTRING_DELIMITER);

         result.Append("Country='");
         result.Append(Country);
         result.Append(Constants.TEXT_TOSTRING_DELIMITER);

         result.Append("Zipcode='");
         result.Append(Zipcode);
         result.Append(Constants.TEXT_TOSTRING_DELIMITER);

         result.Append("Slogan='");
         result.Append(Slogan);
         result.Append(Constants.TEXT_TOSTRING_DELIMITER);

         result.Append("Phone='");
         result.Append(Phone);
         result.Append(Constants.TEXT_TOSTRING_DELIMITER);

         result.Append("Email='");
         result.Append(Email);
         result.Append(Constants.TEXT_TOSTRING_DELIMITER);

         result.Append("Ubergenre='");
         result.Append(Ubergenre);
         result.Append(Constants.TEXT_TOSTRING_DELIMITER);

         result.Append("Genre='");
         result.Append(Genre);
         result.Append(Constants.TEXT_TOSTRING_DELIMITER);

         result.Append("Language='");
         result.Append(Language);
         result.Append(Constants.TEXT_TOSTRING_DELIMITER);

         result.Append("Websiteurl='");
         result.Append(Websiteurl);
         result.Append(Constants.TEXT_TOSTRING_DELIMITER);

         result.Append("Imageurl='");
         result.Append(Imageurl);
         result.Append(Constants.TEXT_TOSTRING_DELIMITER);

         result.Append("Description='");
         result.Append(Description);
         result.Append(Constants.TEXT_TOSTRING_DELIMITER);

         result.Append("Encoding='");
         result.Append(Encoding);
         result.Append(Constants.TEXT_TOSTRING_DELIMITER);

         result.Append("Bitrate='");
         result.Append(Bitrate);
         result.Append(Constants.TEXT_TOSTRING_DELIMITER);

         result.Append("Status='");
         result.Append(Status);
         result.Append(Constants.TEXT_TOSTRING_DELIMITER_END);

         result.Append(Constants.TEXT_TOSTRING_END);

         return result.ToString();
      }
   }
}
// © 2019-2022 crosstales LLC (https://www.crosstales.com)