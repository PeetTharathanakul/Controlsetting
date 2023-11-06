using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TestJoin : MonoBehaviour
{
    [SerializeField] private GameObject Menu;
    [SerializeField] private GameObject First;
    public GameObject lastedpoint;

    public EventSystem Eventsys;

    public static TestJoin current;

    private void Awake()
    {
        current = this;
    }

    void Start()
    {
        StartCoroutine(Countdown());
        StartCoroutine(Highlight());
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(0.5f);
        Menu.SetActive(true);
        Eventsys.SetSelectedGameObject(First);
    }

    IEnumerator Highlight()
    {
        while (true)
        {
            if (Eventsys.currentSelectedGameObject == null)
            {
                Eventsys.SetSelectedGameObject(lastedpoint);
            }
            yield return null;
        }
    }

}
