using PlayerComponents; // Para acessar o script Health
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    // Este script deve estar no GameObject "LifeContainer"
    public class LifeCount: MonoBehaviour
    {
        [SerializeField] private GameObject fullHeartPrefab; 
        [SerializeField] private GameObject emptyHeartPrefab;
        private Health _health;

        private List<GameObject> _heartIcons = new List<GameObject>();

        private void Start()
        {
            _health = FindFirstObjectByType<Health>();

            _health.OnHealthChanged += UpdateHeartDisplay;
        }

        private void OnDestroy()
        {
            if (_health != null)
            {
                _health.OnHealthChanged -= UpdateHeartDisplay;
            }
        }

        private void UpdateHeartDisplay()
        {
            foreach (GameObject heart in _heartIcons)
            {
                Destroy(heart);
            }
            _heartIcons.Clear();

            for (int i = 0; i < _health.GetPlayerCurrentHealth(); i++)
            {
                GameObject newHeart = Instantiate(fullHeartPrefab, transform);
                _heartIcons.Add(newHeart);
            }
            
            for (int i = _health.GetPlayerCurrentHealth(); i < _health.GetPlayerMaxHealth(); i++)
            {
                GameObject newHeart = Instantiate(emptyHeartPrefab, transform);
                _heartIcons.Add(newHeart);
            }
        }
    }
}