using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using PlayerComponents;
using UnityEngine;
using Random = UnityEngine.Random;

public class GhostVisuals : MonoBehaviour
{
    [SerializeField] private Transform _view;
    [SerializeField] private Renderer _mainRenderer;
    [SerializeField] private float _amplitude;
    [SerializeField] private float _frequency;
    [SerializeField, ColorUsage(false, true)] private List<Color> _availableColors;
    [SerializeField, ColorUsage(false, true)] private Color _vulnerableColor;
    [SerializeField, ColorUsage(false, true)] private Color _intermediateColor;
    [SerializeField] private float _transitionDuration;

    [SerializeField] private GameObject _destroyParticles;

    private float _startingY;
    private Frenzy _frenzy;

    private void Start()
    {
        _startingY = _view.localPosition.y;
        Color color = _availableColors[(int) GetComponent<GhostController>().ChasePattern];
        ChangeColor(color);

        _frenzy = FindFirstObjectByType<Frenzy>();
        _frenzy.OnFrenzyEnter += ChangeVisualsToVulnerable;
        _frenzy.OnFrenzyHardExit += RevertColor;
        _frenzy.OnFrenzyExiting += DoBlink;
        if (_frenzy.IsInTransition)
        {
            DoBlink();
        }
        else if (_frenzy.IsActive)
        {
            ChangeVisualsToVulnerable();
        }
    }

    private void ChangeVisualsToVulnerable()
    {
        ChangeColor(_vulnerableColor);
    }

    private void RevertColor()
    {
        ChangeColor(_availableColors[(int) GetComponent<GhostController>().ChasePattern]);
    }
    
    private void ChangeColor(Color color)
    {
        _mainRenderer.material.SetColor("_BaseColor", color);
        _mainRenderer.material.SetColor("_1st_ShadeColor", color * 0.8f);
        _mainRenderer.material.SetColor("_Emissive_Color", new Color(color.r, color.g, color.b, 1.5f));
    }

    private void Update()
    {
        _view.localPosition = new Vector3(_view.localPosition.x, _startingY + _amplitude * Mathf.Sin(_frequency * Time.time), _view.localPosition.z);
    }

    private void OnDestroy()
    {
        _frenzy.OnFrenzyEnter -= ChangeVisualsToVulnerable;
        _frenzy.OnFrenzyHardExit -= RevertColor;
        _frenzy.OnFrenzyExiting -= DoBlink;
    }

    public void PlayParticles()
    {
        var particles = Instantiate(_destroyParticles, transform.position + Vector3.up * 0.2f, Quaternion.identity);
        particles.transform.localScale = 0.5f * Vector3.one;
    }

    private void DoBlink() => Blink().Forget();
    private async UniTaskVoid Blink()
    {
        while (_frenzy.IsInTransition)
        {
            ChangeColor(_intermediateColor);
            await UniTask.Delay(TimeSpan.FromSeconds(_transitionDuration));
            if (!_frenzy.IsActive) break;
            
            ChangeColor(_vulnerableColor);
            await UniTask.Delay(TimeSpan.FromSeconds(_transitionDuration));
        }
    }
}