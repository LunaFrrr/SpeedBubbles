using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SelectStack : MonoBehaviour
{
    public static List<Sprite> selectedSprites = new List<Sprite>();
    private int numBubbles = 15;
    public string label = "default"; 

    private void Awake()
    {
        Addressables.LoadAssetsAsync<Sprite>(label, null).Completed += OnSpritesLoaded;
        DontDestroyOnLoad(this);
    }


    private void OnSpritesLoaded(AsyncOperationHandle<IList<Sprite>> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Se cargaron " + handle.Result.Count + " sprites.");
            selectedSprites = GetRandomSprites(handle.Result, numBubbles);

            foreach (var sprite in selectedSprites)
            {
                Debug.Log("Sprite seleccionado: " + sprite.name);
            }
        }
        else
        {
            Debug.LogError("Error al cargar sprites.");
        }
    }

    private List<Sprite> GetRandomSprites(IList<Sprite> sprites, int count)
    {
        List<Sprite> randomSprites = new List<Sprite>();
        HashSet<int> usedIndices = new HashSet<int>();

        while (randomSprites.Count < count && randomSprites.Count < sprites.Count)
        {
            int index = Random.Range(0, sprites.Count);
            if (usedIndices.Add(index)) // Solo agrega índices no repetidos
            {
                randomSprites.Add(sprites[index]);
            }
        }

        return randomSprites;
    }

    public void NextScene()
    {
        SceneManager.LoadScene("StickBubblesManga");
    }
}
