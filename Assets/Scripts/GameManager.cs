using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tanks
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Tank _player1, _player2;
        [SerializeField] private GameObject[] _levels;

        [SerializeField] private TextMeshProUGUI _winText;
        [SerializeField] private GameObject _selectMapScreen;
        [SerializeField] private GameObject _restartButton;
        
        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void OnChooseLevel(int chosenIndex)
        {
            for (int i = 0; i < _levels.Length; i++)
            {
                _levels[i].SetActive(false);
            }
            _levels[chosenIndex].SetActive(true);
        }

        public void OnStartGame()
        {
            _selectMapScreen.SetActive(false);
            
            _player1.IsMoveEnabled = true;
            _player2.IsMoveEnabled = true;
            
            _player1.onTakeDamage += OnPlayer1TakeDamage;
            _player2.onTakeDamage += OnPlayer2TakeDamage;
        }
        
        public void OnRestart()
        {
            SceneManager.LoadScene(0);
        }
        
        public void Exit()
        {
            Application.Quit();
        }

        private void OnPlayer1TakeDamage(int health, int maxHealth)
        {
            if (health <= 0)
            {
                _winText.text = "Player 2 wins!";
                _restartButton.SetActive(true);
                _audioSource.Play();
            }
        }
        
        private void OnPlayer2TakeDamage(int health, int maxHealth)
        {
            if (health <= 0)
            {
                _winText.text = "Player 1 wins!";
                _restartButton.SetActive(true);
                _audioSource.Play();
            }
        }
    }
}