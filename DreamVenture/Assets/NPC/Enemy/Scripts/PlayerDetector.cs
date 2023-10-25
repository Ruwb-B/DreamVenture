using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [field: SerializeField] public bool _playerDetected { get; private set; }
    // For other script to take the direction to the detected target
    public Vector2 _directionToTarget => _target.transform.position - _detectorOrigin.position;

    [Header("OverlapBox parameters")]
    [SerializeField]
    private Transform _detectorOrigin;
    [SerializeField] Vector2 _detectorSize = Vector2.one;
    [SerializeField] Vector2 _detectorOriginOffset = Vector2.zero;

    [SerializeField] float _detectionDelay = 0.3f;

    [SerializeField] LayerMask _detectorLayerMask;

    [Header("Gizmo parameters")]
    [SerializeField] Color _gizmoIdleColor = Color.green;
    [SerializeField] Color _gizmoDetectedColor = Color.red;
    [SerializeField] bool _showGizmos = true;

    private GameObject _target;

    // For other script to get detected target
    public GameObject _Target
    {
        get => _target;
        private set
        {
            _target = value;
            _playerDetected = _target != null;
        }
    }

    private void Start()
    {
        StartCoroutine(DetectionCoroutine());
    }

    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(_detectionDelay);
        PerformDetection();
        StartCoroutine(DetectionCoroutine());
    }

    public void PerformDetection()
    {
        Collider2D collider = Physics2D.OverlapBox((Vector2)_detectorOrigin.position + _detectorOriginOffset, _detectorSize, 0, _detectorLayerMask);
        if (collider != null)
        {
            _Target = collider.gameObject;
        }
        else
        {
            _Target = null;
        }
    }

    private void OnDrawGizmos()
    {
        if(_showGizmos && _detectorOrigin != null)
        {
            Gizmos.color = _gizmoIdleColor;
            if (_playerDetected)
            {
                Gizmos.color = _gizmoDetectedColor;
            }
            Gizmos.DrawCube((Vector2)_detectorOrigin.position + _detectorOriginOffset, _detectorSize);
        }
    }
}
