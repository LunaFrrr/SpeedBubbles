using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PlayVideoEnd : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private readonly string videoURL = "https://LunaFrrr.github.io/SpeedBubble/Assets/Arte/Animaticas/animatica_final.mp4";

    void Start()
    {
            videoPlayer = this.GetComponent<VideoPlayer>();
            videoPlayer.url = videoURL;
            videoPlayer.Play();
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
