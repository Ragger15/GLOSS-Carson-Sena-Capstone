using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Pin
{
    [SerializeField] private string title;

    public string Title
    {
        get { return title; }
        set { title = value; }
    }

    public void SetTitle(string ttle)
    {
        Title = ttle;
    }

    [SerializeField] private string message;

    public string Messgae
    {
        get { return message; }
        set { message = value; }
    }

    public void SetMessage(string mssage)
    {
        message = mssage;
    }
}
