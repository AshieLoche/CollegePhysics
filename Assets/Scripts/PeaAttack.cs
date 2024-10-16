using System.Collections;
using UnityEngine;

public class PeaAttack : MonoBehaviour
{

    [SerializeField] private GameObject _peaPF;
    [SerializeField] private Transform _peaSpawnMarker;
    [SerializeField] private float _velocity;
    private bool isFiring;

    private void Update()
    {
        if (!isFiring && Physics.Raycast(transform.position, transform.forward, 10f, 1 << LayerMask.NameToLayer("Squash")))
        {
            StartCoroutine(IFirePea());
        }
    }

    private IEnumerator IFirePea()
    {
        isFiring = true;
        GameObject _peaClone = Instantiate(_peaPF, _peaSpawnMarker, true);
        _peaClone.transform.position = _peaSpawnMarker.position;
        _peaClone.GetComponent<Rigidbody>().velocity = transform.forward * _velocity;
        yield return new WaitForSeconds(2f);
        isFiring = false;
    }

}