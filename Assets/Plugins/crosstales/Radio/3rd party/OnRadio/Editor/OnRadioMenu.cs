#if UNITY_EDITOR
using UnityEditor;
using Crosstales.Radio.EditorUtil;

namespace Crosstales.Radio.OnRadio.EditorExtension
{
   /// <summary>Editor component for for adding the prefabs from 'OnRadio' in the "Tools"-menu.</summary>
   public static class OnRadioMenu
   {
      [MenuItem("Tools/" + Crosstales.Radio.Util.Constants.ASSET_NAME + "/Prefabs/3rd party/OnRadio/PlaylistService", false, EditorHelper.MENU_ID + 200)]
      private static void AddPlaylistService()
      {
         EditorHelper.InstantiatePrefab("PlaylistService", $"{EditorConfig.ASSET_PATH}3rd party/OnRadio/Prefabs/");
      }

      [MenuItem("Tools/" + Crosstales.Radio.Util.Constants.ASSET_NAME + "/Prefabs/3rd party/OnRadio/Reco2Service", false, EditorHelper.MENU_ID + 210)]
      private static void AddReco2Service()
      {
         EditorHelper.InstantiatePrefab("Reco2Service", $"{EditorConfig.ASSET_PATH}3rd party/OnRadio/Prefabs/");
      }

      [MenuItem("Tools/" + Crosstales.Radio.Util.Constants.ASSET_NAME + "/Prefabs/3rd party/OnRadio/TopsongsService", false, EditorHelper.MENU_ID + 220)]
      private static void AddTopsongsService()
      {
         EditorHelper.InstantiatePrefab("TopsongsService", $"{EditorConfig.ASSET_PATH}3rd party/OnRadio/Prefabs/");
      }

      [MenuItem("Tools/" + Crosstales.Radio.Util.Constants.ASSET_NAME + "/Prefabs/3rd party/OnRadio/RadioProviderOnRadio", false, EditorHelper.MENU_ID + 240)]
      private static void AddRadioProviderOnRadio()
      {
         EditorHelper.InstantiatePrefab("RadioProviderOnRadio", $"{EditorConfig.ASSET_PATH}3rd party/OnRadio/Prefabs/");
      }
   }
}
#endif
// © 2020-2022 crosstales LLC (https://www.crosstales.com)