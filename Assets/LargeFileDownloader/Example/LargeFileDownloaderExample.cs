using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// import large file downloader
using LargeFileDownloader;
using System.IO;

public class LargeFileDownloaderExample : MonoBehaviour {

	const string FILE_URL_MP4 = "http://www.sample-videos.com/video/mp4/720/big_buck_bunny_720p_30mb.mp4";
	const string FILE_URL_FLV = "http://www.sample-videos.com/video/flv/720/big_buck_bunny_720p_30mb.flv";

	FileDownloader downloader;

	DownloadEvent evt = new DownloadEvent();

    private string[] VideoURLs;
    private string[] AlexVideoURLs;
    private string[] AlexVoiceURLs;
    private string[] VoiceURLs;

	// Use this for initialization
	void Start () {

        VideoURLs = new string[10] { "http://Biohack.network/1.mp4", "http://Biohack.network/2.mp4", "http://biohack.network/3.mp4", "http://Biohack.network/4.mp4", "http://Biohack.network/5.mp4", "http://Biohack.network/6.mp4", "http://Biohack.network/7.mp4", "http://Biohack.network/8.mp4", "http://Biohack.network/9.mp4", "http://Biohack.network/10.mp4" };
        AlexVideoURLs = new string[9] { "https://storage.googleapis.com/mindfulness_vr_bucket/Videos/2K/1.mp4", "https://storage.googleapis.com/mindfulness_vr_bucket/Videos/2K/2.mp4", "https://storage.googleapis.com/mindfulness_vr_bucket/Videos/2K/3.mp4", "https://storage.googleapis.com/mindfulness_vr_bucket/Videos/2K/4.mp4", "https://storage.googleapis.com/mindfulness_vr_bucket/Videos/2K/5.mp4", "https://storage.googleapis.com/mindfulness_vr_bucket/Videos/2K/6.mp4", "https://storage.googleapis.com/mindfulness_vr_bucket/Videos/2K/7.mp4", "https://storage.googleapis.com/mindfulness_vr_bucket/Videos/2K/8.mp4", "https://storage.googleapis.com/mindfulness_vr_bucket/Videos/2K/9.mp4" };
        AlexVoiceURLs = new string[10] { "https://storage.googleapis.com/mindfulness_vr_bucket/Voices/Low/1.mp3", "https://storage.googleapis.com/mindfulness_vr_bucket/Voices/Low/2.mp3", "https://storage.googleapis.com/mindfulness_vr_bucket/Voices/Low/3.mp3", "https://storage.googleapis.com/mindfulness_vr_bucket/Voices/Low/4.mp3", "https://storage.googleapis.com/mindfulness_vr_bucket/Voices/Low/5.mp3", "https://storage.googleapis.com/mindfulness_vr_bucket/Voices/Low/6.mp3", "https://storage.googleapis.com/mindfulness_vr_bucket/Voices/Low/7.mp3", "https://storage.googleapis.com/mindfulness_vr_bucket/Voices/Low/8.mp3", "https://storage.googleapis.com/mindfulness_vr_bucket/Voices/Low/9.mp3", "https://storage.googleapis.com/mindfulness_vr_bucket/Voices/Low/10.mp3" };
        VoiceURLs = new string[10] { "http://Biohack.network/Voices/1.mp3", "http://Biohack.network/Voices/2.mp3", "http://biohack.network/Voices/3.mp3", "http://Biohack.network/Voices/4.mp3", "http://Biohack.network/Voices/5.mp3", "http://Biohack.network/Voices/6.mp3", "http://Biohack.network/Voices/7.mp3", "http://Biohack.network/Voices/8.mp3", "http://Biohack.network/Voices/9.mp3", "http://Biohack.network/Voices/10.mp3" };

		// create downloader instance
		downloader = new FileDownloader ();

		// add events listners
		FileDownloader.onComplete += OnDownloadComplete;
		FileDownloader.onProgress += OnProgress;

		Debug.Log (Application.persistentDataPath);
	}


	void OnDownloadComplete(DownloadEvent e)
	{
		evt = e;

		if (evt.error != null)
			Debug.Log (evt.error);
	}

	void OnProgress(DownloadEvent e)
	{
		evt = e;
	}


   

    private bool IsStartedDownloading = false;
    private bool IsDownloadingTwoFile = false;
    private Text CurrentPercentageText;
    private Slider CurrentFillingBar;

    private VideoThumbnailContainer CurrentVideoThumbnail;
    private CustomMeditationContainer CustomMed;
    private AlexVersionMeditationController AlexMed;

    private string pathToSave;
    private string pathToSaveVoice;
    private string pathToSaveVideo;

    int newVideoId;
    int newVoiceId;

    private string currentDownladingItem;

    public void DownloadVideo(int id, Text percentageText, Slider Fillingbar, GameObject LoadingBar, VideoThumbnailContainer VideoThumbnail) {
        int newId = id - 1;
        CurrentPercentageText = percentageText;
        CurrentFillingBar = Fillingbar;
        LoadingBar.SetActive(true);
        CurrentVideoThumbnail = VideoThumbnail;

        IsStartedDownloading = true;
        pathToSave = System.IO.Path.Combine(Application.persistentDataPath, System.IO.Path.GetFileName(VideoURLs[newId]));
        downloader.Download(VideoURLs[newId], pathToSave);
    }


    public void DownloadOnlyVideo(int id, Text percentageText, Slider slider, CustomMeditationContainer customMed, AlexVersionMeditationController customMed2)
    {
        int newId = id - 1;
        CurrentPercentageText = percentageText;
        CurrentFillingBar = slider;
        currentDownladingItem = "Video";
        IsStartedDownloading = true;
        IsDownloadingTwoFile = false;
        if (customMed != null)
        {
            CustomMed = customMed;
            pathToSave = System.IO.Path.Combine(Application.persistentDataPath, System.IO.Path.GetFileName(VideoURLs[newId]));
            downloader.Download(VideoURLs[newId], pathToSave);
        }
        if (customMed2 != null)
        {
            AlexMed = customMed2;
            string filePath = Application.persistentDataPath + "Alex'Version";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            pathToSave = System.IO.Path.Combine(filePath, System.IO.Path.GetFileName(AlexVideoURLs[newId]));
            downloader.Download(AlexVideoURLs[newId], pathToSave);
        }
    }

    public void DownloadAlexVideo(int id, Text percentageText, Slider slider, AlexVersionMeditationController Med)
    {
        AlexMed = Med;
        int newId = id - 1;
        CurrentPercentageText = percentageText;
        CurrentFillingBar = slider;
        currentDownladingItem = "Video";
        IsStartedDownloading = true;
        IsDownloadingTwoFile = false;
        string filePath = Application.persistentDataPath + "Alex'Version";
        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }
        pathToSave = System.IO.Path.Combine(filePath, System.IO.Path.GetFileName(AlexVideoURLs[newId]));
        downloader.Download(AlexVideoURLs[newId], pathToSave);
    }

    public void DownloadOnlyVoice(int id, Text percentageText, Slider slider, CustomMeditationContainer customMed, AlexVersionMeditationController customMed2)
    {
        int newId = id - 1;
        CurrentPercentageText = percentageText;
        CurrentFillingBar = slider;
        currentDownladingItem = "Voice";
        IsStartedDownloading = true;
        IsDownloadingTwoFile = false;

        if (customMed != null)
        {
            CustomMed = customMed;
            pathToSave = System.IO.Path.Combine(Application.persistentDataPath, System.IO.Path.GetFileName(VoiceURLs[newId]));
            downloader.Download(VoiceURLs[newId], pathToSave);
        }
        if (customMed2 != null)
        {
            AlexMed = customMed2;
            string filePath = Application.persistentDataPath + "Alex'Version";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            pathToSave = System.IO.Path.Combine(filePath, System.IO.Path.GetFileName(AlexVoiceURLs[newId]));
            downloader.Download(AlexVoiceURLs[newId], pathToSave);
        }
    }


    public IEnumerator DownloadVideoAndVoice(int videoId, int voiceId, Text percentageText, Slider slider, string download, CustomMeditationContainer customMed, AlexVersionMeditationController customMed2)
    {
        newVideoId = videoId;
        newVoiceId = voiceId - 1;
        CurrentPercentageText = percentageText;
        CurrentFillingBar = slider;
        IsDownloadingTwoFile = true;

        if (customMed != null)
        {
            CustomMed = customMed;
            yield return new WaitForSeconds(0.5f);
            if (download == "voice")
            {
                index = 0;
                IsStartedDownloading = true;
                currentDownladingItem = "Voice";
                pathToSaveVoice = System.IO.Path.Combine(Application.persistentDataPath, System.IO.Path.GetFileName(VoiceURLs[newVoiceId]));
                downloader.Download(VoiceURLs[newVoiceId], pathToSaveVoice);

            }
            else if (download == "video")
            {
                evt.progress = 0;
                index = 1;
                IsStartedDownloading = true;
                currentDownladingItem = "Video";
                pathToSaveVideo = Path.Combine(Application.persistentDataPath, Path.GetFileName(VideoURLs[newVideoId - 1]));
                downloader.Download(VideoURLs[newVideoId - 1], pathToSaveVideo);
            }
        }
        if (customMed2 != null)
        {
            AlexMed = customMed2;
            yield return new WaitForSeconds(0.5f);
            if (download == "voice")
            {
                index = 0;
                IsStartedDownloading = true;
                currentDownladingItem = "Voice";
                string filePath = Application.persistentDataPath + "Alex'Version";
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                pathToSaveVoice = Path.Combine(filePath, Path.GetFileName(AlexVoiceURLs[newVoiceId]));
                downloader.Download(AlexVoiceURLs[newVoiceId], pathToSaveVoice);

            }
            else if (download == "video")
            {
                evt.progress = 0;
                index = 1;
                IsStartedDownloading = true;
                currentDownladingItem = "Video";
                string filePath = Application.persistentDataPath + "Alex'Version";
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                pathToSaveVideo = Path.Combine(filePath, Path.GetFileName(AlexVideoURLs[newVideoId - 1]));
                downloader.Download(AlexVideoURLs[newVideoId - 1], pathToSaveVideo);
            }
        }

       
    }

    int index = 0;

    private void Update()
    {
        if (IsStartedDownloading)
        {
            CurrentPercentageText.text = "Downloading " + currentDownladingItem + " - " + evt.progress.ToString() + " %";
            CurrentFillingBar.value = evt.progress;

            if (evt.progress == 100)
            {
                if (IsDownloadingTwoFile)
                {
                    if (index == 1)
                    {
                        CurrentPercentageText.text = "Completed";
                        if (AlexMed != null)
                        {
                            AlexMed.DownloadComplated();
                        }
                        else
                        {
                            CustomMed.DownloadComplated();
                        }
                        IsStartedDownloading = false;
                    }
                    else
                    {
                        StartCoroutine(DownloadVideoAndVoice(newVideoId, newVoiceId, CurrentPercentageText, CurrentFillingBar, "video", CustomMed, AlexMed));
                        IsStartedDownloading = false;
                    }
                }
                else
                {
                    CurrentPercentageText.text = "Comple-ted";
                    if (AlexMed != null)
                    {
                        AlexMed.DownloadComplated();
                    }
                    else
                    {
                        CustomMed.DownloadComplated();
                    }
                    
                    IsStartedDownloading = false;
                }
                
            }
        }
    }
}
