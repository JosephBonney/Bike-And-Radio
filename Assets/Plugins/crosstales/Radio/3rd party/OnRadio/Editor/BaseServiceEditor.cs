#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using Crosstales.Radio.OnRadio.Util;
using Crosstales.Radio.OnRadio.Service;
using Crosstales.Radio.EditorUtil;

namespace Crosstales.Radio.OnRadio.EditorExtension
{
   /// <summary>Custom editor for the 'BaseService'-class.</summary>
   [CustomEditor(typeof(BaseService))]
   public abstract class BaseServiceEditor : Editor
   {
      #region Variables

      private BaseService _script;

      private bool showStations;
      private bool showRecords;

      #endregion


      #region Editor methods

      protected virtual void OnEnable()
      {
         _script = (BaseService)target;
      }

      public override bool RequiresConstantRepaint()
      {
         return true;
      }

      public override void OnInspectorGUI()
      {
         EditorHelper.BannerOC();

         if (GUILayout.Button(new GUIContent(" Learn more", EditorHelper.Icon_Manual, "Learn more about OnRadio.")))
            Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ONRADIO_URL);

         DrawDefaultInspector();

         EditorHelper.SeparatorUI();

         if (_script.isActiveAndEnabled)
         {
            if (_script.isValidToken)
            {
               GUILayout.Label("Data", EditorStyles.boldLabel);

               GUILayout.Space(6);

               showStations = EditorGUILayout.Foldout(showStations, "Stations (" + _script.Stations.Count + ")");
               if (showStations)
               {
                  EditorGUI.indentLevel++;

                  foreach (Crosstales.Radio.OnRadio.Model.RadioStationExt station in _script.Stations)
                  {
                     EditorGUILayout.SelectableLabel(station.ToShortString(), GUILayout.Height(16), GUILayout.ExpandHeight(false));
                  }

                  EditorGUI.indentLevel--;
               }

               showRecords = EditorGUILayout.Foldout(showRecords, "Records (" + _script.Records.Count + ")");
               if (showRecords)
               {
                  EditorGUI.indentLevel++;

                  foreach (Crosstales.Radio.OnRadio.Model.RecordInfoExt record in _script.Records)
                  {
                     EditorGUILayout.SelectableLabel(record.ToShortString(), GUILayout.Height(16), GUILayout.ExpandHeight(false));
                  }

                  EditorGUI.indentLevel--;
               }

               EditorHelper.SeparatorUI();

               GUILayout.Label("Stats:", EditorStyles.boldLabel);

               GUILayout.Label("Playlist Requests:\t\t" + BaseService.TotalPlaylistRequests);
               GUILayout.Label("Reco2 Requests:\t\t" + BaseService.TotalReco2Requests);
               GUILayout.Label("Topsongs Requests:\t\t" + BaseService.TotalTopsongsRequests);
               GUILayout.Label("Station Requests:\t\t" + BaseService.TotalStationRequests);
               GUILayout.Label("SongArt Requests:\t\t" + BaseService.TotalSongArtRequests);
               GUILayout.Label("DARStation Requests:\t\t" + BaseService.TotalDARStationRequests);
               GUILayout.Space(6);
               GUILayout.Label("Total Requests:\t\t" + BaseService.TotalRequests);
            }
            else
            {
               EditorGUILayout.HelpBox("Please add a valid 'Token' to access OnRadio!", MessageType.Warning);
            }
         }
         else
         {
            EditorGUILayout.HelpBox("Script is disabled!", MessageType.Info);
         }
      }

      #endregion
   }
}
#endif
// © 2020-2022 crosstales LLC (https://www.crosstales.com)