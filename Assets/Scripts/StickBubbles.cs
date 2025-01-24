using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StickBubbles : MonoBehaviour
{
    public TextMeshProUGUI timer;

    private float seconds;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        seconds = 15;
        timer.text = $"{seconds}";
    }

    // Update is called once per frame
    void Update()
    {
        Countdown();
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


}
