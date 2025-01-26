using UnityEngine;
using UnityEngine.EventSystems;

public class PlayAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator m_Animator;
    private string m_Animation_Name;
    public string animationStack1 = "BubbleHover";
    public string animationStack2 = "BubbleHover2"; 
    public string animationStack3 = "BubbleHover3";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject.name == "Stack1")  
        {
            m_Animation_Name = animationStack1;
        }
        else if (gameObject.name == "Stack2" || gameObject.name == "Stack4")
        {
            m_Animation_Name = animationStack2;
        }
        else if (gameObject.name == "Stack3" || gameObject.name == "Stack5")
        {
            m_Animation_Name = animationStack3;
        }

        m_Animator.SetBool("hover", true);
        m_Animator.Play(m_Animation_Name, 0, 0f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_Animator.SetBool("hover", false);
       
    }
}

