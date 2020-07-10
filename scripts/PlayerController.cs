using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 mousePos;
    public bool rodActive, rodCast, reelTime, bufferEnabled;
    public Transform fishSpawnPoint;
    public GameObject bobber;
    Bobber bobComponent;
    public Transform bobberPosition;
    public GameObject fishSprite;
    public int reelAmount;

    public GameObject[] possibleFishes;
    public Fish fishBeingReeled;
    public float castOffset;

    Coroutine fishing;
    Coroutine reeling;


    //Animator Stuff 
    Animator animator;


    public float castTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        bobComponent = bobber.GetComponent<Bobber>();
        Cursor.visible = false;
        rodCast = false;
        rodActive = true;
        bobber.GetComponent<Bobber>().fishDirection = Vector3.zero;
        bobber.SetActive(false);
        bufferEnabled = false;
        animator = GetComponent<Animator>();
    }

    IEnumerator fish()
    {
        rodCast = true;
        yield return new WaitForSeconds(Random.Range(10, 15));
        print("fish");
        reeling = StartCoroutine(startReel());
    }



    IEnumerator startReel()
    {
        //turn on the tooltip to start reeling
        UIManager.current.toolTipAnimator.toggleEnabled(true);

        animator.Play("fishOn");
        //pick which fish is going to be caught 
        //print(possibleFishes.Length - 1);
        int fishIndex = Random.Range(0, 5);
        print(fishIndex);
        fishBeingReeled = possibleFishes[fishIndex].GetComponent<Fish>();
        reelTime = true;
        bobComponent.ToggleReelTime(true);


        yield return new WaitForSeconds(3);


        bobber.GetComponent<Bobber>().fishDirection = Vector3.zero;
        bobber.SetActive(false);

        if (reelAmount > fishBeingReeled.reelRequired)
        {
            spawnFish(possibleFishes[fishIndex]);
        }

        StartCoroutine(enableBuffer());
        reelAmount = 0;
        reelTime = false;
        rodCast = false;

        //turn off the tooltip 
        UIManager.current.toolTipAnimator.toggleEnabled(false);
    }

    IEnumerator enableBuffer()
    {
        animator.Play("idle");
        bufferEnabled = true;
        yield return new WaitForSeconds(1);
        bufferEnabled = false;
    }

    public void spawnFish(GameObject fishy)
    {
        GameObject fish = Instantiate(fishy, fishSpawnPoint.position, Quaternion.identity);
        //fish.transform.rotation = Quaternion.Euler(0, 0, -90);

        //put a fish into the tank as well
        TankManager.current.SpawnFish(fishy.GetComponent<Fish>());
    }


    IEnumerator castRod()
    {

        bobber.GetComponent<Bobber>().idleAnimation.enabled = false;
        animator.Play("Casting");
        yield return new WaitForSeconds(castTime);
        animator.Play("idle");

        bobber.GetComponent<Rigidbody>().velocity = Vector3.zero;
        rodCast = true;
        bobber.transform.position = bobberPosition.position;
        bobber.SetActive(true);

        //random casting vector 
        Vector3 randomOffset = new Vector3(Random.Range(-castOffset, castOffset), Random.Range(-castOffset, castOffset), 2 + Random.Range(-castOffset, castOffset));
        bobber.GetComponent<Rigidbody>().AddForce(randomOffset * 5, ForceMode.Impulse);

        bobComponent.ToggleReelTime(false);
        fishing = StartCoroutine(fish());


    }

    // Update is called once per frame
    void Update()
    {

        Vector3 mouseDelta = mousePos;


        mousePos = Input.mousePosition;
        mousePos.z = 9;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        mouseDelta = mousePos - mouseDelta;

        mouseDelta.y = 0;

        bobber.GetComponent<Rigidbody>().AddForce(mouseDelta, ForceMode.Impulse);


        if (Input.GetMouseButtonDown(0) && !bufferEnabled)
        {
            //states
            //reeling
            //casting and waiting for bite
            //ready to caast 
            if (reelTime)
            {
                //pnt("reel da fishy");
                reelAmount += Random.Range(8, 15);
            }
            else if (!reelTime && rodCast)
            {
                //print("u fucked up ");
                //turn off the bobber 
                rodCast = false;
                bobber.GetComponent<Bobber>().fishDirection = Vector3.zero;

                bobber.SetActive(false);
                if (fishing != null)
                {
                    StopCoroutine(fishing);
                }
                if (reeling != null)
                {
                    StopCoroutine(reeling);
                }

                reelAmount = 0;
                reelTime = false;
            }
            else if (!rodCast && rodActive)
            {

                StartCoroutine(castRod());
            }
            else
            {
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
                if (hit.collider != null)
                {
                    if (hit.collider.tag == "player")
                    {
                        rodActive = true;
                        Cursor.visible = false;
                    }
                }
            }
        }


        if (rodActive)
        {
            Vector3 newPos = mousePos;

            if (newPos.y > -1)
            {
                newPos.y = -1;
            }

            if (newPos.x < -1.3f)
            {
                newPos.x = -1.3f;
            }

            if (newPos.x > 8.2)
            {
                newPos.x = 8.2f;
            }

            transform.position = newPos;
            //print(mousePos);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !rodCast)
        {
            rodActive = false;
            Cursor.visible = true;
        }
    }
}
