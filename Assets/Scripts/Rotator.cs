using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private StageController stageController;
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
        if (stageController.IsGameStart == false) return;
        
        transform.Rotate(rotateAngle * rotateSpeed * Time.deltaTime);
    }
}
