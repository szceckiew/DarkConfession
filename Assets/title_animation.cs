using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpriteAnimation : MonoBehaviour
{
    public Image m_Image;

    public Sprite[] m_SpriteArray;
    public Sprite[] m_SpriteArray2;
    public float m_Speed = .02f;
    public float rest_time = 5f;

    public int m_IndexSprite = 0;
    Coroutine m_CorotineAnim;
    bool IsDone;
    public void Func_PlayUIAnim()
    {
        StartCoroutine(Func_PlayAnimUI());
    }

    IEnumerator Rest() 
    {
        yield return new WaitForSeconds(rest_time);
        m_CorotineAnim = StartCoroutine(Func_PlayAnimUI2());
    }


    IEnumerator Func_PlayAnimUI()
    {
        yield return new WaitForSeconds(m_Speed);
        m_Image.sprite = m_SpriteArray[m_IndexSprite];
        m_IndexSprite += 1;
        if (m_IndexSprite >= m_SpriteArray.Length)
        {
            m_IndexSprite = 0;
            m_CorotineAnim = StartCoroutine(Func_PlayAnimUI2());
        } else
        {
            m_CorotineAnim = StartCoroutine(Func_PlayAnimUI());
        }
            
    }

    IEnumerator Func_PlayAnimUI2()
    {
        yield return new WaitForSeconds(m_Speed);
        m_Image.sprite = m_SpriteArray2[m_IndexSprite];
        m_IndexSprite += 1;
        if (m_IndexSprite >= m_SpriteArray2.Length)
        {
            m_IndexSprite = 0;
            m_CorotineAnim = StartCoroutine(Rest());
        } else
        {
            m_CorotineAnim = StartCoroutine(Func_PlayAnimUI2());
        }
    }

    private void Start()
    {
        Func_PlayUIAnim();
    }
}