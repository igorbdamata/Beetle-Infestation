using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnimationWithRandomFrames : MonoBehaviour
{
    [SerializeField] private Sprite[] frames;
    [SerializeField] private float intervalFrames;
    SpriteRenderer spriteRenderer;
    Image image;
    int currentFrame;

    void Start()
    {
        if (GetComponent<SpriteRenderer>()) { spriteRenderer = GetComponent<SpriteRenderer>(); }
        else { image = GetComponent<Image>(); }
        StartCoroutine("ChangeFrame");
    }

    IEnumerator ChangeFrame()
    {
        while (true)
        {
            int initialFrame = currentFrame;
            currentFrame = Random.Range(0, frames.Length);
            while (currentFrame == initialFrame) { currentFrame = Random.Range(0, frames.Length); }
            if (GetComponent<SpriteRenderer>()) { spriteRenderer.sprite = frames[currentFrame]; }
            else { image.sprite = frames[currentFrame]; }
            yield return new WaitForSeconds(intervalFrames);
        }
    }
}
