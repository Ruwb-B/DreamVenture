using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetPoint : MonoBehaviour
{
    [SerializeField] Transform _pivotTransform;
    [SerializeField] float _rotationSpeed = 300f;
    [SerializeField] PlayerDetector _playerDetector;

    // Update is called once per frame
    void Update()
    {
        if (_playerDetector._playerDetected)
        {
            transform.RotateAround(_pivotTransform.position,
                Vector3.forward * -_playerDetector._directionToTarget.normalized.x, _rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.RotateAround(_pivotTransform.position,
                Vector3.forward, _rotationSpeed * Time.deltaTime);
        }
    }
}
