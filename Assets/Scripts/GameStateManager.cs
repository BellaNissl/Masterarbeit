using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    [SerializeField] private Canvas _gameOverCanvas;
    [SerializeField] private GameObject _gameOver;
    [SerializeField] private GameObject _gameWon;
    [SerializeField] private GameObject _reason;
    private bool _gameInteractable = true;

    public bool IsGameInteractable() {
        return _gameInteractable;
    }

    private void Awake(){
        Instance = this;
        _gameOverCanvas.enabled = false;
    }

    public void RetryButtonClicked() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EndGameButtonClicked(){
        Application.Quit();
    }

    public void DisplayGameOverScreen(string message) {
        _gameOverCanvas.enabled = true;
        _gameOver.SetActive(true);
        _reason.SetActive(true);
        _gameWon.SetActive(false);
        _reason.GetComponent<TextMeshProUGUI>().text = message;
        _gameInteractable = false;
    }


    public void DisplayGameWonScreen() {
        _gameOverCanvas.enabled = true;
        _gameOver.SetActive(false);
        _reason.SetActive(false);
        _gameWon.SetActive(true);
        _gameInteractable = false;
    }
}
