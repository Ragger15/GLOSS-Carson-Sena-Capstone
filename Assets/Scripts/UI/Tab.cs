using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class Tab : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField] public TabControler tabControler;
    //[SerializeField] public Image image;

    public void OnPointerClick(PointerEventData eventData)
    {
        tabControler.Clicked(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabControler.Enter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabControler.Exit(this);
    }

    void Start()
    {
        //image = GetComponent<Image>();
        tabControler.Subscribe(this);
    }
}
