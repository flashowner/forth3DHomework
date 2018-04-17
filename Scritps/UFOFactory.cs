using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOFactory : MonoBehaviour {
    public GameObject ufoModel;

    private List<UFOInfo> used = new List<UFOInfo>();
    private List<UFOInfo> unused = new List<UFOInfo>();

    private void Awake()
    {
        ufoModel = GameObject.Instantiate((GameObject)Resources.Load("Prefabs/ufo"), Vector3.zero, Quaternion.identity);
        ufoModel.SetActive(false);
    }

    public GameObject getUFO(int round)
    {
        GameObject newItem = null;
        if (unused.Count > 0)
        {
            newItem = unused[0].gameObject;
            unused.Remove(unused[0]);
        }
        else
        {
            newItem = GameObject.Instantiate(ufoModel, Vector3.zero, Quaternion.identity);
            newItem.AddComponent<UFOInfo>();
        }

        switch (round)
        {
            case 0:
                {
                    newItem.GetComponent<UFOInfo>().color = Color.red;
                    newItem.GetComponent<UFOInfo>().speed = 4.0F;
                    float randomX = UnityEngine.Random.Range(-3f, 3f);
                    float randomY = UnityEngine.Random.Range(1f, 3f);
                    newItem.GetComponent<UFOInfo>().direction = new Vector3(randomX, randomY, 0);
                    newItem.GetComponent<Renderer>().material.color = Color.red;
                    break;
                }

            case 1:
                {
                    newItem.GetComponent<UFOInfo>().color = Color.blue;
                    newItem.GetComponent<UFOInfo>().speed = 6.0F;
                    float randomX = UnityEngine.Random.Range(-6f, 6f);
                    float randomY = UnityEngine.Random.Range(-3f, 3f);
                    newItem.GetComponent<UFOInfo>().direction = new Vector3(randomX, randomY, 0);
                    newItem.GetComponent<Renderer>().material.color = Color.blue;
                    break;
                }

            case 2:
                {
                    newItem.GetComponent<UFOInfo>().color = Color.green;
                    newItem.GetComponent<UFOInfo>().speed = 8.0F;
                    float randomX = UnityEngine.Random.Range(-10f, 10f);
                    float randomY = UnityEngine.Random.Range(1f, 3f);
                    newItem.GetComponent<UFOInfo>().direction = new Vector3(randomX, randomY, 0);
                    newItem.GetComponent<Renderer>().material.color = Color.green;
                    break;
                }
        }

        used.Add(newItem.GetComponent<UFOInfo>());
        newItem.name = newItem.GetInstanceID().ToString();
        return newItem;
    }

    public void recoverUFO(GameObject item)
    {
        UFOInfo data = null;
        foreach (UFOInfo i in used)
        {
            if (item.GetInstanceID() == i.gameObject.GetInstanceID())
            {
                data = i;
            }
        }
        if (data != null)
        {
            data.gameObject.SetActive(false);
            unused.Add(data);
            used.Remove(data);
        }
    }
}
