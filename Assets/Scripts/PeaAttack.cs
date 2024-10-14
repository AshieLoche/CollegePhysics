using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaAttack : MonoBehaviour
{

    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, 10f, 1 << LayerMask.NameToLayer("Squash")))
        {

        }
    }

}
