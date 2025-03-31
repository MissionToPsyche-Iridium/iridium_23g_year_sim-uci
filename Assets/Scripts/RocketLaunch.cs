// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class SimpleRocketLaunch : MonoBehaviour
// {
//     [SerializeField] private float speed = 1;
//     [SerializeField] private float acceleration = 1;
//     [SerializeField] private float maxSpeed = 5;
//     // Update is called once per frame
//     void Update()
//     {
//         transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
//         if(speed < maxSpeed)
//             speed += acceleration * Time.deltaTime;
//         Debug.Log("Speed" + speed);
//     }
// }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLaunch : MonoBehaviour
{
    [SerializeField] Transform Rocket;
    [SerializeField] float SpeedIncrease;
    [SerializeField] float LaunchSpeed;

    void Start()
    {

    }

    void Update()
    {
        LaunchSpeed += SpeedIncrease * Time.deltaTime;
        Rocket.Translate(0, LaunchSpeed, 0);
    }
}
