using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotate : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 80f;
    // Update is called once per frame
    void Update()
    {
        transform.rotation *= Quaternion.Euler(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}
