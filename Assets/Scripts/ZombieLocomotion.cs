using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ZombieLocomotion : MonoBehaviour
{

    [SerializeField] GameObject Squash;
    [SerializeField] Animator anim;
    [SerializeField] [Range(0f, 1f)] float speedModifier = 1;

    public static UnityEvent<bool> Walk = new UnityEvent<bool>();
    public static UnityEvent InPosition = new UnityEvent();
    public static UnityEvent<bool> DisplayTitleUI = new UnityEvent<bool>();
    public static UnityEvent ZombieAttack = new UnityEvent();

    private bool move = false, dead = false, attack = false, attacking = false,targetDead = false;
    Vector3 initPos;

    void Start()
    {
        SquashLocomotion.Attack.AddListener(Attack);
        ZombieDeath.DeathState.AddListener(Dead);
        SunflowerDeath.DeathState.AddListener(StopAttack);
        Squash = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        initPos = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && move == false)
        {
            anim.SetBool("Stop", false);
            anim.SetTrigger("Move");
            move = true;
        }
        if (Input.GetKeyDown(KeyCode.Backspace) && Vector3.Distance(transform.position,initPos) > 0.01f)
        {
            ResetZombie();
        }

        if (attack && !attacking && !dead && !targetDead)
        {
            StartCoroutine(IAttack());
        }
    }

    private void FixedUpdate()
    {
       if (move)
       {
            Move();
       }
    }

    void Move()
    {
        if (Vector3.Distance(transform.position, Squash.transform.position) > 25)
        {
            transform.position = Vector3.MoveTowards(transform.position, Squash.transform.position, Time.fixedDeltaTime*speedModifier);
            Walk.Invoke(true);
        }
        else if (Vector3.Distance(transform.position, Squash.transform.position) <= 25)
        {
            move = false;
            anim.SetBool("Stop", true);
            InPosition.Invoke();
            Walk.Invoke(false);
        }
    }

    public void MoveTrigger()
    {
        anim.SetBool("Stop", false);
        anim.SetTrigger("Move");
        move = true;
        DisplayTitleUI.Invoke(false);
    }

    public void ResetZombie()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        transform.position = initPos;
        anim.SetBool("Stop", true);
        if (!dead)
        {
            StopCoroutine(IAttack());
            anim.ResetTrigger("Attack");
            anim.Play("root_Zombie_Idle", -1);
        }
        move = false;
        dead = false;
        attack = false;
        attacking = false;
        targetDead = false;
    }

    private void Dead()
    {
        dead = true;
    }

    private void Attack()
    {
        attack = true;
    }

    private void StopAttack()
    {
        attack = false;
        attacking = false;
        targetDead = true;
        StopCoroutine(IAttack());
        anim.ResetTrigger("Attack");
    }

    private IEnumerator IAttack()
    {
        attacking = true;
        anim.SetTrigger("Attack");
        anim.Play("root|Zombie_Attack", -1);
        ZombieAttack.Invoke();
        yield return new WaitForSeconds(3f);
        attacking = false;
    }

}