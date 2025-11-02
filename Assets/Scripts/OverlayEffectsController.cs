using DG.Tweening;
using PlayerComponents;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class OverlayEffectsController : MonoBehaviour
{
    [SerializeField] private Volume _volume;
    [SerializeField] private Color _damageColor;
    [SerializeField] private float _damageIntensity;
    [SerializeField] private float _damageBlinkCycleDuration;
    [SerializeField] private float _totalDamageDuration;
    
    [SerializeField] private float _hitShakeDuration;
    [SerializeField] private float _hitShakeIntensity;
    [SerializeField] private int _hitShakeVibrato = 10;
    
    [SerializeField] private Color _frenzyColor;
    [SerializeField] private float _frenzyIntensity;
    [SerializeField] private float _frenzyTransitionDuration;
    [SerializeField] private float _timeTo;

    private Vignette _vignette;
    private Frenzy _frenzy;
    private Tween _currentTween;
    
    private void Start()
    {
        _volume.profile.TryGet(out _vignette);
        _frenzy = FindFirstObjectByType<Frenzy>();
        _frenzy.OnFrenzyEnter += SetupFrenzy;
        _frenzy.OnFrenzyHardExit += CancelFrenzy;

        var health = FindFirstObjectByType<Health>();
        health.OnHitTaken += ShowHitFeedback;
    }

    private void SetupFrenzy()
    {
        _currentTween?.Kill();
        
        _vignette.color.value = _frenzyColor;
        _currentTween = DOTween.To(
                () => _vignette.intensity.value,
                x => _vignette.intensity.value = x,
                _frenzyIntensity,
                _frenzyTransitionDuration
            ).SetEase(Ease.InOutSine)
            .SetTarget(_vignette);
    }
    
    private void CancelFrenzy()
    {
        _currentTween?.Kill();
        
        _currentTween = DOTween.To(
                () => _vignette.intensity.value,
                x => _vignette.intensity.value = x,
                0,
                _frenzyTransitionDuration
            ).SetEase(Ease.InOutSine)
            .SetTarget(_vignette);
    }

    private void ShowHitFeedback()
    {
        if (_frenzy.IsActive || _currentTween?.active == true) return;

        Camera.main.DOShakePosition(_hitShakeDuration, _hitShakeIntensity, _hitShakeVibrato);
        
        _vignette.color.value = _damageColor;
        _vignette.intensity.value = 0;

        // Create looping tween
        var hpTween = DOTween.To(
                () => _vignette.intensity.value,
                x => _vignette.intensity.value = x,
                _damageIntensity,
                _damageBlinkCycleDuration
            )
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo); // infinite blink in/out

        _currentTween?.Kill();
        // Stop blinking after totalDuration
        DOVirtual.DelayedCall(_totalDamageDuration, () =>
        {
            if (hpTween.IsActive())
            {
                hpTween.Kill();
                _vignette.intensity.value = 0; // reset to normal
            }
        });
    }
}