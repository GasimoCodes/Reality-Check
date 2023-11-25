using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEnterEvent : MonoBehaviour
{
    public UnityEvent OnEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            OnEnter.Invoke();
        }
    }
}
