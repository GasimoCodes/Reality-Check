using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    private void Start()
    {
        // Lets fade in
        ScreenFader.Instance.FadeIn(1f);
    }

    public GameObject[] Tabs;

    /// <summary>
    /// Sets selected tab to visible
    /// </summary>
    /// <param name="tab"></param>
    public void setTab(GameObject tab)
    {
        foreach (var item in Tabs)
        {
            item.SetActive(false);
        }

        ScreenFader.Instance.FadeIn(0.5f);

        tab.SetActive(true);
    }


    public void Quit()
    {
        Application.Quit();
    }


}
