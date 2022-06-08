using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuadro : MonoBehaviour
{
    private Renderer r;
    private Transform root;
    private void Start()
    {
        root = Camera.main.transform;
        r = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (r.isVisible)
        {
            if (Physics.Raycast(root.position, transform.position - root.position, out RaycastHit hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    print("isVisible & hasDirectSight");
                }
            }
        }


        
        if (other.gameObject.CompareTag("Floor"))
        {

        }   
        //Esto detecta cuando se cae el objeto y choca con el suelo.
    }
}
