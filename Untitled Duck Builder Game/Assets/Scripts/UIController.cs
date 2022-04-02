using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    private Button startButton;
    private Button optionsButton;
    private Button exitButton;

    private void Awake() {
        var root = GetComponent<UIDocument>().rootVisualElement;

        startButton = root.Q<Button>("start-button");
        optionsButton = root.Q<Button>("options-button");
        exitButton = root.Q<Button>("exit-button");

        startButton.clicked += StartTheGame;
        optionsButton.clicked += ShowOptions;
        exitButton.clicked += ExitFromGame;
    }

    private void StartTheGame(){
        SceneManager.LoadScene("");
    }

    private void ShowOptions(){

    }

    private void ExitFromGame(){
        Application.Quit();
    }
}
