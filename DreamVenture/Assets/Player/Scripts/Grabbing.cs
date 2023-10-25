using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbing : MonoBehaviour
{
    [SerializeField] Rigidbody2D _hand;
    [SerializeField] int _isLeftOrRight;
    //[SerializeField] StickmanBehaviors _stickmanBehaviors;
    [SerializeField] LayerMask[] _layersCantGrab;

    GameObject _currentlyHolding;
    bool _canGrab;
    FixedJoint2D _joint;

    private void Update()
    {
        Grab();
    }

    void Grab()
    {
        if (Input.GetMouseButtonDown(_isLeftOrRight))
        {
            _canGrab = true;
        }
        if (Input.GetMouseButtonUp(_isLeftOrRight))
        {
            _canGrab = false;
        }

        if (!_canGrab && _currentlyHolding != null)
        {
            FixedJoint2D[] joints = _currentlyHolding.GetComponents<FixedJoint2D>();
            for (int i = 0; i < joints.Length; i++)
            {
                if (joints[i].connectedBody == _hand)
                {
                    Destroy(joints[i]);
                }
            }

            //Debug.Log("Un grab");
            //_stickmanBehaviors._isGrabbingGround[_isLeftOrRight] = false;

            _joint = null;
            _currentlyHolding = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject @gameObject = collision.gameObject;
        if (_canGrab && _currentlyHolding == null && gameObject.GetComponent<Rigidbody2D>() != null)
        {
            foreach (LayerMask layermask in _layersCantGrab)
            {
                if (IsInLayerMask(@gameObject, layermask))
                {
                    return;
                }
            }
            _currentlyHolding = collision.gameObject;
            _joint = _currentlyHolding.AddComponent<FixedJoint2D>();
            _joint.connectedBody = _hand;

            //Debug.Log("Grab");
            //if (_currentlyHolding.gameObject.layer == LayerMask.NameToLayer("Ground"))
            //{
            //    Debug.Log("Grab Ground");
            //    _stickmanBehaviors._isGrabbingGround[_isLeftOrRight] = true;
            //}
        }
    }

    private bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        return ((layerMask.value & (1 << obj.layer)) > 0);
    }
}
