using UnityEngine;
using UnityEngine.EventSystems;

public class CursorClick : MonoBehaviour
{
    public Texture2D cursorTexture; // Cursor personalizado
    public Vector2 hotSpot = Vector2.zero;
    private Texture2D defaultCursor; // Cursor original

    void Start()
    {
        // Guarda el cursor original de Project Settings
        defaultCursor = null;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Click presionado
        {
            Debug.Log("Cursor cambiado (Click Sostenido)");
            Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
        }
        else if (Input.GetMouseButtonUp(0)) // Click soltado
        {
            Debug.Log("Cursor restaurado (Click Soltado)");
            Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
        }
    }
}

