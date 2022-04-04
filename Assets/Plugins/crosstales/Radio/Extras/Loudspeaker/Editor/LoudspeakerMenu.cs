#if UNITY_EDITOR
using UnityEditor;
using Crosstales.Radio.EditorUtil;

namespace Crosstales.Radio.EditorIntegration
{
   /// <summary>Editor component for the "Tools"-menu.</summary>
   public static class LoudspeakerMenu
   {
      [MenuItem("Tools/" + Crosstales.Radio.Util.Constants.ASSET_NAME + "/Prefabs/Loudspeaker", false, EditorHelper.MENU_ID + 150)]
      private static void AddLoudspeaker()
      {
         EditorHelper.InstantiatePrefab("Loudspeaker", $"{EditorConfig.ASSET_PATH}Extras/Loudspeaker/Resources/Prefabs/");
      }
   }
}
#endif
// © 2021-2022 crosstales LLC (https://www.crosstales.com)