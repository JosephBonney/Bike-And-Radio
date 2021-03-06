using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Crosstales.Radio.Util;

namespace Crosstales.Radio.Demo
{
   /// <summary>Main GUI for all demo scenes.</summary>
   [HelpURL("https://www.crosstales.com/media/data/assets/radio/api/class_crosstales_1_1_radio_1_1_demo_1_1_g_u_i_main.html")]
   public class GUIMain : MonoBehaviour
   {
      #region Variables

      [Header("UI Objects")] public Text Name;
      public Text Version;
      public Text Scene;

      public GameObject InternetNotAvailable;
      public Text DownloadSize;
      public Text ElapsedTotalTime;
      public Toggle FullscreenToogle;

      [Header("Scene-Link")] public string NamePreviousScene;
      public string NameNextScene;

      [Header("Mobile Behaviour"), Tooltip("Never go to sleep mode as long as the app is active (default: true).")]
      public bool NeverSleep = true;


      private float delayCount = 1f;

      #endregion


      #region MonoBehaviour methods

      private void Start()
      {
         Screen.sleepTimeout = NeverSleep ? SleepTimeout.NeverSleep : SleepTimeout.SystemSetting;

         if (FullscreenToogle != null && !Screen.fullScreen)
            FullscreenToogle.isOn = false;

         if (Name != null)
            Name.text = Constants.ASSET_NAME;

         if (Version != null)
            Version.text = Constants.ASSET_VERSION;

         if (DownloadSize != null)
            DownloadSize.text = Helper.FormatBytesToHRF(Context.TotalDataSize);

         if (ElapsedTotalTime != null)
            ElapsedTotalTime.text = Helper.FormatSecondsToHourMinSec(Context.TotalPlayTime);

         if (Scene != null)
            Scene.text = SceneManager.GetActiveScene().name;
      }

      private void Update()
      {
         delayCount += Time.deltaTime;

         if (delayCount > 1f)
         {
            delayCount = 0f;

            //if (DownloadSize != null)
            DownloadSize.text = Helper.FormatBytesToHRF(Context.TotalDataSize);

            //if (ElapsedTotalTime != null)
            ElapsedTotalTime.text = Helper.FormatSecondsToHourMinSec(Context.TotalPlayTime);

            InternetNotAvailable.SetActive(!Crosstales.Common.Util.NetworkHelper.isInternetAvailable);
         }
      }

      #endregion


      #region Public methods

      public void FullscreenEnabled(bool val)
      {
         Screen.fullScreen = val;
      }

      public void OpenAssetURL()
      {
         Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ASSET_CT_URL);
      }

      public void OpenCTURL()
      {
         Crosstales.Common.Util.NetworkHelper.OpenURL(Constants.ASSET_AUTHOR_URL);
      }

      public void PreviousScene()
      {
         Invoke(nameof(previousScene), Constants.INVOKE_DELAY);
      }

      public void NextScene()
      {
         Invoke(nameof(nextScene), Constants.INVOKE_DELAY);
      }

      public void Quit()
      {
         if (Application.isEditor)
         {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
         }
         else
         {
            Application.Quit();
         }
      }

      #endregion


      #region Private methods

      private void previousScene()
      {
         SceneManager.LoadScene(NamePreviousScene);
      }

      private void nextScene()
      {
         SceneManager.LoadScene(NameNextScene);
      }

      #endregion
   }
}
// © 2015-2022 crosstales LLC (https://www.crosstales.com)