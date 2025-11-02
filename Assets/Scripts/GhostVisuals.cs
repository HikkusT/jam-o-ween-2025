using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GhostVisuals : MonoBehaviour
{
    [SerializeField] private Transform _view;
    [SerializeField] private Renderer _mainRenderer;
    [SerializeField] private float _amplitude;
    [SerializeField] private float _frequency;
    [SerializeField, ColorUsage(false, true)] private List<Color> _availableColors;

    private float _startingY;

    private void Start()
    {
        _startingY = _view.localPosition.y;
        Color color = _availableColors[(int) GetComponent<GhostController>().ChasePattern];
        _mainRenderer.material.SetColor("_BaseColor", color);
        _mainRenderer.material.SetColor("_1st_ShadeColor", color * 0.8f);
        _mainRenderer.material.SetColor("_Emissive_Color", new Color(color.r, color.g, color.b, 1.5f));
    }

    private void Update()
    {
        _view.localPosition = new Vector3(_view.localPosition.x, _startingY + _amplitude * Mathf.Sin(_frequency * Time.time), _view.localPosition.z);
    }
}