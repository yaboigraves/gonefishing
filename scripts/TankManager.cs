using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankManager : MonoBehaviour
{
    public static TankManager current;
    public GameObject fish;
    public Vector3 randomDestRange;
    // Start is called before the first frame update
    void Awake()
    {
        current = this;
    }

    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(new Vector3(0, 10f, 0) * Time.deltaTime);
    }

    public Vector3 generateFishDest()
    {
        Vector3 fishDest = new Vector3(transform.position.x + Random.Range(-randomDestRange.x, randomDestRange.x), transform.position.y + Random.Range(-randomDestRange.y, randomDestRange.y), transform.position.z + Random.Range(-randomDestRange.z, randomDestRange.z));
        return fishDest;
    }

    public void SpawnFish(Fish fishToSpawn)
    {

        GameObject fishy = Instantiate(fish, transform.position + Vector3.up, transform.rotation, transform);
        fishy.GetComponent<SpriteRenderer>().sprite = fishToSpawn.tankSprite;
    }
}
