using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 1f;
    public float maxX, minX;
    public float waitingTime = 2f;

    private GameObject target;
    private Animator animator;
    private Weapon weapon;

    void Awake()
    {
        animator = GetComponent<Animator>();
        weapon = GetComponentInChildren<Weapon>();
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateTarget();
        StartCoroutine("PatrolToTarget");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void UpdateTarget()
    {
        if (target == null)
        {
            target = new GameObject("Target");
            target.transform.position = new Vector2(minX, transform.position.y);
            transform.localScale=new Vector3 (-1, 1, 1);
            return;
        }
        if (target.transform.position.x == minX)
        {
            target.transform.position = new Vector2(maxX, transform.position.y);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (target.transform.position.x == maxX) { 
            target.transform.position = new Vector2(minX, transform.position.y);
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private IEnumerator PatrolToTarget()
    {
        while (Vector2.Distance(transform.position, target.transform.position) > 0.05f)
        {
            animator.SetBool("Idle", false);
            Vector2 direction = target.transform.position - transform.position;
            float xDirection = direction.x;

            transform.Translate(direction.normalized * speed * Time.deltaTime);
            yield return null;
        }
        Debug.Log("reached");
        transform.position =new Vector2 (target.transform.position.x, transform.position.y);
        UpdateTarget();
        
        animator.SetBool("Idle", true);
        animator.SetTrigger("Shoot");
     
        
        
        Debug.Log("waiting for " + waitingTime + "seconds");
        yield return new WaitForSeconds(waitingTime);

        Debug.Log("waited enough");
        
        StartCoroutine("PatrolToTarget");

       
    } 
    void CanShoot()
        {
            if (weapon!= null)
            {
                weapon.Shoot();
            }

        }
}
