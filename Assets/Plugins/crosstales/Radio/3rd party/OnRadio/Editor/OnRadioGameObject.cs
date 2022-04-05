#if UNITY_EDITOR
using UnityEditor;
using Crosstales.Radio.EditorUtil;

namespace Crosstales.Radio.OnRadio.EditorExtension
{
   /// <summary>Editor component for for adding the prefabs from 'OnRadio' in the "Hierarchy"-menu.</summary>
   public static class OnRadioGameObject
   {
      [MenuItem("GameObject/" + Crosstales.Radio.Util.Constants.ASSET_NAME + "/3rd party/OnRadio/PlaylistService", false, EditorHelper.GO_ID + 20)]
      private static void AddPlaylistService()
      {
         EditorHelper.InstantiatePrefab("PlaylistService", $"{EditorConfig.ASSET_PATH}3rd party/OnRadio/Prefabs/");
      }

      [MenuItem("GameObject/" + Crosstales.Radio.Util.Constants.ASSET_NAME + "/3rd party/OnRadio/Reco2Service", false, EditorHelper.GO_ID + 21)]
      private static void AddReco2Service()
      {
         EditorHelper.InstantiatePrefab("Reco2Service", $"{EditorConfig.ASSET_PATH}3rd party/OnRadio/Prefabs/");
      }

      [MenuItem("GameObject/" + Crosstales.Radio.Util.Constants.ASSET_NAME + "/3rd party/OnRadio/TopsongsService", false, EditorHelper.GO_ID + 22)]
      private static void AddTopsongsService()
      {
         EditorHelper.InstantiatePrefab("TopsongsService", $"{EditorConfig.ASSET_PATH}3rd party/OnRadio/Prefabs/");
      }

      [MenuItem("GameObject/" + Crosstales.Radio.Util.Constants.ASSET_NAME + "/3rd party/OnRadio/RadioProviderOnRadio", false, EditorHelper.GO_ID + 23)]
      private static void AddRadioProviderOnRadio()
      {
         EditorHelper.InstantiatePrefab("RadioProviderOnRadio", $"{EditorConfig.ASSET_PATH}3rd party/OnRadio/Prefabs/");
      }
   }
}
#endif
// © 2020-2021 crosstales LLC (https://www.crosstales.com)