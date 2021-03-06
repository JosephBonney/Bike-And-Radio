using UnityEngine;

namespace Crosstales.Radio.OnRadio.Demo
{
   /// <summary>Shows the details for OnRadio.</summary>
   [HelpURL("https://www.crosstales.com/media/data/assets/radio/api/class_crosstales_1_1_radio_1_1_on_radio_1_1_demo_1_1_show_more.html")]
   public class ShowMore : MonoBehaviour
   {
      #region Public methods

      public void Show()
      {
         Crosstales.Common.Util.NetworkHelper.OpenURL(Crosstales.Radio.OnRadio.Util.Constants.ONRADIO_URL);
      }

      #endregion
   }
}
// © 2020-2022 crosstales LLC (https://www.crosstales.com)