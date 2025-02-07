using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PlayVideo : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private GameObject music;

    private readonly string videoURL = "https://LunaFrrr.github.io/SpeedBubble/Assets/Arte/Animaticas/animatica_ini.mp4";
    void Start()
    {
        music = GameObject.Find("MusicMenu");
        if (music != null)
        {
            Destroy(music);
        }
        //string videoPath = Path.Combine(Application.streamingAssetsPath, "animatica_ini.mp4");

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
        SceneManager.LoadScene("MenuBubbleSelection");
    }
}
