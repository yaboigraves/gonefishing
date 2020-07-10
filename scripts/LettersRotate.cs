using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LettersRotate : MonoBehaviour
{

    Quaternion startingRotation, nextRotation;
    float rotationRange = 0.075f;
    // Start is called before the first frame update
    private void Awake()
    {
        startingRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z + Random.Range(-rotationRange * 2, rotationRange), transform.rotation.w);


        nextRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z + Random.Range(-rotationRange, rotationRange * 2), transform.rotation.w);


    }
    void Start()
    {
        transform.rotation = startingRotation;

        StartCoroutine(switchRotation());

    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator switchRotation()
    {
        yield return new WaitForSeconds(1 + Random.Range(-0.1f, 0.1f));
        if (transform.rotation == startingRotation)
        {
            transform.rotation = nextRotation;
        }
        else
        {
            transform.rotation = startingRotation;
        }

        StartCoroutine(switchRotation());
    }
}
