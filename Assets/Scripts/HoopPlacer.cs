using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class HoopPlacer : MonoBehaviour
{
    [SerializeField] private ARRaycastManager _RaycastManager;
    [SerializeField] private GameObject _HoopPrefab;
    private bool _HoopPlaced;
    private GameObject _CurrentHoop;
    private List<ARRaycastHit> _RaycastHits = new List<ARRaycastHit>();
    [SerializeField] private float _hoopVerticalOffset = 0.025f;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _rotationCorrectionY = 90f;
    private bool _placementEnabled = false;
    [SerializeField] private ARPlaneManager _planeManager;

    void Update()
    {
        if (_HoopPlaced) return;
        if (!_placementEnabled) return;
        if (Pointer.current == null) return;

        if (Pointer.current.press.wasPressedThisFrame)
        {
            Vector2 tapPosition = Pointer.current.position.ReadValue();
            if (_RaycastManager.Raycast(tapPosition, _RaycastHits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPos = _RaycastHits[0].pose;
                Vector3 spawnPosition = hitPos.position + new Vector3(0, _hoopVerticalOffset, 0);

                Vector3 directionToCamera = _cameraTransform.position - spawnPosition;
                directionToCamera.y = 0f;

                Quaternion correctionOffset = Quaternion.Euler(0, _rotationCorrectionY, 0);
                Quaternion facingRotation = Quaternion.LookRotation(directionToCamera) * correctionOffset;

                _CurrentHoop = Instantiate(_HoopPrefab, spawnPosition, facingRotation);
                _HoopPlaced = true;

                _planeManager.enabled = false;
                foreach (var plane in _planeManager.trackables)
                {
                    plane.gameObject.SetActive(false);
                }

                GameTimer.Instance.BeginTimer();

                ScoreTrigger trigger = _CurrentHoop.GetComponentInChildren<ScoreTrigger>();
                if (trigger != null)
                {
                    trigger.OnScored += (points) => ScoreManager.Instance.AddScore(points);
                }
            }
        }
    }

    public void ResetHoop()
    {
        if (_CurrentHoop != null) Destroy(_CurrentHoop);
        _HoopPlaced = false;

        _planeManager.enabled = true;
        foreach (var plane in _planeManager.trackables)
        {
            plane.gameObject.SetActive(true);
        }
    }

    public void EnablePlacement() => _placementEnabled = true;
    public void DisablePlacement() => _placementEnabled = false;
}