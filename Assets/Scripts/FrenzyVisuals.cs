using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using PlayerComponents;
using UnityEngine;

namespace DefaultNamespace
{
    public class FrenzyVisuals : MonoBehaviour
    {
        [SerializeField, ColorUsage(false, true)] private Color _frenzyColor;
        [SerializeField, ColorUsage(false, true)] private Color _normalColor;
        [SerializeField] private Material _frenzyMaterial;
        [SerializeField] private Material _normalMaterial;
        [SerializeField] private float _transitionSpeed;
        [SerializeField] private float _spreadSpeed;

        private List<Renderer> _lightRenderers;
        
        private void Start()
        {
            var frenzy = FindFirstObjectByType<Frenzy>();
            frenzy.OnFrenzyEnter += StartEffect;
            frenzy.OnFrenzyHardExit += StopEffect;
            _lightRenderers = GameObject.FindGameObjectsWithTag("Light").Select(it => it.GetComponent<Renderer>()).ToList();
        }

        private CancellationTokenSource _cts = new();
        
        private void StartEffect()
        {
            async UniTaskVoid Spread(Vector3 origin, CancellationToken token)
            {
                float startedAt = Time.time;
                var orderedRenderes = _lightRenderers.OrderBy(it => Vector3.Distance(it.transform.position, origin)).ToList();
                int index = 0;
                while (index < orderedRenderes.Count())
                {
                    while (Vector3.Distance(orderedRenderes[index].transform.position, origin) <
                           (Time.time - startedAt) * _spreadSpeed)
                    {
                        orderedRenderes[index].material.SetColor("_EmissionColor", _frenzyColor);
                        index++;
                    }

                    await UniTask.Yield(token);
                }
            }
            
            var player = FindFirstObjectByType<CharacterController>();
            Vector3 position = player.transform.position;
            _cts.Cancel();
            _cts = new CancellationTokenSource();
            Spread(position, _cts.Token).Forget();
        }

        private void StopEffect()
        {
            _cts.Cancel();
            foreach (var lightRenderer in _lightRenderers)
            {
                lightRenderer.material.SetColor("_EmissionColor", _normalColor);
            }
        }
    }
}