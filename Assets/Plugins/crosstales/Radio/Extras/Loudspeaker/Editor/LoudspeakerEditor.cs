#if UNITY_EDITOR
using UnityEditor;
using Crosstales.Radio.EditorUtil;

namespace Crosstales.Radio.EditorExtension
{
   /// <summary>Custom editor for the 'Loudspeaker'-class.</summary>
   [CustomEditor(typeof(Crosstales.Radio.Tool.Loudspeaker))]
   public class LoudspeakerEditor : Editor
   {
      #region Variables

      private Crosstales.Radio.Tool.Loudspeaker script;

      #endregion


      #region Editor methods

      private void OnEnable()
      {
         script = (Crosstales.Radio.Tool.Loudspeaker)target;
      }

      public override void OnInspectorGUI()
      {
         DrawDefaultInspector();

         if (script.isActiveAndEnabled)
         {
            if (script.Player != null)
            {
               //add stuff if needed
            }
            else
            {
               EditorHelper.SeparatorUI();
               EditorGUILayout.HelpBox("Please add a 'Player'!", MessageType.Warning);
            }
         }
         else
         {
            EditorHelper.SeparatorUI();
            EditorGUILayout.HelpBox("Script is disabled!", MessageType.Info);
         }
      }

      #endregion
   }
}
#endif
// © 2017-2022 crosstales LLC (https://www.crosstales.com)