using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Unity.VisualScripting;

public class SelectStack : MonoBehaviour
{
    public Sprite[] allBubbles = new Sprite[8];
    public static List<Sprite> randomBubbles = new List<Sprite>();
    private int numBubbles = 8;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateRandomStack() 
    {
        HashSet<int> usedIndexes = new HashSet<int>();

        for (int i = 0; i < numBubbles; i++)
        {
            int nRandom;
            do
            {
                nRandom = Random.Range(0, numBubbles);
            } while (usedIndexes.Contains(nRandom));

            usedIndexes.Add(nRandom);
            randomBubbles.Add(allBubbles[nRandom]);
        }

    }

    public void NextScene()
    {
        GenerateRandomStack();
        SceneManager.LoadScene("StickBubblesManga");
    }
}
