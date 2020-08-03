using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearMovement : MonoBehaviour
{
    public Transform startingLocation;
    public Transform endingLocation;

    [SerializeField] Tree tree;
    [SerializeField] private float movementDuration = 75, cutDownDuration = 5, progress = 0;

     
    void Start()
    {
        StartCoroutine("BearSlowMovement");
    }

    public IEnumerator BearSlowMovement()
    {
        while(progress < movementDuration)
        {
            transform.localPosition = Vector3.Lerp(startingLocation.position, endingLocation.position, progress / movementDuration);
            progress += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(cutDownDuration);
        print("tHeN fAlL tReEsAr");
        tree.Fall();        
    }

}
