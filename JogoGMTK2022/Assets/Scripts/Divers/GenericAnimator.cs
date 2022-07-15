using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericAnimator : MonoBehaviour
{
    public List<Sprite> sprites { get; set; } = new List<Sprite>();
    public int currentSprite;
    private SpriteRenderer sprRenderer;
    private void Start()
    {
        sprRenderer = GetComponent<SpriteRenderer>();        
    }

    private void Update()
    {
        sprRenderer.sprite = sprites[currentSprite];        
    }
}
