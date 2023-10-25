using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArmController : MonoBehaviour
{
    [SerializeField] int _isLeftOrRight;
    [SerializeField] float _speed = 300f;
    [SerializeField] Transform _targetPoint;

    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 difference = _targetPoint.position - transform.position;
        float rotationZ = Mathf.Atan2(difference.x, -difference.y) * Mathf.Rad2Deg;

        // +-90 for the hand to point correctly
        _rb.MoveRotation(Mathf.LerpAngle(_rb.rotation, rotationZ + 90 - _isLeftOrRight * 180, _speed * Time.deltaTime));
    }
}
