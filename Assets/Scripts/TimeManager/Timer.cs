using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        StarTime(other.GetComponent<ITimeable>());
    }

    protected void StarTime(ITimeable timeable)
    {
        //timeable.StartTimeCoroutine();
        //timeable.StopAllCoroutines();
    }
}
