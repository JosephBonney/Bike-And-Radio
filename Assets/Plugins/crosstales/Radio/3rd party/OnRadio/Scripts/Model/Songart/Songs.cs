using System.Xml.Serialization;
using Crosstales.Radio.Util;

namespace Crosstales.Radio.OnRadio.Model.Songart
{
   /// <summary>Model of a song holder.</summary>
   [XmlRoot(ElementName = "songs")]
   [System.SerializableAttribute]
   public class Songs
   {
      [XmlElement(ElementName = "song")] public Song Song { get; set; }

      public override string ToString()
      {
         System.Text.StringBuilder result = new System.Text.StringBuilder();

         result.Append(GetType().Name);
         result.Append(Constants.TEXT_TOSTRING_START);

         result.Append("Song='");
         result.Append(Song);
         result.Append(Constants.TEXT_TOSTRING_DELIMITER_END);

         result.Append(Constants.TEXT_TOSTRING_END);

         return result.ToString();
      }
   }
}
// © 2019-2022 crosstales LLC (https://www.crosstales.com)