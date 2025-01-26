using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // Asegúrate de tener esto para trabajar con los UI Buttons

public class DisplayMangas : MonoBehaviour
{
    int nPage = 0;

    public GameObject buttonViewNext;
    public GameObject buttonViewLast;
    public GameObject page;
    public GameObject sounds;
    public AudioSource endSound;

    public GameObject bubbleContainer;

    private GameObject lastSong;
    private GameObject loop;


    private void Awake()
    {
        page.GetComponent<Image>().sprite = StickBubbles.randomPages[nPage];
    }


    void Start()
    {
        lastSong = GameObject.Find("Music");
        DontDestroyOnLoad(endSound);
        AddBubbles(StickBubbles.bubblesFirstPage);
    }

    void Update()
    {

        if (nPage == 0)
        {
            buttonViewLast.SetActive(false);
        }
        else
        {
            buttonViewLast.SetActive(true);
        }
    }

    public void AddBubbles(List<Dictionary<string, object>> bubbles)
    {
        for (int i = 0; i < bubbles.Count; i++)
        {
            GameObject bubble = new GameObject($"Bubble_{i}");
            bubble.transform.SetParent(bubbleContainer.transform, false);
            Image image = bubble.AddComponent<Image>();
            image.sprite = (Sprite)bubbles[i]["sprite"];
            RectTransform rectTransform = bubble.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = (Vector3)bubbles[i]["position"];

            Vector2 newSize = rectTransform.sizeDelta;
            newSize.x = 125;
            newSize.y = 155;
            rectTransform.sizeDelta = newSize;
        }
    }

    public void RemoveBubbles()
    {
        GameObject[] allChildren = new GameObject[bubbleContainer.transform.childCount];
        for (int i = 0;i < allChildren.Length; i++)
        {
            Destroy(bubbleContainer.transform.GetChild(i).gameObject);
        }
    }

    public void NextPage()
    {
        
        if (nPage > 1)
        {
            Destroy(loop);
            Destroy(lastSong);
            endSound.Play();
            SceneManager.LoadScene("EndAnimatic");
        }
        else
        {
            sounds.GetComponent<AudioSource>().Play();
            nPage++;
            page.GetComponent<Image>().sprite = StickBubbles.randomPages[nPage];
            RemoveBubbles();
            switch (nPage)
            {
                case 0:
                    AddBubbles(StickBubbles.bubblesFirstPage);
                    break;
                case 1:
                    AddBubbles(StickBubbles.bubblesSecondPage);
                    break;
                case 2:
                    AddBubbles(StickBubbles.bubblesThirdPage);
                    break;
                default:
                    Debug.LogWarning("nPage tiene un valor inesperado: " + nPage);
                    break;
            }

        }

    }

    public void LastPage()
    {
        sounds.GetComponent<AudioSource>().Play();
        nPage--;
        page.GetComponent<Image>().sprite = StickBubbles.randomPages[nPage];
        RemoveBubbles();
        switch (nPage)
        {
            case 0:
                AddBubbles(StickBubbles.bubblesFirstPage);
                break;
            case 1:
                AddBubbles(StickBubbles.bubblesSecondPage);
                break;
            case 2:
                AddBubbles(StickBubbles.bubblesThirdPage);
                break;
            default:
                Debug.LogWarning("nPage tiene un valor inesperado: " + nPage);
                break;
        }

    }
}
