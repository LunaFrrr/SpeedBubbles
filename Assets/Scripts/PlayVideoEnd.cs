using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PlayVideoEnd : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string videoPath = Path.Combine(Application.streamingAssetsPath, "animatica_final.mp4");

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
        SceneManager.LoadScene("MainMenu");
    }
}
