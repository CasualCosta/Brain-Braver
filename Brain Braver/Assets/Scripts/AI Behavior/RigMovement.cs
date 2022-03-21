using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Route { Cyclical, Returning }
public class RigMovement : MonoBehaviour
{
    [SerializeField] Transform _kart = null;
    [SerializeField] Transform[] _checkpoints = null;
    [SerializeField] Route _route = Route.Cyclical;
    [SerializeField] float _moveSpeed = 5f, _waitTime = 1f;

    bool isReturning = false;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Patrol());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Patrol()
    {
        while(true)
        {
            if (!_kart)
            {
                Destroy(gameObject);
                yield break;
            }
            if (Vector3.Distance(_kart.position, _checkpoints[index].position) == 0)
            {
                yield return new WaitForSeconds(_waitTime);
                //Get next movepoint
                switch (_route)
                {
                    case Route.Cyclical:
                        index = (index + 1) % _checkpoints.Length; 
                        break;
                    case Route.Returning:
                        if(isReturning)
                        {
                            index--;
                            if (index == 0)
                                isReturning = false;
                        }
                        else
                        {
                            index++;
                            if (index == _checkpoints.Length - 1)
                                isReturning = true;
                        }
                        break;
                }
            }
            if (_kart)
                _kart.position = Vector3.MoveTowards
                    (_kart.position, _checkpoints[index].position, _moveSpeed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
}
