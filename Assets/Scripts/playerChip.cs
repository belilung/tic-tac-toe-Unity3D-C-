using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerChip : MonoBehaviour {

    private int owner = -1;

    private int i, j;

    // Use this for initialization
    void Start () {
        this.owner = -1;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //if owner == 1, then cross
    //setter block
    public void setOwner(int owner)
    {
        this.owner = owner;
    }

    public void setI(int i)
    {
        this.i = i;
    }


    public void setJ(int j)
    {
        this.j = j;
    }

    public void setColor(Color color)
    {
        this.GetComponent<Renderer>().material.color = color;
    }

    //getter block
    public int getOwner()
    {
        return this.owner;
    }

    public int getI()
    {
        return this.i;
    }

    public int getJ()
    {
        return this.j;
    }
    
}
