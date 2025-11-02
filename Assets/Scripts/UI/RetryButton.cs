using PlayerComponents;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class RetryButton: MonoBehaviour
    {
        private Health _health;

        private void Start()
        {
            _health = FindFirstObjectByType<Health>();
            _health.OnHardDeath += HandleDeath;
            gameObject.SetActive(false);

        }

        private void HandleDeath()
        {
            gameObject.SetActive(true);
        }

        public void RestartGame()
        {
            Time.timeScale = 0f;
            SceneManager.LoadScene("SampleScene");
            Time.timeScale = 1f;
            gameObject.SetActive(false);
        }
        
        
    }
}