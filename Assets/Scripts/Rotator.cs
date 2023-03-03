using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed = 50f;
    [SerializeField]
    private float maxRotateSpeed = 500;
    [SerializeField]
    private Vector3 rotateAngle = Vector3.forward;

    public void Stop()
    {
        rotateSpeed = 0;
    }

    public void RotateFast()
    {
        rotateSpeed = maxRotateSpeed;
    }

    private void Update() {
        transform.Rotate(rotateAngle * rotateSpeed * Time.deltaTime);
    }
}
