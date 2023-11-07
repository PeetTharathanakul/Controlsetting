using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public static GameMenu current;
    public bool Isup;

    [SerializeField] private GameObject Menuobj;
    [SerializeField] private GameObject lastedpoint;
    [SerializeField] private EventSystem Eventsys;

    private void Awake()
    {
        current = this;
    }

    public void OpenMenu()
    {
        if (!Menuobj.active) 
        {
            Menuobj.SetActive(true);
            Isup = true;
        }
        else
        {
            Menuobj.SetActive(false);
            Isup = false;
        }
        
    }

    private void OnEnable()
    {
        Eventsys.SetSelectedGameObject(lastedpoint);
        StartCoroutine(Highlight());
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

    public void Loading()
    {
        SceneManager.LoadScene(0);
    }
}
