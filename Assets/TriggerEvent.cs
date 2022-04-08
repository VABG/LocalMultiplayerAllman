using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField] UnityEvent enterEvent;
    [SerializeField] UnityEvent exitEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (enterEvent != null) enterEvent.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (exitEvent != null) exitEvent.Invoke();
    }
}
