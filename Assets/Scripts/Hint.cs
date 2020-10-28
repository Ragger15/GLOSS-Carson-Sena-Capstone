using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Hint
{
    [SerializeField] private string title;

    public string Title
    {
        get { return title; }
        set { title = value; }
    }

    [SerializeField] private string message;

    public string Messgae
    {
        get { return message; }
        set { message = value; }
    }
}
