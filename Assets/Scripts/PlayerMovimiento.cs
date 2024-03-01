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

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Voypalla();
        }
    }

    //Funcion que realiza un raycast de la camata a la posicion en la que se situa el raton y le indica a la IA que debe dirigirse hacia la posicion de el raton
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
