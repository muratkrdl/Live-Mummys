using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FloorTrigger : MonoBehaviour
{
    [SerializeField] UnityEvent onEntered;
    [SerializeField] UnityEvent onExited;

    [SerializeField] float exitDelay = 1.5f;

    int entered;

    void OnTriggerEnter(Collider other) 
    {
        if(other.GetComponent<PlayerMovement>() != null)
        {
            onEntered.Invoke();
            entered++;
        }
    }

    void OnTriggerExit(Collider other) 
    {
        if(other.GetComponent<PlayerMovement>() != null)
        {
            entered--;
            if(entered <= 0)
            {
                StartCoroutine(ExitAfterDelay());
            }
        }    
    }

    IEnumerator ExitAfterDelay()
    {
        yield return new WaitForSeconds(exitDelay);
        onExited.Invoke(); 
    }

}
