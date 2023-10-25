using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickmanBehaviors : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField] Balance[] _balanceList;
    // Use what is ground when not use Tilesmap
    public LayerMask _jumpableLayers;
    //public bool[] _isGrabbingGround = { false, false };

    // To check if the stickman is on ground
    float _raycastDistance;

    private void Awake()
    {
        _rb = this.GetComponent<Rigidbody2D>();
    }

    public void Move(float movementForce, float moveDirection)
    {
        _rb.velocity = new Vector2(moveDirection * movementForce * Time.deltaTime, _rb.velocity.y);
    }

    public void setRaycastDistance(float distance)
    {
        _raycastDistance = distance;
    }

    public void Jump(float jumpForce)
    {
        if (isGrounded()) //|| _isGrabbingGround[0] == true || _isGrabbingGround[1] == true)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce * Time.deltaTime);
        }
    }

    private bool isGrounded()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _raycastDistance, _jumpableLayers);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }

    public void StartCrouching()
    {
        for (int i = 0; i < _balanceList.Length; i++)
        {
            _balanceList[i].enabled = false;
        }
    }

    public void StopCrouching()
    {
        for (int i = 0; i < _balanceList.Length; i++)
        {
            _balanceList[i].enabled = true;
        }
    }
}
