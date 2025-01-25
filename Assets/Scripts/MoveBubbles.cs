using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveBubbles : MonoBehaviour, IDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private Vector3 originalPos;
    private Canvas canvas;
    private GraphicRaycaster raycaster;
    private EventSystem eventSystem;
    private bool droppedInside = false;

    public StickBubbles StickBubbles;
    public AudioSource dropBubble;
    public AudioSource pickBubble;

    void Start()
    {
        originalPos = transform.position;
        canvas = GetComponentInParent<Canvas>();
        raycaster = canvas.GetComponent<GraphicRaycaster>();
        eventSystem = EventSystem.current;
    }

    void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pickBubble.Play();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!droppedInside && !StickBubbles.isDragging)
        {
            transform.localScale = Vector3.one * 2;
            gameObject.GetComponent<RectTransform>().SetAsLastSibling();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
    }

    public void OnDrag(PointerEventData eventData)
    {
        StickBubbles.isDragging = true;
        transform.localScale = Vector3.one;
        if (!droppedInside) 
        {
            gameObject.GetComponent<RectTransform>().SetAsLastSibling();
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                eventData.position,
                canvas.worldCamera,
                out localPoint
            );
            transform.position = canvas.transform.TransformPoint(localPoint);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        dropBubble.Play();
        StickBubbles.isDragging = false;
        if (IsFullyInsideManga())
        {
            StickBubbles.remainingBubbles.Remove(gameObject);
            if (!StickBubbles.collidedObjects.Contains(gameObject))
            {
                StickBubbles.collidedObjects.Add(gameObject);
            }
            droppedInside = true;

            // Obtener el sprite y la posición
            Sprite bubbleSprite = gameObject.GetComponent<Image>().sprite;
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            Vector3 bubblePosition = rectTransform.anchoredPosition;

            var bubbleData = new Dictionary<string, object>
        {
            { "sprite", bubbleSprite },
            { "position", bubblePosition }
        };

            switch (StickBubbles.nPages)
            {
                case 0:
                    StickBubbles.bubblesFirstPage.Add(bubbleData);
                    break;
                case 1:
                    StickBubbles.bubblesSecondPage.Add(bubbleData);
                    break;
                case 2:
                    StickBubbles.bubblesThirdPage.Add(bubbleData);
                    break;
                default:
                    Debug.LogWarning("nPages tiene un valor inesperado: " + StickBubbles.nPages);
                    break;
            }
        }
        else
        {
            transform.position = originalPos;
        }
    }



    public bool IsFullyInsideManga()
    {
        PointerEventData pointerData = new PointerEventData(eventSystem)
        {
            position = Input.mousePosition
        };

        var results = new System.Collections.Generic.List<RaycastResult>();
        raycaster.Raycast(pointerData, results);

        foreach (var result in results)
        {
            if (result.gameObject.CompareTag("Manga"))
            {
                RectTransform mangaRect = result.gameObject.GetComponent<RectTransform>();
                RectTransform bubbleRect = GetComponent<RectTransform>();

                if (IsRectTransformFullyInside(bubbleRect, mangaRect))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool IsRectTransformFullyInside(RectTransform inner, RectTransform outer)
    {
        Vector3[] innerCorners = new Vector3[4];
        inner.GetWorldCorners(innerCorners);

        Vector3[] outerCorners = new Vector3[4];
        outer.GetWorldCorners(outerCorners);

        foreach (var corner in innerCorners)
        {
            if (!IsPointInsideRect(corner, outerCorners))
            {
                return false; 
            }
        }

        return true;
    }

    private bool IsPointInsideRect(Vector3 point, Vector3[] rectCorners)
    {
        return point.x >= rectCorners[0].x && point.x <= rectCorners[2].x &&
               point.y >= rectCorners[0].y && point.y <= rectCorners[1].y;
    }
}
