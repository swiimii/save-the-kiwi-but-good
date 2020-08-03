using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class Level1Manager : EventManager
{
    [SerializeField] Trampoline trampoline;
    [SerializeField] GameObject postLossButton;
    [SerializeField] GameObject player;
    public override void VictoryCheck()
    {

        if(trampoline.isRepaired)
        {
            SucceedLevel();
        }
        else
        {
            FailLevel();
        }
    }
    public override void SucceedLevel()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public override void FailLevel()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        player.SetActive(false);
        postLossButton.SetActive(true);
        print("Failure.");
    }

    public IEnumerator DelayedVictoryCheck()
    {
        yield return new WaitForSeconds(2);
    }
}
