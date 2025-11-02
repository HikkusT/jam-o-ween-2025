using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using PlayerComponents;
using UnityEngine;

namespace DefaultNamespace
{
    public class PacmanVisuals : MonoBehaviour
    {
        [SerializeField] private Health _health;
        
        [Header("Blink")]
        [SerializeField] private float _visibleTime;
        [SerializeField] private float _invisibleTime;
        [SerializeField] private int _amountOfCycles;
        
        [SerializeField] private float _amountToCloseMouth = 0.5f;
        [SerializeField] private float _timeToOpenMouth = 0.1f;
        [SerializeField] private List<VisualConfig> _full;
        [SerializeField] private Transform _view;
        
        [SerializeField] private float _amplitude;
        [SerializeField] private float _frequency;
        
        [SerializeField] private float _amplitudeRotation;
        [SerializeField] private float _frequencyRotation;

        private Vector2 _lastUpdateAt;
        private float _closedAt;
        private float _amountWalked;
        private int _currentIndex = -1;
        
        private void Start()
        {
            _lastUpdateAt = new Vector2(transform.position.x, transform.position.z);
            _health.OnHitTaken += Blink;
        }

        private void Update()
        {
            _view.position = new Vector3(_view.position.x, 0.5f + _amplitude * Mathf.Sin(_frequency * Time.time), _view.position.z);
            _view.rotation = Quaternion.Euler(_amplitudeRotation + _amplitudeRotation * Mathf.Sin(_frequencyRotation * Time.time), _view.rotation.eulerAngles.y, _view.rotation.eulerAngles.z);
            
            if (_currentIndex > -1)
            {
                if (Time.time > _closedAt + _timeToOpenMouth)
                {
                    _full[_currentIndex].Mouth.SetActive(false);
                    _full[_currentIndex].Body.SetActive(false);
                    if (_currentIndex == _full.Count - 1)
                    {
                        _currentIndex = -1;
                    }
                    else
                    {
                        _currentIndex++;
                        _full[_currentIndex].Mouth.SetActive(true);
                        _full[_currentIndex].Body.SetActive(true);
                        _closedAt = Time.time;
                    }
                    _amountWalked = 0;
                    _lastUpdateAt = new Vector2(transform.position.x, transform.position.z);
                }
            }
            else
            {
                _amountWalked += (new Vector2(transform.position.x, transform.position.z) - _lastUpdateAt).magnitude;

                if (_amountWalked >= _amountToCloseMouth)
                {
                    _amountWalked = 0;
                    _currentIndex = 0;
                    _full[_currentIndex].Mouth.SetActive(true);
                    _full[_currentIndex].Body.SetActive(true);
                    _closedAt = Time.time;
                }

                _lastUpdateAt = new Vector2(transform.position.x, transform.position.z);
            }
        }

        CancellationTokenSource _blinkCts = new CancellationTokenSource();
        private void Blink()
        {
            async UniTaskVoid DoBlink(CancellationToken ct)
            {
                for (int i = 0; i < _amountOfCycles && !ct.IsCancellationRequested; i++)
                {
                    _view.gameObject.SetActive(false);
                    await UniTask.Delay(TimeSpan.FromSeconds(_invisibleTime), cancellationToken: ct).SuppressCancellationThrow();
                    _view.gameObject.SetActive(true);
                    await UniTask.Delay(TimeSpan.FromSeconds(_visibleTime), cancellationToken: ct).SuppressCancellationThrow();
                }
            }
            
            _blinkCts.Cancel();
            _blinkCts = new CancellationTokenSource();
            DoBlink(_blinkCts.Token).Forget();
        }
    }

    [Serializable]
    struct VisualConfig
    {
        [SerializeField] public GameObject Body;
        [SerializeField] public GameObject Mouth;
    }
}