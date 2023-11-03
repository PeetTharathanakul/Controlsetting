using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject On;
    [SerializeField] private GameObject Off;
    [SerializeField] private GameObject First;

    public void Switching()
    {
        On.SetActive(true);
        Off.SetActive(false);
        TestJoin.current.Eventsys.SetSelectedGameObject(First);
        TestJoin.current.lastedpoint = First;
    }

}
