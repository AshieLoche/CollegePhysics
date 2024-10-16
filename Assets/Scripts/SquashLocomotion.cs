using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SquashLocomotion : MonoBehaviour
{

    [SerializeField] float horizontalDisplacement, verticalDisplacement;
    [SerializeField] float timeOfFlight, timeofDescent;
    [SerializeField] float horizontalVelocity;
    [SerializeField] bool started = false, inPeak = false, canJump = false, canLaunch = false, startCount;
    [SerializeField] GameObject PromptPanel;
    [SerializeField] TextMeshProUGUI PromptTxt;

    public static UnityEvent InPeak = new UnityEvent();
    public static UnityEvent DescentThreeQuarters = new UnityEvent();
    public static UnityEvent Taunt = new UnityEvent();
    public static UnityEvent<bool> DisplayTitleUI = new UnityEvent<bool>();
    public static UnityEvent<float> ResetUI = new UnityEvent<float>();
    public static UnityEvent<float> Prompt = new UnityEvent<float>();
    public static UnityEvent<float> HorizontalVelocity = new UnityEvent<float>();
    public static UnityEvent<float> AttackTime = new UnityEvent<float>();
    public static UnityEvent<float> AttackRange = new UnityEvent<float>();

    private Rigidbody SquashRb;
    private Vector3 initPos;

    void Start()
    {
        Physics.gravity.Set(0,-9.8f,0);
        SquashRb = GetComponent<Rigidbody>();
        initPos = transform.position;
        horizontalVelocity = 7.5f;
        HorizontalVelocity.Invoke(horizontalVelocity);
        UIManager.Alerted.AddListener(StartJump);
    }

    void Update()
    {
        if (!started)
        {
            transform.position = initPos;
            SquashRb.velocity = Vector3.zero;
        }

        if (Mathf.CeilToInt(transform.position.y) == 30)
        {
            inPeak = true;
        }
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            ResetSquash();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ResetSquash();
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeVel();
        }

    }

    private void FixedUpdate()
    {
        if (canJump)
        {
            SquashRb.velocity = new Vector3(0, Mathf.Sqrt(2 * 9.8f * 30));
            canJump = false;
        }
        if (SquashRb.velocity.y < 0 && canLaunch)
        {
            SquashRb.velocity = new Vector3(horizontalVelocity,SquashRb.velocity.y);
            canLaunch = false;
        }
        if (SquashRb.velocity.y < 0 && inPeak)
        {
            timeofDescent += Time.fixedDeltaTime;
        }
        if (startCount)
        {
            timeOfFlight += Time.fixedDeltaTime;
            AttackTime.Invoke(Mathf.Round(timeOfFlight * 100) / 100);
            AttackRange.Invoke(Mathf.Round(SquashRb.position.x * 100) / 100);
        }

        if (timeOfFlight > 2.6f)
        {
            InPeak.Invoke();
        }

        if (timeofDescent > 1.8525f)
        {
            DescentThreeQuarters.Invoke();
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
                Taunt.Invoke();
                DisplayTitleUI.Invoke(true);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<ZombieDeath>().Die();
        }
    }
    void StartJump()
    {
        started = true;
        canJump = true;
        startCount = true;
        canLaunch = true;
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
    public void ChangeVel()
    {
        horizontalVelocity = horizontalVelocity == 7.5f ? 10.12f : 7.5f;
        Prompt.Invoke(horizontalVelocity);
    }
    public void ResetTrigger()
    {
        ResetSquash();
        ResetUI.Invoke(horizontalVelocity);
    }

}