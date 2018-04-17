using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRuler : MonoBehaviour {
    public int total;
    private Dictionary<Color, int> scoreRule = new Dictionary<Color, int>();

	// Use this for initialization
	void Start () {
        total = 0;
        scoreRule.Add(Color.red, 1);
        scoreRule.Add(Color.blue, 2);
        scoreRule.Add(Color.gray, 4);
	}
	
	public void Compute(GameObject obj)
    {
        total += scoreRule[obj.GetComponent<UFOInfo>().color];
    }

    public void Reset()
    {
        total = 0;
    }
}
