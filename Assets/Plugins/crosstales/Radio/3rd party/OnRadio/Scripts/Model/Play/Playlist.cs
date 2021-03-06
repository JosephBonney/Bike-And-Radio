using System.Xml.Serialization;
using System.Collections.Generic;
using Crosstales.Radio.Util;

namespace Crosstales.Radio.OnRadio.Model.Play
{
   /// <summary>Model of a station holder.</summary>
   [XmlRoot(ElementName = "playlist")]
   [System.SerializableAttribute]
   public class Playlist
   {
      [XmlElement(ElementName = "station")] public List<Station> Station { get; set; }

      public override string ToString()
      {
         System.Text.StringBuilder result = new System.Text.StringBuilder();

         result.Append(GetType().Name);
         result.Append(Constants.TEXT_TOSTRING_START);

         result.Append("Station='");
         result.Append(Station.CTDump());
         result.Append(Constants.TEXT_TOSTRING_DELIMITER_END);

         result.Append(Constants.TEXT_TOSTRING_END);

         return result.ToString();
      }
   }
}
// © 2019-2022 crosstales LLC (https://www.crosstales.com)