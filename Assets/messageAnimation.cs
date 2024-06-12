using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class messageAnimation : MonoBehaviour
{
    public bool inMenu = true;
    public Image image;
    
    public Sprite message;
    public Sprite noMessage;
    public float waitTime = .02f;
    public float showTime = .02f;
    public float hideTime = .02f;

    Coroutine CorotineAnim;

    IEnumerator WaitCorotine()
    {
        yield return new WaitForSeconds(waitTime);
        CorotineAnim = StartCoroutine(ShowCorotine());
    }

    IEnumerator ShowCorotine()
    {
        image.sprite = message;
        yield return new WaitForSeconds(showTime);
        CorotineAnim = StartCoroutine(HideCorotine());
    }

    IEnumerator HideCorotine()
    {
        image.sprite = noMessage;
        yield return new WaitForSeconds(hideTime);
        CorotineAnim = StartCoroutine(ShowCorotine());
    }

    void Start()
    {
        CorotineAnim = StartCoroutine(WaitCorotine());
    }

    private void Update()
    {
        if (Input.anyKeyDown && inMenu == true)
        {
            inMenu = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
