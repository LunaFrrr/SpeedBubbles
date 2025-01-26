using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PlayVideo : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private GameObject music;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        music = GameObject.Find("MusicMenu");
        if (music != null)
        {
            Destroy(music);
        }
        string videoPath = Path.Combine(Application.streamingAssetsPath, "animatica_ini.mp4");

        if (File.Exists(videoPath))
        {
            videoPlayer = this.GetComponent<VideoPlayer>();
            videoPlayer.url = videoPath;
            videoPlayer.Play();
        }
        else
        {
            Debug.LogError("No se pudo encontrar el archivo de video: " + videoPath);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (videoPlayer.isPaused)
        {
            NextScene();
        }
    }

    public void NextScene()
    {
        SceneManager.LoadScene("MenuBubbleSelection");
    }
}
