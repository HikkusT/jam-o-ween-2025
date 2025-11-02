using System;
using PlayerComponents;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreCount: MonoBehaviour
    {
        
        [SerializeField] private TMP_Text score;
        private RogueLikeManager  _rogueLikeManager;

        private void Start()
        {
            _rogueLikeManager = FindFirstObjectByType<RogueLikeManager>();
        }

        private void Update()
        {
            score.text = _rogueLikeManager.GetScore().ToString();
        }
        
    }
}