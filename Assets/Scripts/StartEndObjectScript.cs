using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;
using PathCreation;
public class StartEndObjectScript : MonoBehaviour
{
    public PathCreator pathCreator;


   public bool isFinishObject;
    Vector3 newPos;

   void Start()
    {
        pathCreator = GameObject.FindGameObjectWithTag("PathCreator").GetComponent<PathCreator>();
        if (isFinishObject)
        {
           newPos = pathCreator.path.localPoints[pathCreator.path.localPoints.Length - 1];
            newPos.y = -0.3f;
            transform.position = newPos;
        }
        else
        {
            newPos = pathCreator.path.localPoints[0];
            newPos.y = -0.3f;
            transform.position = newPos;
        }
    }
}
