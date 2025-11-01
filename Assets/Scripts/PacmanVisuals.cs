using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class PacmanVisuals : MonoBehaviour
    {
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
    }

    [Serializable]
    struct VisualConfig
    {
        [SerializeField] public GameObject Body;
        [SerializeField] public GameObject Mouth;
    }
}