using System;
using UnityEngine;

public class GhostVisuals : MonoBehaviour
{
    [SerializeField] private Transform _view;
    [SerializeField] private float _amplitude;
    [SerializeField] private float _frequency;

    private void Update()
    {
        _view.localPosition = new Vector3(_view.localPosition.x, _amplitude * Mathf.Sin(_frequency * Time.time), _view.localPosition.z);
    }
}