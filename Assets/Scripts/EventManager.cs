using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventManager : MonoBehaviour
{
    public abstract void VictoryCheck();
    public abstract void SucceedLevel();
    public abstract void FailLevel();

    
}
