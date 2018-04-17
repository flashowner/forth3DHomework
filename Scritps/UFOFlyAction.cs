using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOFlyAction : SSAction {
    float time;
    float gravity;
    float horizonSpeed;
    Vector3 direction;

    public override void Start()
    {
        enable = true;
        time = 0;
        gravity = 10F;
        horizonSpeed = gameobject.GetComponent<UFOInfo>().speed;
        direction = gameobject.GetComponent<UFOInfo>().direction;
    }

    public override void Update()
    {
        if (gameobject.activeSelf)
        {
            time += Time.deltaTime;

            transform.Translate(Vector3.down * gravity * time * Time.deltaTime);

            transform.Translate(direction * horizonSpeed * Time.deltaTime);

            if (this.transform.position.y < -6)
            {
                this.destroy = true;
                this.enable = false;
                this.callback.SSActionEvent(this);
            }
        }
    }

    public static UFOFlyAction getSSAction()
    {
        UFOFlyAction action = ScriptableObject.CreateInstance<UFOFlyAction>();
        return action;
    }
}
