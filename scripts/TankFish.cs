using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//middle of the tank for y is 10 

public class TankFish : MonoBehaviour
{
    TankManager tankManager;

    Rigidbody rb;


    public LeanTweenType tweenType;
    public float speed;
    public Vector3 targetDest;

    SpriteRenderer render;


    [SerializeField]
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        // GenerateRandomDestination();
        tankManager = transform.parent.GetComponent<TankManager>();
        targetDest = tankManager.generateFishDest();
        render = GetComponent<SpriteRenderer>();
        RandomizeDirection();

        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        transform.Translate(direction * Time.deltaTime);
        //rb.MovePosition(transform.position + direction);

        if (direction.x > 0)
        {
            render.flipX = true;
        }
        else
        {
            render.flipX = false;
        }


    }

    void RandomizeDirection()
    {
        direction = new Vector3(Random.Range(-speed, speed), Random.Range(-speed, speed), 0);
        direction.x *= 3;

    }





    private void OnCollisionEnter(Collision other)
    {
        print("oh shit");

        //RandomizeDirection();
        direction *= -1;
    }
}
