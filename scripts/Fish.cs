using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    bool lowering = false;
    public Transform spriteEndSpot;

    public int reelRequired;
    Vector3 spriteStartSpot;

    public LeanTweenType tweenType;
    public float travelTime;

    public Sprite tankSprite;
    // Start is called before the first frame update
    void Start()
    {
        spriteStartSpot = transform.position;
        spriteEndSpot = GameObject.FindGameObjectWithTag("fishend").transform;
        LeanTween.moveY(gameObject, spriteEndSpot.position.y, travelTime).setEase(tweenType);
        StartCoroutine(lowerFish());
    }

    // Update is called once per frame
    void Update()
    {
        if (lowering && transform.position == spriteStartSpot)
        {
            //spawn the fish into the ta
            //TankManager.current.SpawnFish(this);
            lowering = false;
        }
    }

    IEnumerator lowerFish()
    {
        yield return new WaitForSeconds(travelTime + 3);
        LeanTween.moveY(gameObject, spriteStartSpot.y, travelTime).setEase(tweenType);
        lowering = true;
    }


}
