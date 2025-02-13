using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class StickBubbles : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI pagesText;
    public GameObject nextButton;

    public Sprite[] pages = new Sprite[3];
    public GameObject manga;
    public GameObject bubbleContainer;
    public static GameObject[] bubbles;
    public static bool isDragging = false;
    private float seconds;

    public static List<Dictionary<string, object>> bubblesFirstPage = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> bubblesSecondPage = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> bubblesThirdPage = new List<Dictionary<string, object>>();

    public static int nPages = 0;
    public int[] lastRand;
    public static Sprite[] randomPages = new Sprite[3];

    public static List<GameObject> remainingBubbles = new List<GameObject>();
    public static List<GameObject> collidedObjects = new List<GameObject>();

    public AudioSource clock;
    public AudioSource music;
    public GameObject info;

    public static bool readBubbles;

    private SelectStack selectStack;
    private GameObject lastMusic;

    TransitionManager TransitionManager;


    private void Awake()
    {
        bubblesFirstPage.Clear();
        bubblesSecondPage.Clear();
        bubblesThirdPage.Clear();

        nPages = 0;
        seconds = 15;
        readBubbles = false;
        nextButton.SetActive(false);
        info.SetActive(true);
        InitializeBubbles();
        lastMusic = GameObject.Find("Selection");

        TransitionManager = FindFirstObjectByType<TransitionManager>();
    }

    void Start()
    {
        
        
        DontDestroyOnLoad(music);
        DontDestroyOnLoad(gameObject);
        pagesText.text = $"{nPages + 1}/3";
        if(timerText != null)
        {
            Debug.Log("ENTRAAAA");
            timerText.text = $"0:{seconds}";
            manga.GetComponent<Image>().sprite = randomPages[nPages];
        }
    }

    void Update()
    {
        if (timerText != null)
        {
            
            if (seconds <= 0)
            {
                seconds = 30;
                if (readBubbles)
                {
                    HideCollidedObjects();
                    NextPage();
                }
            }
            if (seconds <= 10 && !clock.isPlaying)
            {
                clock.Play();
            }
            if (seconds > 10 && clock.isPlaying)
            {
                clock.Pause();
            }
            Countdown();
        }

        if (!readBubbles)
        {
            nextButton.SetActive(false);
        }
        else nextButton.SetActive(true);
    }

    private void Countdown()
    {

        if (seconds > 0)
        {
            seconds -= Time.deltaTime;
            if (!readBubbles && seconds >= 29)
            {
                info.SetActive(false);
                readBubbles = true;
                AudioSource audioSource = lastMusic.GetComponent<AudioSource>();
                if (audioSource != null && audioSource.isPlaying)
                {
                    audioSource.Pause();
                }
                    music.Play();
            }
        }
        else
        {
            
            seconds = 0;
        }

        string textSeconds;
        if (seconds >= 10)
        {
            textSeconds = Mathf.FloorToInt(seconds % 60).ToString();
        }
        else if(seconds < 0)
        {
            textSeconds = "00";
        }
        else
        {
            textSeconds = "0" +  Mathf.FloorToInt(seconds % 60).ToString();
        }
        timerText.text = $"0:{textSeconds}";
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
        for (int i = 0; i < SelectStack.selectedSprites.Count; i++)
        {
            if (i < bubbles.Length)
            {
                bubbles[i].GetComponent<Image>().sprite = SelectStack.selectedSprites[i];
            }
        }
    }

    public void NextPage()
    {
        nextButton.GetComponent<AudioSource>().Play();
        nPages++;
        
        if (nPages < 3) 
        {
            pagesText.text = $"{nPages + 1}/3";
            seconds = 30;
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
            TransitionManager.LoadTransition();
            //SceneManager.LoadScene("ShowMangas");
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
