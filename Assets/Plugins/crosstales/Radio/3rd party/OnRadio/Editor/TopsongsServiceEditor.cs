#if UNITY_EDITOR
using UnityEditor;

namespace Crosstales.Radio.OnRadio.EditorExtension
{
   /// <summary>Custom editor for the 'TopsongsService'-class.</summary>
   [CustomEditor(typeof(Crosstales.Radio.OnRadio.Service.TopsongsService))]
   public class TopsongsServiceEditor : BaseServiceEditor
   {
      //empty
   }
}
#endif
// © 2020-2022 crosstales LLC (https://www.crosstales.com)