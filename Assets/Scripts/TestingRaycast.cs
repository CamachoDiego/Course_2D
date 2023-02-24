using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingRaycast : MonoBehaviour
{
    private Animator animator;
    private Weapon weapon;

    void Awake()
    {
        animator = GetComponent<Animator>();
        weapon = GetComponent<Weapon>();        
    }
    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("Idle", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Shoot");
        }
        
    }
    void CanShoot()
    {
        if (weapon != null)
        {
            StartCoroutine(weapon.ShootWithRaycast());
        }
      
    }
}
