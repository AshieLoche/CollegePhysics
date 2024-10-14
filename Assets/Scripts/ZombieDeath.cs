using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDeath : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
/*    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            anim.SetBool("Stop", false);
            anim.SetTrigger("Dead");
        }
    }*/
    public void Die()
    {
        anim.SetBool("Stop", false);
        anim.SetTrigger("Dead");
    }
}
