using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FirstSceneControl : MonoBehaviour, ISceneControl, IUserAction {

	public UFOActionManager actionManager { get; set; }

    public ScoreRuler scoreRuler { get; set; }

    public Queue<GameObject> ufoQueue = new Queue<GameObject>();

    private int ufoNumber;

    private int currentRound = -1;

    public int rounds = 3;

    private float interval = 0;

    private GameState state = GameState.START;

    private void Awake()
    {
        Director director = Director.getInstance();
        director.currentSceneControl = this;
        ufoNumber = 10;
        this.gameObject.AddComponent<ScoreRuler>();
        this.gameObject.AddComponent<UFOFactory>();
        scoreRuler = Singleton<ScoreRuler>.Instance;
        director.currentSceneControl.LoadResources();
    }

    public void LoadResources()
    {
        GameObject obj = GameObject.Instantiate((GameObject)Resources.Load("Prefabs/grass"));
    }

    private void Update()
    {
        if (state != GameState.WIN)
        {
            if (actionManager.UFONumber == 0 && state == GameState.RUNNING)
            {
                state = GameState.ROUND_FINISH;
            }

            if (actionManager.UFONumber == 0 && state == GameState.ROUND_START)
            {
                currentRound = (currentRound + 1) % rounds;
                nextRound();
                actionManager.UFONumber = 10;
                state = GameState.RUNNING;
            }

            if (this.getScore() > 10)
            {
                state = GameState.WIN;
            }

            if (interval > 1)
            {
                throwUFO();
                interval = 0;
            }
            else
            {
                interval += Time.deltaTime;
            }
        } 

    }

    public void hitObject(Vector3 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);

        RaycastHit[] objects;
        objects = Physics.RaycastAll(ray);
        for  (int i = 0; i < objects.Length; i++)
        {
            RaycastHit hit = objects[i];

            if (hit.collider.gameObject.GetComponent<UFOInfo>() != null)
            {
                scoreRuler.Compute(hit.collider.gameObject);
                hit.collider.gameObject.transform.position = new Vector3(0, -4, 0);
            }
        }
    }

    public int getScore()
    {
        return scoreRuler.total;
    }

    public GameState getGameState()
    {
        return state;
    }

    public void setGameState(GameState gs)
    {
        state = gs;
    }

    private void nextRound()
    {
        UFOFactory ufof = Singleton<UFOFactory>.Instance;
        for (int i = 0; i < ufoNumber; i++)
        {
            ufoQueue.Enqueue(ufof.getUFO(currentRound));
        }

        actionManager.StartRun(ufoQueue);
    }

    void throwUFO()
    {
        if (ufoQueue.Count != 0)
        {
            GameObject obj = ufoQueue.Dequeue();

            float y = UnityEngine.Random.Range(0F, 4F);
            obj.transform.position = new Vector3(-obj.GetComponent<UFOInfo>().direction.x, y, 0);

            obj.SetActive(true);
        }
    }

    public void gameOver()
    {
        GUI.color = Color.red;
        GUI.Label(new Rect(700, 300, 400, 400), "win");
    }
}
