using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PlayVideoEnd : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private readonly string videoURL = "https://LunaFrrr.github.io/SpeedBubble/Assets/Arte/Animaticas/animatica_final.mp4";
    TransitionManager TransitionManager;

    void Start()
    {
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
            TransitionManager.LoadTransition();
        }
    }

    public void NextScene()
    {
        //SceneManager.LoadScene("MainMenu");
        TransitionManager.LoadTransition();
    }
}
