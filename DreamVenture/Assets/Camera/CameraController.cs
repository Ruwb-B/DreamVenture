using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform _target;
    [Range(0f, 1f)]
    [SerializeField] float _interpolation = 0.1f;
    [SerializeField] Vector3 offset = new Vector3(0, 1f, -10f);

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position + offset, _interpolation);
    }
}
