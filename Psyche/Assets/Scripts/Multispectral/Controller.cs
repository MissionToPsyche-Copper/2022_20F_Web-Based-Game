using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField]
    private GameObject circleTarget;

    private int targetCount;

    private Vector2 targetPos;

    private Vector3 circleDirection;

    public CircleCollider2D asteroid;
    [Range(0.0f, 1.0f)]
    public float spawnChance = 0;
    [Range(0.0f, 20.0f)]
    public float spawnInterval = 0;
    private float currentTime = 0;
    public GameObject pivot;
    /// NEED TO FIX THE BOXCOLLIDER SO THE TARGET SPAWNS INSIDE IT
   
    void Start()
    {

    }

    private Vector3 generateDirection()
    {
        circleDirection = new Vector3(Random.value, Random.value, 0);
        if(Random.value > 0.5)
        {
            circleDirection.x *= -1;
        }
        if(Random.value > 0.5)
        {
            circleDirection.y *= -1;
        }
        return circleDirection.normalized * asteroid.radius * (asteroid.gameObject.transform.localScale.x);
    }
    public void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime > spawnInterval)
        {
            if (Random.value < spawnChance)
            {
                GameObject temp = Instantiate(circleTarget);
                temp.transform.parent = pivot.transform;
                temp.transform.position = generateDirection();
            }
            currentTime = 0;
        }
    }
}
