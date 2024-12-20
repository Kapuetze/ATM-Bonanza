using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public string m_Tag;
    public UnityEvent m_OnEnter = new UnityEvent();
    public UnityEvent m_OnExit = new UnityEvent();


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(m_Tag))
        {
            m_OnEnter.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(m_Tag))
        {
            m_OnExit.Invoke();
        }
    }
}
