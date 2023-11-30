using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    private Button button;
    private Controller controller;
    public int difficulty;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
        controller = GameObject.Find("GameController").GetComponent<Controller>();
    }

    void SetDifficulty()
    {
        Debug.Log(gameObject.name + "was clicked");
        controller.StartGame(difficulty);
    }
}
