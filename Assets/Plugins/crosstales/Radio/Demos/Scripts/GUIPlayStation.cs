#if CT_RADIO_ONRADIO
using UnityEngine;
using UnityEngine.UI;
using Crosstales.Radio.Util;
using Crosstales.Radio.Model;

namespace Crosstales.Radio.Demo
{
   /// <summary>GUI for a very simple normal/random radio station player.</summary>
   [HelpURL("https://www.crosstales.com/media/data/assets/radio/api/class_crosstales_1_1_radio_1_1_demo_1_1_g_u_i_play_station.html")]
   public class GUIPlayStation : MonoBehaviour
   {
      #region Variables

      /// <summary>'SimplePlayer' from the scene.</summary>
      [Header("Settings")] [Tooltip("'SimplePlayer' from the scene.")] public SimplePlayer Player;

      /// <summary>'BaseService' from the scene.</summary>
      [Header("Settings")] [Tooltip("'BaseService' from the scene.")] public Crosstales.Radio.OnRadio.Service.BaseService Service;

      /// <summary>The color for the Play-mode.</summary>
      [Tooltip("The color for the Play-mode.")] public Color32 PlayColor = new Color32(0, 255, 0, 64);

      /// <summary>Limit row length for station info.</summary>
      [Tooltip("Limit row length for station info.")] public int RowLength = 40;


      [Header("UI Objects")] public Button NextButton;
      public Button PreviousButton;
      public Button PlayButton;
      public Button StopButton;
      public Image MainImage;
      public Text Station;
      public Text ElapsedTime;
      public Text StationsNumberText;
      public Text ErrorText;
      public Text ElapsedRecordTime;
      public Text RecordTitle;
      public Text RecordArtist;
      public Text DownloadSizeStation;
      public Text ElapsedStationTime;
      public Text NextRecordTitle;
      public Text NextRecordArtist;
      public Text NextRecordDelay;
      public Text StationInfoDesc;
      public Text StationInfoArea;

      public Image StationIcon;
      public Image SongIcon;

      private Color32 color;
      private int playtime;
      private int recordPlaytime;

      private RecordInfo currentRecord;
      private RecordInfo nextRecord;

      private string uidQuery;

      #endregion


      #region MonoBehaviour methods

      private void Start()
      {
         if (Player != null)
         {
            Player.OnPlaybackStart += onPlaybackStart;
            Player.OnPlaybackEnd += onPlaybackEnd;
            Player.OnAudioPlayTimeUpdate += onAudioPlayTimeUpdate;
            Player.OnBufferingProgressUpdate += onBufferingProgressUpdate;
            Player.OnErrorInfo += onErrorInfo;
            Player.OnProviderReady += onProviderReady;
            Player.OnRecordChange += onRecordChange;
            Player.OnRecordPlayTimeUpdate += onRecordPlayTimeUpdate;
            Player.OnNextRecordChange += onNextRecordChange;
            Player.OnNextRecordDelayUpdate += onNextRecordDelayUpdate;
            Player.OnFilterChange += onFilterChange;
         }

         if (Service != null)
            Service.OnQueryComplete += onQueryComplete;

         if (MainImage != null)
            color = MainImage.color;

         if (ElapsedTime != null)
            ElapsedTime.text = "Loading...";

         if (PlayButton != null)
            PlayButton.interactable = false;

         if (NextButton != null)
            NextButton.interactable = false;

         if (PreviousButton != null)
            PreviousButton.interactable = false;

         if (StopButton != null)
            StopButton.interactable = false;

         if (Player != null && Player.Player != null)
         {
            if (ErrorText != null)
               ErrorText.text = string.Empty;
         }
         else
         {
            const string msg = "'Player' is null!";

            Debug.LogError(msg, this);

            if (ErrorText != null)
               ErrorText.text = msg;
         }

         //initialize GUI-components
         onPlaybackEnd(null);
      }

      private void Update()
      {
         if (Time.frameCount % 20 == 0)
         {
            if (nextRecord?.Equals(currentRecord) == true)
            {
               //if (NextRecordTitle != null)
               NextRecordTitle.text = string.Empty;

               //if (NextRecordArtist != null)
               NextRecordArtist.text = string.Empty;
            }
         }
      }

      private void OnDestroy()
      {
         if (Player != null)
         {
            Player.OnPlaybackStart -= onPlaybackStart;
            Player.OnPlaybackEnd -= onPlaybackEnd;
            Player.OnAudioPlayTimeUpdate -= onAudioPlayTimeUpdate;
            Player.OnBufferingProgressUpdate -= onBufferingProgressUpdate;
            Player.OnErrorInfo -= onErrorInfo;
            Player.OnProviderReady -= onProviderReady;
            Player.OnRecordChange -= onRecordChange;
            Player.OnRecordPlayTimeUpdate -= onRecordPlayTimeUpdate;
            Player.OnNextRecordChange -= onNextRecordChange;
            Player.OnNextRecordDelayUpdate -= onNextRecordDelayUpdate;
         }

         if (Service != null)
            Service.OnQueryComplete -= onQueryComplete;
      }

      #endregion


      #region Public methods

      public void OpenUrl()
      {
         if (Player != null)
            Crosstales.Common.Util.NetworkHelper.OpenURL(Player.Player.Station?.Station);
      }

      public void OpenSpotifyUrl()
      {
         if (Player != null && !string.IsNullOrEmpty(Player.RecordInfo?.SpotifyUrl))
            Application.OpenURL(Player.Player.RecordInfo.SpotifyUrl);
      }

      public void OpenLyricsUrl()
      {
         if (Player != null)
            Crosstales.Common.Util.NetworkHelper.OpenURL(Player.RecordInfo?.LyricsUrl);
      }

      public void FilterStations(string filter)
      {
         if (Player != null)
            Player.Filter.Stations = filter;
      }

      public void FilterNames(string filter)
      {
         if (Player != null)
            Player.Filter.Names = filter;
      }

      public void FilterGenre(string filter)
      {
         if (Player != null)
            Player.Filter.Genres = filter;
      }

      public void FilterRatingMin(string rating)
      {
         if (Player != null)
            Player.Filter.RatingMin = float.TryParse(rating, out float _rating) ? _rating : 0f;
      }

      public void FilterRatingMax(string rating)
      {
         if (Player != null)
            Player.Filter.RatingMax = float.TryParse(rating, out float _rating) ? _rating : 5f;
      }

      #endregion


      #region Private methods

      private void loadRecordIcon(RecordInfo record)
      {
         //Debug.LogWarning($"loadRecordIcon: {Service != null} - '{record}'", this);

         if (Service != null && string.IsNullOrEmpty(record.IconUrl) && !string.IsNullOrEmpty(record.Title) && !string.IsNullOrEmpty(record.Artist))
         {
            uidQuery = Service.SongArtService(record, true);
         }
         else
         {
            if (SongIcon != null)
               SongIcon.sprite = record.Icon == null && Service != null ? Service.DefaultSongIcon : record.Icon;
         }
      }

      private System.Collections.IEnumerator loadStationIcon(RadioStation station)
      {
         if (StationIcon != null)
         {
            if (station.Icon != null)
            {
               StationIcon.sprite = station.Icon;
            }
            else
            {
               if (!string.IsNullOrEmpty(station.IconUrl))
               {
                  yield return Crosstales.Radio.Tool.LoadIcon.Load(station);
                  StationIcon.sprite = station.Icon;
               }
               else
               {
                  StationIcon.sprite = Service.DefaultStationIcon;
               }
            }
         }

         if (string.IsNullOrEmpty(Player.RecordInfo.IconUrl))
            Service.SongArtService(Player.RecordInfo, true);
      }

      #endregion


      #region Callback methods

      private void onPlaybackStart(RadioStation station)
      {
         if (MainImage != null)
            MainImage.color = PlayColor;

         if (PlayButton != null)
            PlayButton.interactable = false;

         if (StopButton != null)
            StopButton.interactable = true;

         if (ErrorText != null)
            ErrorText.text = string.Empty;

         if (Station != null)
            Station.text = station.Name;

         if (StationInfoDesc != null)
            StationInfoDesc.text = station.StationInfoLabels(true);

         if (StationInfoArea != null)
            StationInfoArea.text = station.StationInfo(false, RowLength, true);

         StartCoroutine(loadStationIcon(station));
      }

      private void onPlaybackEnd(RadioStation station)
      {
         if (MainImage != null)
            MainImage.color = color;

         if (PlayButton != null && Player.CountStations() > 0)
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

         if (StationInfoDesc != null)
            StationInfoDesc.text = Constants.TEXT_STOPPED;

         if (StationInfoArea != null)
            StationInfoArea.text = string.Empty;

         if (StationIcon != null)
            StationIcon.sprite = Service.DefaultStationIcon;

         if (SongIcon != null)
            SongIcon.sprite = Service.DefaultSongIcon;

         if (Station != null)
            Station.text = Constants.TEXT_QUESTIONMARKS;
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
         if (ErrorText != null)
            ErrorText.text = info;
      }

      private void onProviderReady()
      {
         if (Player.CountStations() > 0)
         {
            if (NextButton != null)
               NextButton.interactable = true;

            if (PreviousButton != null)
               PreviousButton.interactable = true;

            if (PlayButton != null)
               PlayButton.interactable = true;
         }

         if (ElapsedTime != null)
            ElapsedTime.text = Constants.TEXT_STOPPED;

         onFilterChange();
      }

      private void onFilterChange()
      {
         if (Player != null && StationsNumberText != null)
            StationsNumberText.text = Player.CountStations() + "/" + Player.Set.Stations.Count;
      }

      private void onRecordChange(RadioStation station, RecordInfo record)
      {
         //Debug.LogWarning("onRecordChange: " + record, this);

         currentRecord = record;

         if (RecordTitle != null)
            RecordTitle.text = record.Title;

         if (RecordArtist != null)
            RecordArtist.text = record.Artist;

         if (NextRecordDelay != null)
            NextRecordDelay.text = string.Empty;

         loadRecordIcon(record);
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

      private void onQueryComplete(string id)
      {
         if (id.Equals(uidQuery) && SongIcon != null)
            SongIcon.sprite = Player.RecordInfo.Icon;
      }

      #endregion
   }
}
#endif
// © 2016-2022 crosstales LLC (https://www.crosstales.com)