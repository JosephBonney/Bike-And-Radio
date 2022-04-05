using UnityEngine;
using UnityEngine.UI;
using Crosstales.Radio.Util;
using Crosstales.Radio.Model;
using Crosstales.Radio.Model.Enum;

namespace Crosstales.Radio.Demo
{
   /// <summary>GUI for a very simple radio player.</summary>
   [HelpURL("https://www.crosstales.com/media/data/assets/radio/api/class_crosstales_1_1_radio_1_1_demo_1_1_g_u_i_play_radio.html")]
   public class GUIPlayOwnRadio : MonoBehaviour
   {
      #region Variables

      /// <summary>'RadioPlayer' from the scene.</summary>
      [Header("Settings")] [Tooltip("'RadioPlayer' from the scene.")] public RadioPlayer Player;

      public Crosstales.Radio.Provider.RadioProviderUser Provider;

      /// <summary>The color for the Play-mode.</summary>
      [Tooltip("The color for the Play-mode.")] public Color32 PlayColor = new Color32(0, 255, 0, 64);

      /// <summary>How many times should the radio station restart after an error before giving up (default: 3).</summary>
      [Tooltip("How many times should the radio station restart after an error before giving up (default: 3).")]
      public int Retries = 3;

      [Header("UI Objects")] public Button PlayButton;
      public Button StopButton;
      public Image MainImage;
      public Text Station;
      public Text ElapsedTime;
      public Text ErrorText;
      public Text ElapsedRecordTime;
      public Text RecordTitle;
      public Text RecordArtist;
      public Text DownloadSizeStation;
      public Text ElapsedStationTime;
      public Text NextRecordTitle;
      public Text NextRecordArtist;
      public Text NextRecordDelay;
      public InputField Url;

      public Text NameValue;
      public Text GenresValue;
      public Text BitrateValue;

      private int invokeDelayCounter = 1;
      private bool isStopped = true;
      private Color32 color;
      private int playtime;
      private int recordPlaytime;

      private RecordInfo currentRecord;
      private RecordInfo nextRecord;

      #endregion


      #region MonoBehaviour methods

      private void Start()
      {
         if (Player != null)
         {
            // Subscribe event listeners
            Player.OnPlaybackStart += onPlaybackStart;
            Player.OnPlaybackEnd += onPlaybackEnd;
            Player.OnAudioStart += onAudioStart;
            Player.OnAudioEnd += onAudioEnd;
            Player.OnAudioPlayTimeUpdate += onAudioPlayTimeUpdate;
            Player.OnBufferingProgressUpdate += onBufferingProgressUpdate;
            Player.OnErrorInfo += onErrorInfo;
            Player.OnRecordChange += onRecordChange;
            Player.OnRecordPlayTimeUpdate += onRecordPlayTimeUpdate;
            Player.OnNextRecordChange += onNextRecordChange;
            Player.OnNextRecordDelayUpdate += onNextRecordDelayUpdate;

            if (ErrorText != null)
               ErrorText.text = string.Empty;

            if (Url != null)
               Url.text = Player.Station.Url;
         }
         else
         {
            const string msg = "'Player' is null!";

            if (ErrorText != null)
               ErrorText.text = msg;

            Debug.LogError(msg, this);
         }

         if (ElapsedTime != null)
            ElapsedTime.text = Constants.TEXT_STOPPED;

         if (Station != null)
            Station.text = Constants.TEXT_QUESTIONMARKS;

         if (MainImage != null)
            color = MainImage.color;

         if (Player != null)
         {
            if (PlayButton != null)
               PlayButton.interactable = !Player.isPlayback;

            if (StopButton != null)
               StopButton.interactable = Player.isPlayback;

            if (MainImage != null && Player.isPlayback)
            {
               MainImage.color = PlayColor;
            }
         }
         //         }

         onPlaybackEnd(null); //initialize GUI-components
      }

      private void Update()
      {
         if (Time.frameCount % 20 == 0) //&& Player != null)
         {
            if (Player.isPlayback)
            {
               if (nextRecord?.Equals(currentRecord) == true)
               {
                  //if (NextRecordTitle != null)
                  NextRecordTitle.text = string.Empty;

                  //if (NextRecordArtist != null)
                  NextRecordArtist.text = string.Empty;
               }

               //if (Station != null)
               Station.text = Player.Station.Name;

               //if (NameValue != null)
               NameValue.text = Player.Station.Name;

               if (GenresValue != null)
                  GenresValue.text = Player.Station.Genres;

               if (BitrateValue != null)
                  BitrateValue.text = Player.Station.Bitrate + "kbit/s";
            }
            else
            {
               //if (Station != null)
               Station.text = Constants.TEXT_QUESTIONMARKS;

               //if (NameValue != null)
               NameValue.text = Constants.TEXT_QUESTIONMARKS;

               if (GenresValue != null)
                  GenresValue.text = Constants.TEXT_QUESTIONMARKS;

               if (BitrateValue != null)
                  BitrateValue.text = Constants.TEXT_QUESTIONMARKS;
            }
         }
      }

      private void OnDestroy()
      {
         if (Player != null)
         {
            Player.OnPlaybackStart -= onPlaybackStart;
            Player.OnPlaybackEnd -= onPlaybackEnd;
            Player.OnAudioStart -= onAudioStart;
            Player.OnAudioEnd -= onAudioEnd;
            Player.OnAudioPlayTimeUpdate -= onAudioPlayTimeUpdate;
            Player.OnBufferingProgressUpdate -= onBufferingProgressUpdate;
            Player.OnErrorInfo -= onErrorInfo;
            Player.OnRecordChange -= onRecordChange;
            Player.OnRecordPlayTimeUpdate -= onRecordPlayTimeUpdate;
            Player.OnNextRecordChange -= onNextRecordChange;
            Player.OnNextRecordDelayUpdate -= onNextRecordDelayUpdate;
         }
      }

      #endregion


      #region Public methods

      public void AddToProvider()
      {
         if (!Provider.Stations.Contains(Player.Station))
         {
            Player.Station.Rating = 5f;
            Provider.Stations.Add(Player.Station);
         }
      }

      public void SetUrl(string url)
      {
         if (Player != null)
            Player.Station = new RadioStation { Url = url.Trim() };

         PlayButton.interactable = !string.IsNullOrEmpty(url);
      }

      public void Play()
      {
         if (Player != null)
         {
            if (ErrorText != null)
               ErrorText.text = string.Empty;

            Player.Play();
         }
      }

      public void Stop()
      {
         if (Player != null)
         {
            Player.Stop();
         }
      }

      public void OpenUrl()
      {
         if (Player != null)
            Crosstales.Common.Util.NetworkHelper.OpenURL(Player.Station.Station);
      }

      public void OpenSpotifyUrl()
      {
         if (Player != null && !string.IsNullOrEmpty(Player.RecordInfo?.SpotifyUrl))
            Application.OpenURL(Player.RecordInfo.SpotifyUrl);
      }

      public void FormatDropdownChanged(int index)
      {
         if (Player != null)
         {
            Player.Station.Format = index == 0 ? AudioFormat.MP3 : AudioFormat.OGG;
         }
      }

      #endregion


      #region Callback methods

      private void onPlaybackStart(RadioStation station)
      {
         if (ErrorText != null)
            ErrorText.text = string.Empty;

         if (PlayButton != null)
            PlayButton.interactable = false;

         if (StopButton != null)
            StopButton.interactable = true;

         if (Station != null)
            Station.text = Player.Station.Name;

         if (MainImage != null)
            MainImage.color = PlayColor;
      }

      private void onPlaybackEnd(RadioStation station)
      {
         if (PlayButton != null && Player != null && !string.IsNullOrEmpty(Player.Station.Url))
            PlayButton.interactable = true;

         if (StopButton != null)
            StopButton.interactable = false;

         if (ElapsedTime != null)
            ElapsedTime.text = Constants.TEXT_STOPPED;

         if (ElapsedRecordTime != null)
            ElapsedRecordTime.text = Helper.FormatSecondsToHourMinSec(0f);

         if (ElapsedStationTime != null)
            ElapsedStationTime.text = Helper.FormatSecondsToHourMinSec(0f);

         if (DownloadSizeStation != null)
            DownloadSizeStation.text = Helper.FormatBytesToHRF(0);

         if (RecordTitle != null)
            RecordTitle.text = string.Empty;

         if (RecordArtist != null)
            RecordArtist.text = string.Empty;

         if (NextRecordTitle != null)
            NextRecordTitle.text = string.Empty;

         if (NextRecordArtist != null)
            NextRecordArtist.text = string.Empty;

         if (NextRecordDelay != null)
            NextRecordDelay.text = string.Empty;

         if (MainImage != null)
            MainImage.color = color;

         if (ElapsedTime != null)
            ElapsedTime.text = Constants.TEXT_STOPPED;
      }

      private void onAudioStart(RadioStation station)
      {
         isStopped = false;
      }

      private void onAudioEnd(RadioStation station)
      {
         isStopped = true;
      }

      private void onAudioPlayTimeUpdate(RadioStation station, float _playtime)
      {
         if ((int)_playtime != playtime)
         {
            if (ElapsedTime != null)
               ElapsedTime.text = Helper.FormatSecondsToHourMinSec(_playtime);

            if (DownloadSizeStation != null)
               DownloadSizeStation.text = Helper.FormatBytesToHRF(station.TotalDataSize);

            if (ElapsedStationTime != null)
               ElapsedStationTime.text = Helper.FormatSecondsToHourMinSec(station.TotalPlayTime);

            if (_playtime > 30f)
               invokeDelayCounter = 1;

            playtime = (int)_playtime;
         }
      }

      private void onBufferingProgressUpdate(RadioStation station, float progress)
      {
         if (ElapsedTime != null)
            ElapsedTime.text = Constants.TEXT_BUFFER + progress.ToString(Constants.FORMAT_PERCENT);
      }

      private void onErrorInfo(RadioStation station, string info)
      {
         Stop();
         onPlaybackEnd(station);

         if (!isStopped)
         {
            if (invokeDelayCounter < Retries)
            {
               Debug.LogWarning("Error occured -> Restarting station." + System.Environment.NewLine + info, this);

               Invoke(nameof(play), Constants.INVOKE_DELAY * invokeDelayCounter);

               invokeDelayCounter++;
            }
            else
            {
               string msg = "Restarting station failed more than " + Retries + " times - giving up!" + System.Environment.NewLine + info;

               if (ErrorText != null)
                  ErrorText.text = msg;

               Debug.LogError(msg, this);
            }
         }
         else
         {
            if (ErrorText != null)
               ErrorText.text = info;
         }
      }

      private void onRecordChange(RadioStation station, RecordInfo record)
      {
         currentRecord = record;

         if (RecordTitle != null)
            RecordTitle.text = record.Title;

         if (RecordArtist != null)
            RecordArtist.text = record.Artist;

         if (NextRecordDelay != null)
            NextRecordDelay.text = string.Empty;
      }

      private void onRecordPlayTimeUpdate(RadioStation station, RecordInfo record, float _playtime)
      {
         if ((int)_playtime != recordPlaytime)
         {
            recordPlaytime = (int)_playtime;

            if (ElapsedRecordTime != null)
               ElapsedRecordTime.text = Helper.FormatSecondsToHourMinSec(_playtime);
         }
      }

      private void onNextRecordChange(RadioStation station, RecordInfo record, float delay)
      {
         nextRecord = record;

         if (NextRecordTitle != null)
            NextRecordTitle.text = record.Title;

         if (NextRecordArtist != null)
            NextRecordArtist.text = record.Artist;
      }

      private void onNextRecordDelayUpdate(RadioStation station, RecordInfo record, float delay)
      {
         if (NextRecordDelay != null)
            NextRecordDelay.text = delay.ToString("#0.0");
      }

      #endregion


      #region Private methods

      private void play()
      {
         if (!isStopped)
            Play();
      }

      #endregion
   }
}
// © 2015-2022 crosstales LLC (https://www.crosstales.com)