using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] float _pickUpRange = 2f;
    [SerializeField] int _isLeftOrRight;

    //** This attribute **
    bool _isPicking = false;
    bool _ablePicking = false;
    Grabbing _grabbing;
    Rigidbody2D _rb;

    //** Top parent attribute **
    Transform _rootTransform;

    //** Weapon Attribute **
    GameObject _weaponObject;
    HingeJoint2D _joint;
    Pickable _weapon;

    private void Awake()
    {
        _grabbing = GetComponent<Grabbing>();
        _rb = GetComponent<Rigidbody2D>();
        _rootTransform = transform.root;
        //_handController = GetComponent<ArmController>();
    }

    private void Update()
    {
        if (!_isPicking)
        {
            FindWeaponInRange();
        }

        //** Pick up or drop down weapon **
        if (Input.GetKeyDown(KeyCode.Q) && _isLeftOrRight == 0)
        {
            if (!_isPicking && _ablePicking)
            {
                Pick();
            }
            else if (_isPicking)
            {
                Drop();
            }
        }
        else if (Input.GetKeyDown(KeyCode.E) && _isLeftOrRight == 1)
        {
            if (!_isPicking && _ablePicking)
            {
                Pick();
            }
            else if (_isPicking)
            {
                Drop();
            }
        }

    }

    void FindWeaponInRange()
    {
        Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, _pickUpRange);
        foreach (Collider2D collider2D in colliderArray)
        {
            if (collider2D.tag == "Pickable")
            {
                //** Ignore picked weapon **
                _weapon = collider2D.GetComponent<Pickable>();
                if (_weapon._isPicked)
                {
                    continue;
                }

                _ablePicking = true;
                _weaponObject = collider2D.gameObject;
                return;
            }
        }

        //** Do not found weapon in range **
        _ablePicking = false;
        _weaponObject = null;
    }

    void Pick()
    {
        IgnoreCollision();
        _grabbing.enabled = false;

        _weapon._isPicked = true;
        _isPicking = true;

        _weaponObject.transform.SetParent(_rb.transform, false);
        _weaponObject.transform.localPosition = Vector2.zero;

        _joint = _weaponObject.AddComponent<HingeJoint2D>();
        _joint.connectedBody = _rb;
        //_joint.useLimits = true;
        //JointAngleLimits2D jointLimits = _joint.limits;
        //jointLimits.max = 0;
        //_joint.limits = jointLimits;


        _weapon._handRigid = _rb;
        _weapon._isLeftOrRight = _isLeftOrRight;
    }

    void Drop()
    {
        RestoreCollision();
        HingeJoint2D[] joints = _weaponObject.GetComponents<HingeJoint2D>();
        for (int i = 0; i < joints.Length; i++)
        {
            if (joints[i].connectedBody == _rb)
            {
                Destroy(joints[i]);
            }
        }
        _weaponObject.transform.SetParent(null);

        _isPicking = false;

        _joint = null;
        _weaponObject = null;
        _weapon._isPicked = false;

        _grabbing.enabled = true;
    }

    // Collision between the picker and the pick up will be ignore or not
    void IgnoreCollision()
    {
        Collider2D[] weaponCollider2D = _weaponObject.GetComponentsInChildren<Collider2D>();
        Collider2D[] collider2Ds = _rootTransform.GetComponentsInChildren<Collider2D>();
        for (int i = 0; i < weaponCollider2D.Length; i++)
        {
            for (int j = 0; j < collider2Ds.Length; j++)
            {
                Physics2D.IgnoreCollision(weaponCollider2D[i], collider2Ds[j]);
            }
        }
    }

    void RestoreCollision()
    {
        Collider2D[] weaponCollider2D = _weaponObject.GetComponentsInChildren<Collider2D>();
        Collider2D[] collider2Ds = _rootTransform.GetComponentsInChildren<Collider2D>();
        for (int i = 0; i < weaponCollider2D.Length; i++)
        {
            for (int j = 0; j < collider2Ds.Length; j++)
            {
                Physics2D.IgnoreCollision(weaponCollider2D[i], collider2Ds[j], false);
            }
        }
    }
}
