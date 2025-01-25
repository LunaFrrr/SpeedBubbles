using System.Drawing;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.UIElements;
using System;
using UnityEngine.UI;

public class DisplayMangas : MonoBehaviour
{
    public Sprite buttonSprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    GameObject FindInChildren(GameObject parent, string name)
    {
        foreach (Transform child in parent.transform)
        {
            if (child.name == name)
            {
                return child.gameObject;
            }

            GameObject result = FindInChildren(child.gameObject, name);
            if (result != null)
            {
                return result;
            }
        }

        return null;
    }

    void Start()
    {
        GameObject ui = GameObject.Find("UI");

        GameObject panel = FindInChildren(ui, "Panel");
        GameObject lastButton = FindInChildren(ui, "Button");
        GameObject timer = FindInChildren(ui, "TimerText (TMP)");
        Destroy(lastButton);
        Destroy(timer);

        DestroyRemainingBubbles();
        Vector3 position = new Vector3(804, -484, 0);
        Vector3 position2 = new Vector3(-761, -484, 0);

        GameObject buttonNext = new GameObject();
        buttonNext.name = "ButtonNext";
        buttonNext.transform.SetParent(panel.transform, false);
        RectTransform rectNext = buttonNext.AddComponent<RectTransform>();
        rectNext.localPosition = position;
        buttonNext.AddComponent<UnityEngine.UI.Image>();
        buttonNext.AddComponent<UnityEngine.UI.Button>();
        buttonNext.GetComponent<UnityEngine.UI.Image>().sprite = buttonSprite;

        GameObject buttonLast = new GameObject();
        buttonLast.name = "ButtonLast";
        buttonLast.transform.SetParent(panel.transform, false);
        RectTransform rectLast = buttonLast.AddComponent<RectTransform>();
        rectLast.localPosition = position2;
        buttonLast.AddComponent<UnityEngine.UI.Image>();
        buttonLast.AddComponent<UnityEngine.UI.Button>();
        buttonLast.GetComponent<UnityEngine.UI.Image>().sprite = buttonSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DestroyRemainingBubbles()
    {
        for (int i = 0; i < StickBubbles.remainingBubbles.Count; i++)
        {
            Destroy(StickBubbles.remainingBubbles[i]);
        }
    }
}
