using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    public Vector3 cloudDirection;
    public Vector3 cloudOffset;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        cloudDirection += new Vector3(Random.Range(-cloudOffset.x, cloudOffset.x), Random.Range(-cloudOffset.y, cloudOffset.y), Random.Range(-cloudOffset.z, cloudOffset.z));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + cloudDirection, speed * Time.deltaTime);
    }
}
