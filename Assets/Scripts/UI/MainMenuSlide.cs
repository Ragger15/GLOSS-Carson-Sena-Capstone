using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSlide : MonoBehaviour
{

    [SerializeField] RectTransform Menu;
    [SerializeField] float time;

    public void Slide()
    {
        Menu.gameObject.SetActive(true);
        StartCoroutine(nameof(Move));
    }

    private IEnumerator Move()
    {
        Vector3 tstart = Menu.transform.localPosition;
        Vector3 tend = new Vector3(0,0,0);

        for (float t = 0; t < time; t += Time.deltaTime)
        {
            Menu.transform.localPosition = Vector3.Lerp(tstart, tend, t / time);
            yield return null;
        }
        Menu.transform.localPosition = tend;
    }

}
