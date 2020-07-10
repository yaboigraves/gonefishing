using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdFlight : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 direction;
    float xOffset = 3;
    public float speed;
    void Start()
    {
        direction = new Vector3(xOffset + Random.Range(-direction.x, direction.x), Random.Range(-direction.y, direction.y), Random.Range(-direction.z, direction.z));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "birdEnd")
        {
            Destroy(this.gameObject);
        }
    }
}
