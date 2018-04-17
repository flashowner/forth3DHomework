using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { ROUND_START, ROUND_FINISH, RUNNING, PAUSE, START, WIN}

public interface IUserAction {
    GameState getGameState();
    void setGameState(GameState state);
    int getScore();
    void hitObject(Vector3 pos);
    void gameOver();

}
