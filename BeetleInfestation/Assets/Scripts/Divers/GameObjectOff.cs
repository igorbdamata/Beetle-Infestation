using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameObjectOff : MonoBehaviour
{
    public float time;
    SpriteRenderer spr;
    Image img;
    Color color;
    float valueToReduce;

    void Start()
    {
        if (GetComponent<SpriteRenderer>()) { spr = GetComponent<SpriteRenderer>(); }
        else if (GetComponentInChildren<SpriteRenderer>()) { spr = GetComponentInChildren<SpriteRenderer>(); }
        else { img = GetComponent<Image>(); }
        color = spr != null ? spr.color : img.color;
        StartCoroutine(ReduceAlpha());
    }

    public IEnumerator ReduceAlpha()
    {
        valueToReduce = time / 10;
        while (color.a >= 0)
        {
            color = new Color(color.r, color.g, color.b, color.a - valueToReduce);
            if (spr != null) { spr.color = color; }
            else { img.color = color; }
            yield return new WaitForSeconds(valueToReduce);
        }
        Destroy(gameObject);
    }
}
