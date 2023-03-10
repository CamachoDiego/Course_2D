using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 2f;
    public Vector2 direction;
    public float livingTime = 3f;

    public Color start = Color.white;
    public Color end;

    private SpriteRenderer sprite;
    private float startingTime;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();  
    }

    // Start is called before the first frame update
    void Start()
    {
        startingTime = Time.time;
        Destroy(gameObject, livingTime);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = direction.normalized * speed * Time.deltaTime;
        // transform.position = new Vector2(transform.position.x + movement.x, transform.position.y + movement.y);
        transform.Translate(movement);

        float timeSinceStart = Time.time - startingTime;
        float percentage = timeSinceStart / livingTime;

        sprite.color = Color.Lerp(start, end, percentage);
        
    }
}
