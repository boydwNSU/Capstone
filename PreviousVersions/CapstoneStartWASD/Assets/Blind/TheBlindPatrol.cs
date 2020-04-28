using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TheBlindPatrol : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;

    public Transform[] moveSpots;

    private int randomSpots;

    void Start()
    {
        randomSpots = Random.Range(0, moveSpots.Length);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpots].position, speed * Time.deltaTime);

        if(Vector2.Distance(transform.position, moveSpots[randomSpots].position) < 0.2f)
        {
            if(waitTime <= 0)
            {
                randomSpots = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
