using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    [SerializeField] int _isLeftOrRight;
    [SerializeField] float _speed = 300f;
    [SerializeField] Camera _cam;

    private Rigidbody2D _rb;
    private Vector3 _camMousePos;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(_isLeftOrRight))
        {
            _camMousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mousePos = new Vector3(_camMousePos.x, _camMousePos.y, 0f);
            Vector3 difference = mousePos - transform.position;
            float rotationZ = Mathf.Atan2(difference.x, -difference.y) * Mathf.Rad2Deg;

            // +-90 for the hand to point correctly
            _rb.MoveRotation(Mathf.LerpAngle(_rb.rotation, rotationZ + 90 - _isLeftOrRight * 180, _speed * Time.deltaTime));
        }
    }

    public void SetIsLeftOrRight(int num)
    {
        _isLeftOrRight = num;
    }

    public int GetIsLeftOrRight()
    {
        return _isLeftOrRight;
    }

    public void SetCam(Camera cam)
    {
        _cam = cam;
    }

    public Camera GetCamera()
    {
        return _cam;
    }
}
