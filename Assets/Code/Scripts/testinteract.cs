using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testinteract : Item
{
    // Start is called before the first frame update
    void Start()
    {
        INTERACT_DISTANCE = 2f;
    }

    public override void Interact(){
        Debug.Log($"Interacting with {interaction.objectName}");
    }



    // Update is called once per frame
    // void Update()
    // {
       
    // }
}
