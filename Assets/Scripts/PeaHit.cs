using UnityEngine;
using UnityEngine.Events;

public class PeaHit : MonoBehaviour
{

    public static UnityEvent<string> HitPea = new UnityEvent<string>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HitPea.Invoke(name);
            Destroy(gameObject);
        }
    }

}