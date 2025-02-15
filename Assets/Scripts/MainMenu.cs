using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public AudioSource buttonSound;
    public AudioSource music;

    TransitionManager TransitionManager;

    private static bool musicPlaying = false;

    void Awake()
    {
        if (musicPlaying)
        {
            Destroy(music.gameObject);
        }
        TransitionManager = FindFirstObjectByType<TransitionManager>();

    }

    public void PlayGame()
    {
        buttonSound.Play();
        TransitionManager.LoadTransitionMenu(music);
        //StartCoroutine(PlaySoundAndLoadScene(buttonSound, "IniAnimatic"));
    }

    public void backToMenu()
    {
        
        SceneManager.LoadScene("MainMenu");
        StartCoroutine(PlaySoundAndLoadScene(buttonSound, "MainMenu"));
    }

    public void ToCredits()
    {
        if (!musicPlaying)
        {
            DontDestroyOnLoad(music.gameObject);
            musicPlaying = true;
        }
        StartCoroutine(PlaySoundAndLoadScene(buttonSound, "EndCredits"));
    }


    private IEnumerator PlaySoundAndLoadScene(AudioSource sound, string sceneName)
    {
        sound.Play();
        yield return new WaitForSeconds(0.5f);  // Espera a que termine el sonido
        SceneManager.LoadScene(sceneName);
    }
}
