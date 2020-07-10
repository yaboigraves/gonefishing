using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimate : MonoBehaviour
{
    Image image;
    public Sprite[] frames;
    int currentFrame;

    public float animationTime;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(animate());
        toggleEnabled(false);
    }


    IEnumerator animate()
    {
        yield return new WaitForSeconds(animationTime);
        currentFrame++;
        image.sprite = frames[currentFrame % 2];

        StartCoroutine(animate());
    }

    public void toggleEnabled(bool enabled)
    {
        image.enabled = enabled;
    }


}
