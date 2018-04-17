using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour {
    private IUserAction action;
    bool judge = true;

    void Start()
    {
        action = Director.getInstance().currentSceneControl as IUserAction;
    }

    private void OnGUI()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 pos = Input.mousePosition;
            action.hitObject(pos);
        }

        GUI.Label(new Rect(1000, 0, 400, 400), action.getScore().ToString() );
        if (GUI.RepeatButton(new Rect(0, 0, 100, 20), "Game Rule"))
        {
            GUI.TextArea(new Rect(0, 0, 100, 400), "    GameRule\nThis is a geme for Hitting UFO, there are three different rounds," +
                "if you hit the red one, you can get 1 point, if you hit the blue one you can get 2 points, if you hit the green one, " +
                "you can get 4 points. You will not win until you get 10 points. If you don't win after the three rounds, the rounds will " +
                "loop from the beginning.");
        }

        if (judge && GUI.Button(new Rect(650, 250, 90, 90), "Start"))
        {
            judge = false;
            action.setGameState(GameState.ROUND_START);
        }

        if (!judge && action.getGameState() == GameState.ROUND_FINISH && GUI.Button(new Rect(650, 250, 90, 90), "Next Round"))
        {
            action.setGameState(GameState.ROUND_START);
        }

        if (action.getGameState() == GameState.WIN)
        {
            GUI.color = Color.red;
            GUI.Label(new Rect(600, 300, 400, 400), "GAMWOVER");
        }
    }
}
