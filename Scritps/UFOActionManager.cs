using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOActionManager : SSActionManager, ISSActionCallback {
    public FirstSceneControl sceneController;
    public List<UFOFlyAction> store;
    public int UFONumber = 0;

    private List<SSAction> used = new List<SSAction>();
    private List<SSAction> unused = new List<SSAction>();

    SSAction getSSAction()
    {
        SSAction action = null;
        if (unused.Count > 0)
        {
            action = unused[0];
            unused.Remove(unused[0]);
        } else
        {
            action = ScriptableObject.Instantiate<UFOFlyAction>(store[0]);
        }

        used.Add(action);
        return action;
    }

    public void releaseSSACtion(SSAction action)
    {
        SSAction ssa = null;
        foreach (SSAction i in used)
        {
            if (action.GetInstanceID() == i.GetInstanceID())
                ssa = i;
        }

        if (ssa != null)
        {
            ssa.reset();
            unused.Add(ssa);
            used.Remove(ssa);
        }
    }
	
	protected new void Start()
    {
        sceneController = (FirstSceneControl)Director.getInstance().currentSceneControl;
        sceneController.actionManager = this;
        store.Add(UFOFlyAction.getSSAction());
    }

    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted,
        int intParam = 0, string strParam = null, UnityEngine.Object objectParam = null)
    {
        if (source is UFOFlyAction)
        {
            UFONumber--;
            UFOFactory ufof = Singleton<UFOFactory>.Instance;
            ufof.recoverUFO(source.gameobject);
            releaseSSACtion(source);
        }
    }

    public void StartRun(Queue<GameObject> UFOQueue)
    {
        foreach (GameObject item in UFOQueue)
        {
            RunAction(item, getSSAction(), (ISSActionCallback)(this));
        }
    }
}
