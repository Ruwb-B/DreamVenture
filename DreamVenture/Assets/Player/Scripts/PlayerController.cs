using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(StickmanBehaviors))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float _movementForce = 700f;
    [SerializeField] float _jumpForce = 27000f;
    [SerializeField] float _crouchDecreaseMovement = 0.5f;

    [Space(5)]
    [Range(0f, 100f)]
    [SerializeField] float _raycastDistance = 1.5f;

    [Header("Animation")]
    [SerializeField] Animator _animator;

    StickmanBehaviors _behaviors;
    float _moveDirection;
    bool _jump;
    bool _crouch;
    bool _isCrouching = false;

    private void Awake()
    {
        _behaviors = this.GetComponent<StickmanBehaviors>();
        _behaviors.setRaycastDistance(_raycastDistance);
    }

    private void Update()
    {
        ReadInput();
    }

    private void FixedUpdate()
    {
        _behaviors.Move(_movementForce, _moveDirection);

        if (_jump)
        {
            _behaviors.Jump(_jumpForce);
            _jump = false;
        }

        if (_crouch && !_isCrouching)
        {
            _behaviors.StartCrouching();
            _movementForce *= _crouchDecreaseMovement;
            _isCrouching = true;
        }
        else if (!_crouch && _isCrouching)
        {
            _behaviors.StopCrouching();
            _movementForce /= _crouchDecreaseMovement;
            _isCrouching = false;
        }
    }

    void ReadInput()
    {
        //Move side way
        _moveDirection = Input.GetAxis("Horizontal");

        //Animation
        if (_moveDirection != 0)
        {
            _animator.SetBool("Walk", true);
        }
        else
        {
            _animator.SetBool("Walk", false);
        }

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            _jump = true;
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            _crouch = true;
        }
        else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            _crouch = false;
        }
    }
}
