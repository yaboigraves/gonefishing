using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterOpacityOcillate : MonoBehaviour
{
    //true is sin, false is cos
    public bool sinOrCos;

    public float speed;
    SpriteRenderer[] spriteRenderers;

    public float updateTime;

    private void Awake()
    {
        spriteRenderers = new SpriteRenderer[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            spriteRenderers[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
        }


    }

    void Start()
    {
        StartCoroutine(updateOpacity());
    }

    // Start is called before the first frame update


    // Update is called once per frame
    void updateDaShit()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            float randAlpha;
            if (sinOrCos)
            {
                randAlpha = 0.15f + (Mathf.Abs(Mathf.Sin(Time.time * speed))) / 2;
            }
            else
            {
                randAlpha = 0.15f + (Mathf.Abs(Mathf.Cos(Time.time * speed))) / 2;

            }

            spriteRenderers[i].color = new Color(spriteRenderers[i].color.r, spriteRenderers[i].color.g, spriteRenderers[i].color.b, randAlpha);
        }
    }

    IEnumerator updateOpacity()
    {
        updateDaShit();
        yield return new WaitForSeconds(updateTime);

        StartCoroutine(updateOpacity());

    }
}
