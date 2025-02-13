using UnityEngine;
using UnityEngine.EventSystems;

public class CursorClick : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Vector2 hotSpot = Vector2.zero;
    private Texture2D defaultCursor;

    void Start()
    {
        defaultCursor = null;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Cursor cambiado (Click Sostenido)");
            Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
        }
    }
}

