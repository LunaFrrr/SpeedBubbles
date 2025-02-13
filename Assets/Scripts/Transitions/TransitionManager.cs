using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Animator transition;
    public float transitionTime = 1f;

    void Start()
    {

    }

    public void LoadTransition()
    {
        StartCoroutine(Transition(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadTransitionMenu()
    {
        StartCoroutine(Transition(2));
    }

    IEnumerator Transition(int sceneIndex)
    {
        transition.SetTrigger("TransitionTrigger");

        yield return new WaitForSeconds(transitionTime);
        if (sceneIndex > 6) sceneIndex = 0;
        SceneManager.LoadScene(sceneIndex);

    }
}
