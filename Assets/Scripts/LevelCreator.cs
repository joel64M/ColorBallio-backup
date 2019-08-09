using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class LevelCreator : MonoBehaviour
{
  
    public bool isSpawned;
    [Range(1, 50)]
    public int widthX = 10;
    public int startZ = 0;

    public int endZ = 50;
    //public GameObject[] groundPrefabs;
    //public GameObject[] sidesPrefabs;
    //public GameObject[] toppingsPrefabs;
    //public GameObject[] treesPrrefabs;

    [System.Serializable]
    public class prefabClass
    {
        public string Name;
        public GameObject[] prefab;
    }

    public List<prefabClass> pList = new List<prefabClass>();
}
