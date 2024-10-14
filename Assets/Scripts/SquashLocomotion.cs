using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SquashLocomotion : MonoBehaviour
{
    [SerializeField] float horizontalDisplacement, verticalDisplacement;
    [SerializeField] float timeOfFlight, timeofDescent;
    [SerializeField] float horizontalVelocity;
    [SerializeField] bool inPeak = false, canJump = false, canLaunch = false, startCount;

    public static UnityEvent InPeak = new UnityEvent();
    public static UnityEvent DescentHalfTime = new UnityEvent();

    private Rigidbody SquashRb;
    private Vector3 initPos;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity.Set(0,-9.8f,0);
        SquashRb = GetComponent<Rigidbody>();
        initPos = transform.position;
        ZombieLocomotion.InPosition.AddListener(StartJump);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.CeilToInt(transform.position.y) == 30)
        {
            inPeak = true;
            InPeak.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            ResetSquash();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ResetSquash();
        }

    }
    private void FixedUpdate()
    {
        if(canJump)
        {
            SquashRb.velocity = new Vector3(0, Mathf.Sqrt(2 * 9.8f * 30));
            canJump = false;
        }
        if (SquashRb.velocity.y < 0 && canLaunch)
        {
            SquashRb.velocity = new Vector3(7.5f,SquashRb.velocity.y);
            canLaunch = false;
        }
        if (SquashRb.velocity.y < 0 && inPeak)
        {
            timeofDescent += Time.fixedDeltaTime;
        }
        if (startCount)
        {
            timeOfFlight += Time.fixedDeltaTime;
        }

        if (timeofDescent > 1.235f)
        {
            DescentHalfTime.Invoke();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (inPeak)
        {
            /*SquashRb.constraints = RigidbodyConstraints.FreezeAll;*/
            if (collision.gameObject.tag == "Ground")
            {
                startCount = false;
                inPeak = false;
            }
        }
    }
    void StartJump()
    {
        canJump = true;
        startCount = true;
        canLaunch = true;
        Debug.Log("Start Jumping!");
    }
    void ResetSquash()
    {
        timeofDescent = 0;
        timeOfFlight = 0;
        SquashRb.velocity = Vector3.zero;
        transform.position = initPos;
        /*        SquashRb.constraints = RigidbodyConstraints.None;
                SquashRb.constraints = RigidbodyConstraints.FreezeRotationX;
                SquashRb.constraints = RigidbodyConstraints.FreezeRotationY;
                SquashRb.constraints = RigidbodyConstraints.FreezeRotationZ;
                SquashRb.constraints = RigidbodyConstraints.FreezePositionZ;*/
    }
}
