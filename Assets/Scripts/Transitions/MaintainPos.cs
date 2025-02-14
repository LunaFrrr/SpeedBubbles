using UnityEngine;

public class MaintainPos : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Vector3 posOriginal;
    Vector3 offSet;
    Vector3 newPos;
    public GameObject parent;
    void Start()
    {
        posOriginal = new Vector3(-1500,-500,0);

        
    }

    // Update is called once per frame
    void Update()
    {

        offSet = posOriginal + parent.transform.position;

        if (parent.transform.position.x < 0)
        {
            newPos = new Vector3(-offSet.x, -offSet.y, 0f);
            transform.position = parent.transform.position + newPos;
        }
        else
        {
            newPos = new Vector3(offSet.x, offSet.y, 0f);
            transform.position = parent.transform.position - newPos;
        }



    }
}
