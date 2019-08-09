using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

public class AIScript : MonoBehaviour
{

    PathFollower pf;
    GameManagerScript gms;

    bool isAiStart;

    float speed;

    void Start()
    {
        pf = GetComponent<PathFollower>();
        gms = GameManagerScript.instance;
        speed = gms.speed;
    }

    void Update()
    {
        if (gms.isGameStart && !isAiStart)
        {
            isAiStart = true;
            pf.speed= speed;
        }
    }


}
