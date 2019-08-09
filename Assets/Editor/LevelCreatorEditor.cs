using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(LevelCreator))]
public class LevelCreatorEditor : Editor
{

    // GameObject[] prefabs;
    GameObject go;

    GameObject[] groundPrefabs;
    GameObject[] sidePrefabs;
    GameObject[] treePrefabs;
    GameObject[] toppingPrefabs;

    GameObject selectedPrefab;
    int selectedPrefabIndex = 0;

    Vector3 rotation = Vector3.zero;
    Transform parent;

    List<GameObject> spawnedGO = new List<GameObject>();

    public override void OnInspectorGUI()
    {
    
        DrawDefaultInspector();
   
        if (go == null)
        {
            go = GameObject.FindGameObjectWithTag("LevelCreator");
          
        }
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Instiantiate", GUILayout.MaxWidth(80), GUILayout.MaxHeight(20)))
        {

            if (!go.GetComponent<LevelCreator>().isSpawned)
            {

                go.GetComponent<LevelCreator>().isSpawned = true;
                SpawnFromCorner();
            }
            EditorWindow.FocusWindowIfItsOpen<SceneView>();
        }
        if (GUILayout.Button("Replace", GUILayout.MaxWidth(80), GUILayout.MaxHeight(20)))
        {
            // selectedPrefab = go.GetComponent<Map>().pList[j].prefab[i];
            //  selectedPrefabIndex = j;//go.GetComponent<Map>().pList[j].prefab.Length;
         ///   ReplaceFromCorner();
            EditorWindow.FocusWindowIfItsOpen<SceneView>();
        }
        GUILayout.EndHorizontal();
        #region List
        for (int j = 0; j < go.GetComponent<LevelCreator>().pList.Count; j++)
        {
            GUILayout.BeginHorizontal();
            if(EditorPrefs.GetBool("BOOLS" + j))
            GUI.backgroundColor = new Color32(0, 255, 0,255);

            var style = new GUIStyle(EditorStyles.toolbarButton);
            style.normal.textColor =Color.black;
            if (GUILayout.Button("Show/Hide " + go.GetComponent<LevelCreator>().pList[j].Name, style)) //, GUILayout.MaxWidth(210), GUILayout.MaxHeight(20)
            {
                EditorPrefs.SetBool("BOOLS"+j, !EditorPrefs.GetBool("BOOLS"+j));
           

            }
            GUI.backgroundColor = Color.white;
            //  GUILayout.Label(" Element " + j.ToString());
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (go.GetComponent<LevelCreator>().pList[j].prefab != null && EditorPrefs.GetBool("BOOLS" + j))
            {
               
                int elementsInThisRow = 0;
                for (int i = 0; i < go.GetComponent<LevelCreator>().pList[j].prefab.Length; i++)
                {
                    elementsInThisRow++;
  
                    Texture prefabTexture = AssetPreview.GetAssetPreview(go.GetComponent<LevelCreator>().pList[j].prefab[i]);
                                                                                                       

                    if (GUILayout.Button(prefabTexture, GUILayout.MaxWidth(50), GUILayout.MaxHeight(50)))
                    {
                        selectedPrefab = go.GetComponent<LevelCreator>().pList[j].prefab[i];
                        selectedPrefabIndex = j;//go.GetComponent<Map>().pList[j].prefab.Length;
                        EditorWindow.FocusWindowIfItsOpen<SceneView>();
                    }
                    //move to next row after creating a certain number of buttons so it doesn't overflow horizontally
                    if (elementsInThisRow > Screen.width / 70)
                    {
                        elementsInThisRow = 0;
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                    }
                }
            }
         
            GUILayout.EndHorizontal();
        }

        #endregion

        #region unused code
        /*
        GUILayout.Label("Sides");
        #region sideprefabs
        GUILayout.BeginHorizontal();
        if (sidePrefabs != null)
        {
            int elementsInThisRow = 0;
            for (int i = 0; i < sidePrefabs.Length; i++)
            {
                elementsInThisRow++;
                //get the texture from the prefabs
                Texture prefabTexture = AssetPreview.GetAssetPreview(sidePrefabs[i]);// groundPrefabs[i].GetComponent().sprite.texture;
                //create one button for each prefab
                //if a button is clicked, select that prefab and focus on the scene view
                if (GUILayout.Button(prefabTexture, GUILayout.MaxWidth(50), GUILayout.MaxHeight(50)))
                {
                    selectedPrefab = sidePrefabs[i];
                    if (!go.GetComponent<Map>().isSpawned)
                    {

                        go.GetComponent<Map>().isSpawned = true;
                        SpawnFromCorner();
                    }
                    EditorWindow.FocusWindowIfItsOpen<SceneView>();
                }
                //move to next row after creating a certain number of buttons so it doesn't overflow horizontally
                if (elementsInThisRow > Screen.width / 70)
                {
                    elementsInThisRow = 0;
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                }
            }
        }
        GUILayout.EndHorizontal();
        #endregion
        GUILayout.Label("Trees");
        #region treePrefabs
        GUILayout.BeginHorizontal();
        if (treePrefabs != null)
        {
            int elementsInThisRow = 0;
            for (int i = 0; i < treePrefabs.Length; i++)
            {
                elementsInThisRow++;
                //get the texture from the prefabs
                Texture prefabTexture = AssetPreview.GetAssetPreview(treePrefabs[i]);// groundPrefabs[i].GetComponent().sprite.texture;
                //create one button for each prefab
                //if a button is clicked, select that prefab and focus on the scene view
                if (GUILayout.Button(prefabTexture, GUILayout.MaxWidth(50), GUILayout.MaxHeight(50)))
                {
                    selectedPrefab = treePrefabs[i];
                    EditorWindow.FocusWindowIfItsOpen<SceneView>();
                }
                //move to next row after creating a certain number of buttons so it doesn't overflow horizontally
                if (elementsInThisRow > Screen.width / 70)
                {
                    elementsInThisRow = 0;
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                }
            }
        }
        GUILayout.EndHorizontal();
        #endregion
        GUILayout.Label("Toppings");
        #region toppingsPrefabs
        GUILayout.BeginHorizontal();
        if (toppingPrefabs != null)
        {
            int elementsInThisRow = 0;
            for (int i = 0; i < toppingPrefabs.Length; i++)
            {
                elementsInThisRow++;
                //get the texture from the prefabs
                Texture prefabTexture = AssetPreview.GetAssetPreview(toppingPrefabs[i]);// groundPrefabs[i].GetComponent().sprite.texture;
                //create one button for each prefab
                //if a button is clicked, select that prefab and focus on the scene view
                if (GUILayout.Button(prefabTexture, GUILayout.MaxWidth(50), GUILayout.MaxHeight(50)))
                {
                    selectedPrefab = toppingPrefabs[i];
                    EditorWindow.FocusWindowIfItsOpen<SceneView>();
                }
                //move to next row after creating a certain number of buttons so it doesn't overflow horizontally
                if (elementsInThisRow > Screen.width / 70)
                {
                    elementsInThisRow = 0;
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                }
            }
        }
        GUILayout.EndHorizontal();
        #endregion

        */

        #endregion
    }


    void OnSceneGUI()
    {
       
        Handles.BeginGUI();
        GUILayout.Box("Map Edit Mode");
        if (selectedPrefab == null)
        {
            GUILayout.Box("No prefab selected!");
        }
        Handles.EndGUI();

        Vector3 spawnPosition = Vector3.zero;

        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.V)
        {

            Vector3 mousePos = Event.current.mousePosition;
            float ppp = EditorGUIUtility.pixelsPerPoint;
            mousePos.y = Camera.current.pixelHeight - mousePos.y * ppp;
            mousePos.x *= ppp;

            Ray ray = Camera.current.ScreenPointToRay(mousePos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {



                if (!hit.transform.gameObject.CompareTag("Character"))
                {
                    if (!hit.transform.gameObject.CompareTag("Ground"))
                    {
                        spawnPosition = hit.transform.position;//   new Vector3(Mathf.Round(hit.point.x), Mathf.Round(hit.point.y), Mathf.Round(hit.point.z));
                        DestroyImmediate(hit.transform.gameObject);

                        //    spawnPosition = new Vector3(Mathf.Round(hit.point.x), Mathf.Round(hit.point.y ), Mathf.Round(hit.point.z));

                        // if (!hit.transform.gameObject.CompareTag(selectedPrefab.gameObject.tag))
                        {
                            Spawn(spawnPosition);

                        }
                    }
                }

            }
        }
        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.D)
        {

            Vector3 mousePos = Event.current.mousePosition;
            float ppp = EditorGUIUtility.pixelsPerPoint;
            mousePos.y = Camera.current.pixelHeight - mousePos.y * ppp;
            mousePos.x *= ppp;

            Ray ray = Camera.current.ScreenPointToRay(mousePos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
              


                if (!hit.transform.gameObject.CompareTag("Character"))
                {
                    if (!hit.transform.gameObject.CompareTag("Ground"))
                    {
                        spawnPosition = hit.transform.position;//   new Vector3(Mathf.Round(hit.point.x), Mathf.Round(hit.point.y), Mathf.Round(hit.point.z));
                        DestroyImmediate(hit.transform.gameObject);

                    //    spawnPosition = new Vector3(Mathf.Round(hit.point.x), Mathf.Round(hit.point.y ), Mathf.Round(hit.point.z));

                       // if (!hit.transform.gameObject.CompareTag(selectedPrefab.gameObject.tag))
                        {
                            SpawnRandom(spawnPosition);
                        }
                    }
                }

            }
        }
        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.C)
        {

            Vector3 mousePos = Event.current.mousePosition;
            float ppp = EditorGUIUtility.pixelsPerPoint;
            mousePos.y = Camera.current.pixelHeight - mousePos.y * ppp;
            mousePos.x *= ppp;

            Ray ray = Camera.current.ScreenPointToRay(mousePos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {



                if (!hit.transform.gameObject.CompareTag("Character"))
                {
                    if (!hit.transform.gameObject.CompareTag("Ground"))
                    {
                        spawnPosition = hit.transform.position;//   new Vector3(Mathf.Round(hit.point.x), Mathf.Round(hit.point.y), Mathf.Round(hit.point.z));
                                                               //  DestroyImmediate(hit.transform.gameObject);
                      //  GameObject go = ;
                        //    spawnPosition = new Vector3(Mathf.Round(hit.point.x), Mathf.Round(hit.point.y ), Mathf.Round(hit.point.z));
                        rotation.y += 90;
                        hit.transform.gameObject.transform.rotation = Quaternion.Euler(rotation);
                        // if (!hit.transform.gameObject.CompareTag(selectedPrefab.gameObject.tag))
                      //  {
                          //  SpawnRandom(spawnPosition);
                      //  }
                    }
                }

            }
        }
        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.A)
        {
            
            Vector3 mousePos = Event.current.mousePosition;
            float ppp = EditorGUIUtility.pixelsPerPoint;
            mousePos.y = Camera.current.pixelHeight - mousePos.y * ppp;
            mousePos.x *= ppp;

            Ray ray = Camera.current.ScreenPointToRay(mousePos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                spawnPosition = new Vector3(Mathf.Round(hit.point.x), Mathf.Round(hit.point.y + 0.5f), Mathf.Round(hit.point.z));

                if (!hit.transform.gameObject.CompareTag(selectedPrefab.gameObject.tag))
                {
                    Spawn(spawnPosition);
                }
            }
        }

        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.S)
        {
           // EditorApplication.SaveScene();
            EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
        }


        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.X)
        {
            
            Vector3 mousePos = Event.current.mousePosition;
            float ppp = EditorGUIUtility.pixelsPerPoint;
            mousePos.y = Camera.current.pixelHeight - mousePos.y * ppp;
            mousePos.x *= ppp;

            Ray ray = Camera.current.ScreenPointToRay(mousePos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (!hit.transform.gameObject.CompareTag("Character"))
                {
                    if (!hit.transform.gameObject.CompareTag("Ground"))
                        DestroyImmediate(hit.transform.gameObject);
                }

                //   Debug.Log("Instantiated at " + hit.point);
            }
        }
        //if 'E' pressed, spawn the selected prefab

        //if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.E)
        //{

        //    Spawn(spawnPosition);
        //}

        //if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Z)
        //{
        //    if (spawnedGO.Count > 0)
        //    {
        //        DestroyImmediate(spawnedGO[spawnedGO.Count - 1]);
        //        spawnedGO.RemoveAt(spawnedGO.Count - 1);
        //    }
        //}
    }

    GameObject selectedGameObject;
    void Spawn(Vector3 _spawnPosition )
    {
        if (selectedPrefab != null)
        {
            if (parent == null)
            {
                GameObject gg = new GameObject("Environment");
                parent = gg.transform;
            }
       
              //  GameObject goo = (GameObject)Instantiate(selectedPrefab, new Vector3(_spawnPosition.x, _spawnPosition.y, _spawnPosition.z), Quaternion.identity, parent);
            GameObject goo= (GameObject)PrefabUtility.InstantiatePrefab(selectedPrefab);
            goo.gameObject.transform.SetParent(parent);
            goo.gameObject.transform.position = new Vector3(_spawnPosition.x, _spawnPosition.y, _spawnPosition.z);
            selectedGameObject = goo;
                goo.name = selectedPrefab.name;
                spawnedGO.Add(goo);
        }
    }
    void SpawnRandom(Vector3 _spawnPosition)
    {
        if (selectedPrefab != null)
        {
            if (parent == null)
            {
                GameObject gg = new GameObject("Environment");
                parent = gg.transform;
            }
            GameObject goRandom = go.GetComponent<LevelCreator>().pList[selectedPrefabIndex].prefab[Random.Range(0, go.GetComponent<LevelCreator>().pList[selectedPrefabIndex].prefab.Length)];
            GameObject goo = (GameObject)Instantiate(goRandom, new Vector3(_spawnPosition.x, _spawnPosition.y, _spawnPosition.z), Quaternion.identity, parent);
            selectedGameObject = goo;
            goo.name = selectedPrefab.name;
            spawnedGO.Add(goo);
        }
    }

    /*
    void ReplaceFromCorner()
    {
      
        for (int i = -(go.GetComponent<LevelCreator>().gridSize -3); i <= go.GetComponent<LevelCreator>().gridSize -3 ; i++)
        {
            for (int j = -(go.GetComponent<LevelCreator>().gridSize-3); j <= go.GetComponent<LevelCreator>().gridSize - 3; j++)
            {
              //  RaycastHit hit;
             //   Ray ray = new Ray(new Vector3(i, .5f, j));
                Vector3 vec = new Vector3(i, 1, j);
                Collider[] cd =  Physics.OverlapSphere(vec, 0.4f);
               
                if (cd.Length > 0)
                {
                    if (cd[0].transform.gameObject.CompareTag("Block"))
                    {
                       
                        float r = Random.Range(0f, 20f);
                        if (r > 18.99f)
                        {
                            DestroyImmediate(cd[0].transform.gameObject);
                            SpawnRandom(vec);
                        }
                 
                        //Debug.Log("Collided with " + cd[0].transform.name);
                    }
                }
               
            
                
                  
            }
        }
    }
    */
    void SpawnFromCorner()
    {
        int widthX = go.GetComponent<LevelCreator>().widthX;
        int startZ = go.GetComponent<LevelCreator>().startZ;
        int endZ = go.GetComponent<LevelCreator>().endZ;
        for (int i = -widthX; i <= widthX; i++)
        {
            for (int j = startZ; j <= endZ; j++)
            {
                Vector3 vec = new Vector3(i, 0, j);
                /*
                Collider[] cd = Physics.OverlapSphere(vec, 0.4f);
                if (cd.Length > 0)
                {
                    if (cd[0].transform.gameObject.CompareTag("Block") && cd[0].transform.gameObject.CompareTag("Goal"))
                    {
                        Debug.Log("Collided with " + cd[0].transform.name);
                        DestroyImmediate(cd[0].transform.gameObject);
                        Spawn(vec);
                       
                    }


                }
                else
                */               
                {
                    Spawn(vec);
                }
             
       
                // Spawn(new Vector3(i, 1, j));

            }
        }
       
    }
}
