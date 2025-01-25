using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class StickBubbles : MonoBehaviour
{
    public TextMeshProUGUI timer;
    public Sprite[] bubbleSprites = new Sprite[14];
    public Sprite[] pages = new Sprite[3];
    public GameObject manga;
    public GameObject bubbleContainer;
    public GameObject UI;
    public static GameObject[] bubbles;
    public static bool isDragging = false;
    public float seconds;

    public static List<Dictionary<string, object>> bubblesFirstPage = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> bubblesSecondPage = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> bubblesThirdPage = new List<Dictionary<string, object>>();



    public static int nPages = 0;
    public int[] lastRand;
    public static Sprite[] randomPages = new Sprite[3];

    public static List<GameObject> remainingBubbles = new List<GameObject>();
    public static List<GameObject> collidedObjects = new List<GameObject>();


    private void Awake()
    {
        
        InitializeBubbles();  
     
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if(timer != null)
        {
            seconds = 15;
            timer.text = $"{seconds}";
            manga.GetComponent<Image>().sprite = randomPages[nPages];
        }
    }

    void Update()
    {
        if (timer != null)
        {
            Countdown();
            if (seconds == 0)
            {
                HideCollidedObjects();
                NextPage();
            }
        }
    }

    private void Countdown()
    {
        if (seconds > 0)
        {
            seconds -= Time.deltaTime;
        }
        else
        {
            seconds = 0;
        }
        int textSeconds = Mathf.FloorToInt(seconds % 60);
        timer.text = $"{textSeconds}";
    }

    private void InitializeBubbles()
    {
        bubbles = new GameObject[bubbleContainer.transform.childCount];
        for (int i = 0; i < bubbleContainer.transform.childCount; i++)
        {
            bubbles[i] = bubbleContainer.transform.GetChild(i).gameObject;
            remainingBubbles.Add(bubbles[i]);
        }

        lastRand = new int[randomPages.Length];
        AddImageBubbles();
        SelectRandomPages();
    }

    private void SelectRandomPages()
    {
        HashSet<int> usedIndexes = new HashSet<int>();

        for (int i = 0; i < randomPages.Length; i++)
        {
            int nRandom;
            do
            {
                nRandom = Random.Range(0, randomPages.Length);
            } while (usedIndexes.Contains(nRandom));

            usedIndexes.Add(nRandom);
            randomPages[i] = pages[nRandom];
        }

        manga.GetComponent<Image>().sprite = randomPages[nPages];
    }

    private void AddImageBubbles()
    {
        for (int i = 0; i < SelectStack.randomBubbles.Count; i++)
        {
            if (i < bubbles.Length)
            {
                bubbles[i].GetComponent<Image>().sprite = SelectStack.randomBubbles[i];
            }
        }
    }

    public void NextPage()
    {
        nPages++;
        if (nPages < 3) 
        {
            seconds = 15;
            manga.GetComponent<Image>().sprite = randomPages[nPages];
            for (int i = 0; i < bubbles.Length; i++)
            {
                if (collidedObjects.Contains(bubbles[i]))
                {
                    bubbles[i].SetActive(false);
                }
            }
        }
        else
        {
            SceneManager.LoadScene("ShowMangas");
        }
    }

    private void HideCollidedObjects()
    {
        foreach (var obj in collidedObjects)
        {
            obj.SetActive(false);
        }
        collidedObjects.Clear();
    }
}
