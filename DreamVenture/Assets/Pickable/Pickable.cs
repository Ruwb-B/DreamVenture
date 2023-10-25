using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    [SerializeField] float _force = 300f;
    public bool _isPicked;
    [HideInInspector] public int _isLeftOrRight;
    Rigidbody2D _rb;
    public bool _isPickedByEnemy = false;

    //** Hand attibute that pick **
    [HideInInspector] public Rigidbody2D _handRigid;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_isPicked && !_isPickedByEnemy)
        {
            _rb.MoveRotation(Mathf.LerpAngle(_rb.rotation, _handRigid.rotation + 90 - 180 * _isLeftOrRight, _force * Time.deltaTime));
        }
        if(transform.position.y < -1000)
        {
            Destroy(gameObject);
        }
    }
}
