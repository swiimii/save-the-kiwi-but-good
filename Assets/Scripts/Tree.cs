using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public EventManager eManager;
    public GameObject kiwi;
    public GameObject postGameScreen;
    public void Fall()
    {
        StartCoroutine("FallTree");
    }
    public IEnumerator FallTree()
    {
        GetComponent<Animator>().SetBool("isFalling", true);
        yield return new WaitForSeconds(.7f);
        kiwi.GetComponent<Animator>().SetBool("treeFalling", true);
        yield return new WaitForSeconds(.5f);
        postGameScreen.SetActive(true);
        //fade screen
        yield return new WaitForSeconds(2f);
        eManager.VictoryCheck();
    }
}
