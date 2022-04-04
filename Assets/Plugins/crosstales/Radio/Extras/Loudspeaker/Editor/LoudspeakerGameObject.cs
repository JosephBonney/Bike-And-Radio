#if UNITY_EDITOR
using UnityEditor;
using Crosstales.Radio.EditorUtil;

namespace Crosstales.Radio.EditorIntegration
{
   /// <summary>Editor component for the "Hierarchy"-menu.</summary>
   public static class LoudspeakerGameObject
   {
      [MenuItem("GameObject/" + Crosstales.Radio.Util.Constants.ASSET_NAME + "/Loudspeaker", false, EditorHelper.GO_ID + 8)]
      private static void AddLoudspeaker()
      {
         EditorHelper.InstantiatePrefab("Loudspeaker", $"{EditorConfig.ASSET_PATH}Extras/Loudspeaker/Resources/Prefabs/");
      }
   }
}
#endif
// © 2021-2022 crosstales LLC (https://www.crosstales.com)