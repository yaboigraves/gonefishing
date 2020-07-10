using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{

    public Transform directionalLight;
    public float sunsetSpeed;
    public GameObject[] birds;

    public Transform birdSpawnPos;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnBirds());
    }

    // Update is called once per frame
    void Update()
    {
        directionalLight.Rotate(sunsetSpeed * Time.deltaTime, 0, 0);
    }


    IEnumerator spawnBirds()
    {

        GameObject bird;

        int numBirds = Random.Range(3, 6);
        for (int i = 0; i < numBirds; i++)
        {
            bird = birds[Random.Range(0, 3)];
            Vector3 offset = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5));
            GameObject birdy = Instantiate(bird, birdSpawnPos.position, Quaternion.identity);
            birdy.GetComponent<Animator>().speed = Random.Range(0.2f, 0.35f);
        }

        yield return new WaitForSeconds(Random.Range(10, 30));

        StartCoroutine(spawnBirds());
    }
}
