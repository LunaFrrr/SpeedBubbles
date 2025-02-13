using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float velocidadParallax = 0.1f; 
    private float suavizado = 0.1f;

    private Vector3 ultimaPosicion;

    void Start()
    {
        ultimaPosicion = Input.mousePosition;
    }

    void Update()
    {
        Vector3 posicionMouse = Input.mousePosition;
        Vector3 movimientoMouse = posicionMouse - ultimaPosicion;

        Vector3 movimientoParallax = new Vector3(movimientoMouse.x, movimientoMouse.y, 0) * velocidadParallax;

        transform.position = Vector3.Lerp(transform.position, transform.position - movimientoParallax, suavizado);

        ultimaPosicion = posicionMouse;
    }
}
