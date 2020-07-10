using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour
{

    //gameobject 
    public SpriteRenderer idleAnimation;

    public float fishSpeed;
    public Vector3 fishDirection;

    Rigidbody rb;

    public SpriteRenderer spriteRenderer;
    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ToggleReelTime(false);
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        sprite.enabled = false;
        idleAnimation.enabled = false;
        rb = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        rb.AddForce(fishDirection);
    }

    public void ToggleReelTime(bool timeToReel)
    {
        if (timeToReel)
        {
            //spriteRenderer.color = new Color(0, 255, 0);
            idleAnimation.enabled = false;
            sprite.enabled = true;

            //pick a direction for the fish to start moving
            fishDirection = new Vector3(fishSpeed / 2 + Random.Range(-fishSpeed, fishSpeed), 0, fishSpeed / 2 + Random.Range(-fishSpeed, fishSpeed));
        }
        else
        {
            //spriteRenderer.color = new Color(255, 0, 0);

            if (sprite != null)
            {
                sprite.enabled = false;
            }

        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "bobberFloor")
        {
            idleAnimation.enabled = true;
        }
    }
}
