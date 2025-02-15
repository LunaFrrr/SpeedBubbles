using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class SelectStack : MonoBehaviour
{
    public static List<Sprite> selectedSprites = new List<Sprite>();
    private int numBubbles = 15;
    public string folderName = "Bubbles";

    TransitionManager TransitionManager;

    private void Awake()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>(folderName);
        selectedSprites = GetRandomSprites(sprites, numBubbles);
        DontDestroyOnLoad(this);
        TransitionManager = FindFirstObjectByType<TransitionManager>();
    }

    private List<Sprite> GetRandomSprites(Sprite[] sprites, int count)
    {
        List<Sprite> randomSprites = new List<Sprite>();
        HashSet<int> usedIndices = new HashSet<int>();

        while (randomSprites.Count < count && randomSprites.Count < sprites.Length)
        {
            int index = Random.Range(0, sprites.Length);
            if (usedIndices.Add(index)) // Solo agrega índices no repetidos
            {
                randomSprites.Add(sprites[index]);
            }
        }

        return randomSprites;
    }

    public void NextScene()
    {
        TransitionManager.LoadTransition(null);
        //SceneManager.LoadScene("StickBubblesManga");
    }
}
