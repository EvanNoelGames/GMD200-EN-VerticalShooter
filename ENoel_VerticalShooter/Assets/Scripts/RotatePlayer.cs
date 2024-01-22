using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 3f;

    // Update is called once per frame
    void Update()
    {
        float rotate = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, 0, -rotate);
    }
}
