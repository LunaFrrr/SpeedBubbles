using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using System.Collections.Generic;

public class StickBubbles : MonoBehaviour
{
    public TextMeshProUGUI timer;
    public Sprite[] pages = new Sprite[3];
    public GameObject manga;


    private float seconds;

    public static int nPages = 0;
    public int[] lastRand;
    public static Sprite[] randomPages = new Sprite[3];
    public static GameObject[] bubbles = new GameObject[15];


    private void Awake()
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastRand = new int[randomPages.Length];


        seconds = 15;
        timer.text = $"{seconds}";
        Debug.Log(nPages);

        if (nPages == 0)
        {
            SelectRandomPages();
        }

        manga.GetComponent<Image>().sprite = randomPages[nPages];
    }

    // Update is called once per frame
    void Update()
    {
        Countdown();
        if (seconds == 0)
        {
            NextPage();
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

    private void SelectRandomPages()
    {
        HashSet<int> usedIndexes = new HashSet<int>(); // Conjunto para almacenar índices usados

        for (int i = 0; i < randomPages.Length; i++)
        {
            int nRandom;
            do
            {
                nRandom = Random.Range(0, randomPages.Length); // Generar un índice aleatorio
            } while (usedIndexes.Contains(nRandom)); // Verificar si el índice ya fue usado

            usedIndexes.Add(nRandom); // Agregar el índice al conjunto
            randomPages[i] = pages[nRandom]; // Asignar la página correspondiente
        }

        manga.GetComponent<Image>().sprite = randomPages[nPages];
    }

    public void NextPage()
    {
        nPages++;
        if(nPages < 3)
        {
            SceneManager.LoadScene("StickBubblesManga");

        }

        else
        {

        }
        
    }
}
