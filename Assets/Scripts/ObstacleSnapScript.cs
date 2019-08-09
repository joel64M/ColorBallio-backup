using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;
using PathCreation;

public class ObstacleSnapScript : MonoBehaviour
{
    public PathCreator pathCreator;


    int pointIndex;

    void Start()
    {
        pathCreator = GameObject.FindGameObjectWithTag("PathCreator").GetComponent<PathCreator>();
      //  print("LENGT OF POINTS ? " + pathCreator.path.localPoints.Length);
        Vector3 obstaclePos = transform.position;

        float tempLenght = 100f;


        for (int i = 0; i < pathCreator.path.localPoints.Length; i++)
        {
           if(Vector3.Distance(obstaclePos,pathCreator.path.localPoints[i]) < tempLenght)
            {
                tempLenght = Vector3.Distance(obstaclePos, pathCreator.path.localPoints[i]);
                pointIndex = i;
            }
        }

        Vector3 newObstaclePos = (pathCreator.path.localPoints[pointIndex]);
        transform.position = newObstaclePos;

        //  float angle = (CalculateAngle(pathCreator.path.localPoints[pointIndex ], pathCreator.path.localPoints[pointIndex + 1]) );
        //(Vector3.Angle(pathCreator.path.localPoints[pointIndex-1], pathCreator.path.localPoints[pointIndex+1]));
    //    transform.eulerAngles = pathCreator.path.localTangents[pointIndex];  //  new Vector3(0, angle , 0);

    }


    public static float CalculateAngle(Vector3 from, Vector3 to)
    {
        return Quaternion.FromToRotation(Vector3.up, to - from).eulerAngles.z;
    }
}
