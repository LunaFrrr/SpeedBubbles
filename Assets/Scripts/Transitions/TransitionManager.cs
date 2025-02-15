using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public void LoadTransition(AudioSource music)
    {
        if (music != null)
        {
            StartCoroutine(FadeOut(music, 1));
        }
        StartCoroutine(Transition(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadTransitionMenu(AudioSource music)
    {
        StartCoroutine(FadeOut(music, 1));
        StartCoroutine(Transition(2));
    }

    IEnumerator Transition(int sceneIndex)
    {
        transition.SetTrigger("TransitionTrigger");

        yield return new WaitForSeconds(transitionTime);
        if (sceneIndex > 6) sceneIndex = 0;
        SceneManager.LoadScene(sceneIndex);

    }

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        audioSource.Play();
        float startVolume = audioSource.volume;
        while (audioSource.volume < 1)
        {
            audioSource.volume += startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.volume = startVolume;
    }
}
