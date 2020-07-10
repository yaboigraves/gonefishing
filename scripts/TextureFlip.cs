using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureFlip : MonoBehaviour
{
    SpriteRenderer sprite;
    public float flipTime;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(flipSprite());
    }

    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerator flipSprite()
    {
        sprite.flipX = !sprite.flipX;
        yield return new WaitForSeconds(flipTime);
        StartCoroutine(flipSprite());
    }
}
