#if UNITY_EDITOR
using UnityEditor;

namespace Crosstales.Radio.OnRadio.EditorExtension
{
   /// <summary>Custom editor for the 'PlaylistService'-class.</summary>
   [CustomEditor(typeof(Crosstales.Radio.OnRadio.Service.PlaylistService))]
   public class PlaylistServiceEditor : BaseServiceEditor
   {
      //empty
   }
}
#endif
// © 2020-2022 crosstales LLC (https://www.crosstales.com)