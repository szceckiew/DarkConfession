using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaDetection : MonoBehaviour
{
    public bool winnable = false;
    public GameObject player;

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.name == player.name && winnable == true)
        {
            Debug.Log("Player entered area");
            Debug.Log("You Win!");
            SceneManager.LoadScene(2);
        }
    }

}
