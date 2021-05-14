using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMaker : MonoBehaviour
{
    public GameObject Zombie;
    private Transform _transform;
    private float tiempo = 0f;

    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    void Update()
    {
        tiempo += Time.deltaTime;
        if (tiempo >= 3)
        {
            var zombie = new Vector3(_transform.position.x + 3f, _transform.position.y, _transform.position.z);
            Instantiate(Zombie, zombie, Quaternion.identity);
            tiempo = 0;
        }
    }
}
