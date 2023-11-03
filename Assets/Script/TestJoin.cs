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
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(0.5f);
        Menu.SetActive(true);
        Eventsys.SetSelectedGameObject(First);
    }

    private void OnMouseUp()
    {
        Debug.Log("Check");
        if (Eventsys.currentSelectedGameObject == null)
        {
            Eventsys.SetSelectedGameObject(lastedpoint);
            Debug.Log("Check2");
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Check");
        if(Eventsys.currentSelectedGameObject == null)
        {
            Eventsys.SetSelectedGameObject(lastedpoint);
            Debug.Log("Check2");
        }
    }
}
