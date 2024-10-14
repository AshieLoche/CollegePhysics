using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZombieLocomotion : MonoBehaviour
{
    [SerializeField] GameObject Squash;
    [SerializeField] Animator anim;
    [SerializeField]
    [Range(0f, 1f)] float speedModifier = 1;

    public static UnityEvent InPosition = new UnityEvent();

    private bool move = false;
    Vector3 initPos;

    // Start is called before the first frame update
    void Start()
    {
        Squash = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        initPos = transform.position;
    }

    // Update is called once per frame
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
    }
    private void FixedUpdate()
    {
       if (move)
        {
            Move();
        }
    }
    public void Move()
    {
        if (Vector3.Distance(transform.position, Squash.transform.position) > 25)
        {
            transform.position = Vector3.MoveTowards(transform.position, Squash.transform.position, Time.fixedDeltaTime*speedModifier);
        }
        else if (Vector3.Distance(transform.position, Squash.transform.position) <= 25)
        {
            move = false;
            anim.SetBool("Stop", true);
            InPosition.Invoke();
            Debug.Log(InPosition.ToString());
        }
    }
    private void ResetZombie()
    {
        transform.position = initPos;
        anim.SetBool("Stop", true);
        move = false;
    }
}
