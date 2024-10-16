using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PeaFire : MonoBehaviour
{

    [SerializeField] private GameObject _peaPF;
    [SerializeField] private Transform _peaSpawnMarker;
    [SerializeField] private float _velocity;
    private bool _isFiring;

    public static UnityEvent FirePea = new UnityEvent();

    private void Update()
    {
        if (!_isFiring && Physics.Raycast(transform.position, transform.forward, 10f, 1 << LayerMask.NameToLayer("Squash")))
        {
            StartCoroutine(IPeaFire());
        }
    }

    private IEnumerator IPeaFire()
    {
        _isFiring = true;
        GameObject _peaClone = Instantiate(_peaPF, _peaSpawnMarker, true);
        _peaClone.transform.position = _peaSpawnMarker.position;
        _peaClone.GetComponent<Rigidbody>().velocity = transform.forward * _velocity;
        FirePea.Invoke();
        yield return new WaitForSeconds(2f);
        _isFiring = false;
    }

}