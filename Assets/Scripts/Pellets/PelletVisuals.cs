using UnityEngine;

public class PelletVisuals : MonoBehaviour
{
    [SerializeField] private float _amplitude;
    [SerializeField] private float _frequency;
    
    private Vector3 _initialPosition;
    private float _phase;
    
    void Start()
    {
        _initialPosition = transform.position;
        _phase = Random.Range(0, 2 * Mathf.PI);
    }

    private void Update()
    {
        transform.position = _initialPosition + Vector3.up * _amplitude * Mathf.Sin(_frequency * Time.time + _phase);
    }
}
