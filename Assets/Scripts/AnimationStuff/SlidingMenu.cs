using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingMenu : MonoBehaviour
{
    [SerializeField] public GameObject MenuPanel;
    [SerializeField] public int prevClicker;
    [SerializeField] public ColorPicker cp;

    public void SlideMenu(int clicker)
    {
        if (MenuPanel != null)
        {
            Animator animator = MenuPanel.GetComponent<Animator>();
            if (animator != null)
            {
                bool open = animator.GetBool("isOpen");
                if (open)
                {
                    animator.SetBool("isOpen", !open);
                    cp.enabled = !cp.enabled;
                }
                else if(prevClicker == clicker)
                {
                    animator.SetBool("isOpen", !open);
                    cp.enabled = !cp.enabled;
                }
                prevClicker = clicker;
            }
        }
    }

}
