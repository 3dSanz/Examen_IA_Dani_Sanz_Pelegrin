using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovimiento : MonoBehaviour
{
    NavMeshAgent _jugadorAgente;
    void Start()
    {
        _jugadorAgente = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Voypalla();
        }
    }

    void Voypalla()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            _jugadorAgente.destination = hit.point;
        }
    }
}
