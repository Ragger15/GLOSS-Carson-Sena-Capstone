using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabControler : MonoBehaviour
{
    [SerializeField] public List<Tab> tabs;
    [SerializeField] public List<GameObject> menus;
    [SerializeField] public Tab selectedTab;
    //[SerializeField] public Color idle;
    //[SerializeField] public Color hover;
    //[SerializeField] public Color clicked;

    public void Subscribe(Tab tab)
    {
        if(tabs == null)
        {
            tabs = new List<Tab>();
        }

        tabs.Add(tab);

    }

    public void Enter(Tab tab)
    {
        Reset();
        //tab.image.tintColor = hover;
        //Debug.Log(tab.image.tintColor);
    }

    public void Exit(Tab tab)
    {
        Reset();
    }

    public void Clicked(Tab tab)
    {
        selectedTab = tab;
        Reset();
        //tab.image.tintColor = clicked;
        int index = tab.transform.GetSiblingIndex();
        for(int i = 0; i < menus.Count; i++)
        {
            if (i == index)
            {
                menus[i].SetActive(true);
            }
            else
            {
                menus[i].SetActive(false);
            }
        }
    }

    public void Reset()
    {
        foreach(Tab tab in tabs)
        {
            //tab.image.tintColor = idle;
        }
    }

}
