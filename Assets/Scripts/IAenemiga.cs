using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAenemiga : MonoBehaviour
{
    //Definicion de la State Machine, indicando los tres estados por los que pasara la IA
    enum State
    {
        Patrolling,
        Chasing,
        Attacking
    }

    State _currentState;
    NavMeshAgent _agent;

    //Transform para en la funcion Awake hacer que la IA busque todo objeto con el Tag "Player"
    Transform _player;

    //Puntos de patrulla
    [SerializeField] Transform[] _patrolPoints;

    //Rangos de deteccion y de ataque de la IA
    [SerializeField] float _detectionRange = 2;
    [SerializeField] float _attackRange =1;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start()
    {
        PuntoAleatorio();
        _currentState = State.Patrolling;
    }

    void Update()
    {
        switch (_currentState) 
        {
            case State.Patrolling:
                Patrulla();
            break;
            case State.Chasing:
                Perseguir();
            break;
            case State.Attacking:
                Atacar();
            break;
        }
    }

    //Funcion en la que la IA se direcciona de un punto aleatorio a otro hasta que detecta en el rango al jugador y cambia al estado de Chasing o perseguir 
    void Patrulla()
    {
        if(EnRango(_detectionRange) == true)
        {
            _currentState = State.Chasing;
        }

        if(_agent.remainingDistance < 0.5f)
        {
            PuntoAleatorio();
        }
    }

    //Funcion que permite a la IA perseguir al jugador indicandole que su destino sea la posicion actual del jugador, siempre que el jugador se encuentre en el rango de vision estipulado
    void Perseguir()
    {
        _agent.destination = _player.position;
        if(EnRango(_detectionRange) == false)
        {
            _currentState = State.Patrolling;
        }

        if(EnRango(_attackRange) == true)
        {
            _currentState = State.Attacking;
        }
    }

    //Funcion que permite a la IA entrar en el estado de atacar. Se simula el ataque con un mensajito
    void Atacar()
    {
        Debug.Log("PUM!");
        _currentState = State.Chasing;
    }

    //Funcion en la que la IA elige la posicion de un punto aleatorio de los creados en la escena para que se dirija a ellos.
    void PuntoAleatorio()
    {
        _agent.destination = _patrolPoints[Random.Range(0,_patrolPoints.Length)].position;
    }

    //Booleana que devuelve un verdadero o falso dependiendo de si el jugador se encuentra en rango o no. Esta funcion permite a la IA saber cuando tiene que pasar a los estados de perseguir y de atacar
    bool EnRango(float _rango)
    {
        if(Vector3.Distance(transform.position, _player.position) < _rango)
        {
            return true;
        } else
        {
            return false;
        }
    }

    //Gizmos creados para visualizar mejor los rangos de vision y ataque de la IA. Tambien hay creados unos Gizmos para indicar las posiciones de los puntos de patrulla.
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        foreach(Transform _point in _patrolPoints)
        {
            Gizmos.DrawWireSphere(_point.position, 0.5f);
        }
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _attackRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectionRange);
    }
}
