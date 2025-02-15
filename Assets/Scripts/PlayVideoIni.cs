using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PlayVideo : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private GameObject music;

    TransitionManager TransitionManager;

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

        TransitionManager = FindFirstObjectByType<TransitionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (videoPlayer.isPaused)
        {

            //NextScene();
            TransitionManager.LoadTransition(music.GetComponent<AudioSource>());
        }
    }

    public void NextScene()
    {
        TransitionManager.LoadTransition(null);
        //SceneManager.LoadScene("MenuBubbleSelection");
    }
}
