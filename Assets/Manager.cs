using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] private Rigidbody[] rbs;
    [SerializeField] private Rigidbody table;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        CuadroCaer();

        //yield return new WaitForSeconds(1f);
        //MesaVolar();
    }

    private void CuadroCaer()
    {
        foreach (var rb in rbs)
        {
            rb.useGravity = true;
            rb.isKinematic = false;
        }
        // Esto hace caer los cuadros
    }

    private void MesaVolar()
    {
        table.AddForce(Vector3.up * 250f);
        // Esto manda la mesa a volar.
    }
}
