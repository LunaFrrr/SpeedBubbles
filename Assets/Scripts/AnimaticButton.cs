using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class IniAnimatic : MonoBehaviour, IPointerDownHandler
{
    public GameObject skipButton;

    public void OnPointerDown(PointerEventData eventData)
    {
        skipButton.SetActive(true);
    }

}
