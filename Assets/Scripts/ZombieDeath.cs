using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZombieDeath : MonoBehaviour
{

    public static UnityEvent DeathState = new UnityEvent();
    private Animator anim;

    void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    public void Die()
    {
        anim.SetBool("Stop", false);
        anim.SetTrigger("Dead");
        DeathState.Invoke();
    }

}
