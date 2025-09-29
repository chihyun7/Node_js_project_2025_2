using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameView gameView;
    public GameController GameController;
    // Start is called before the first frame update
    void Start()
    {
        GameController = gameView.gameObject.AddComponent<GameController>();
        GameController.gameView = gameView;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
