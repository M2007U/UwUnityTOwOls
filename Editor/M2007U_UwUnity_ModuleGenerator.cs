using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using PlasticGui.WorkspaceWindow.Home;

using M2007U_UwUnity_OwOFunctions;




#if UNITY_EDITOR
using UnityEditor;
#endif

public class M2007U_UwUnity_ModuleGenerator : EditorWindow
{
    M2007U_UwUnity_EOwObject EOwO = new M2007U_UwUnity_EOwObject();
    M2007U_UwUnity_FOwObject FOwO = new M2007U_UwUnity_FOwObject();

    Vector2 SkwollingVektor = new Vector2(0f,0f);

    GameObject USER_ParentObject;

    int USER_Pwefabs_size = 1;
    List<GameObject> USER_Pwefabs_objects;
    List<float> USER_Pwefabs_prob;

    Vector3 USER_transform_pos_min = new Vector3(0f,0f,0f);
    Vector3 USER_transform_pos_max = new Vector3(0f,0f,0f);
    Vector3 USER_transform_rot_min = new Vector3(0f,0f,0f);
    Vector3 USER_transform_rot_max = new Vector3(0f,0f,0f);

    bool USER_transform_isGrid = false;
    Vector3 USER_transform_pos_grd = new Vector3(1f,1f,1f);

    bool USER_Pwefabs_Name_bool = true;
    int USER_Pwefabs_Name_Index = 0;
    string USER_Pwefabs_Name_L = "(";
    string USER_Pwefabs_Name_R = ")";

    
    [MenuItem("M2007U UwUnity tOwOls/Module Generator")]
    private static void ShowWindow()
    {
        var window = GetWindow<M2007U_UwUnity_ModuleGenerator>();
        window.titleContent = new GUIContent("Module Generator");
        window.Show();
    }

    private void OnEnable() 
    {
        USER_Pwefabs_objects = new List<GameObject>();
        USER_Pwefabs_prob = new List<float>();
    }

    private void OnGUI()
    {
        SkwollingVektor = EditorGUILayout.BeginScrollView(SkwollingVektor);

            EditorGUILayout.LabelField("Instantiates a GameObject from a list of choice with probability distribution under a selected parent");

        //===== get parent Object
            
            EOwO.MakeSpace(2);
            USER_ParentObject = EditorGUILayout.ObjectField("Parent Object",USER_ParentObject,typeof(GameObject),true) as GameObject;


        //===== get pwefabs

            EOwO.MakeSpace(2);

            //get the user desired size
            USER_Pwefabs_size = Mathf.Max(0,EditorGUILayout.IntField("List Size",USER_Pwefabs_size));

            //adjust the size
            while(USER_Pwefabs_size != USER_Pwefabs_objects.Count)
            {
                if (USER_Pwefabs_size > USER_Pwefabs_objects.Count)
                {
                    USER_Pwefabs_objects.Add(null);
                }
                else
                {
                    USER_Pwefabs_objects.RemoveAt(USER_Pwefabs_objects.Count - 1);
                }
            }
            while(USER_Pwefabs_size != USER_Pwefabs_prob.Count)
            {
                if (USER_Pwefabs_size > USER_Pwefabs_prob.Count)
                {
                    USER_Pwefabs_prob.Add(0f);
                }
                else
                {
                    USER_Pwefabs_prob.RemoveAt(USER_Pwefabs_prob.Count - 1);
                }
            }
            for(int i = 0 ; i < USER_Pwefabs_size ; i++)
            {
                EditorGUILayout.BeginHorizontal();
                USER_Pwefabs_objects[i] = EditorGUILayout.ObjectField("Choice" + i,USER_Pwefabs_objects[i],typeof(GameObject),true) as GameObject; //(name,what will it become next "frame",dataype, allow scene object ?)
                USER_Pwefabs_prob[i] = EditorGUILayout.FloatField("prob" + i,USER_Pwefabs_prob[i]);
                EditorGUILayout.EndHorizontal();
            }

        //===== get namings

            EOwO.MakeSpace(2);
            USER_Pwefabs_Name_bool = EditorGUILayout.Toggle("Use Index Naming",USER_Pwefabs_Name_bool);
            USER_Pwefabs_Name_Index = EditorGUILayout.IntField("Naming Index",USER_Pwefabs_Name_Index);
            USER_Pwefabs_Name_L = EditorGUILayout.TextField("Naming Index L Side",USER_Pwefabs_Name_L);
            USER_Pwefabs_Name_R = EditorGUILayout.TextField("Naming Index R Side",USER_Pwefabs_Name_R);

        //===== get offsets

            EOwO.MakeSpace(2);
            USER_transform_pos_min = EditorGUILayout.Vector3Field("Scatter Min Boundary", USER_transform_pos_min);
            USER_transform_pos_max = EditorGUILayout.Vector3Field("Scatter Max Boundary", USER_transform_pos_max);
            USER_transform_rot_min = EditorGUILayout.Vector3Field("Rotation Min Boundary", USER_transform_rot_min);
            USER_transform_rot_max = EditorGUILayout.Vector3Field("Rotation Max Boundary", USER_transform_rot_max);
            EOwO.MakeSpace(1);
            USER_transform_isGrid = EditorGUILayout.Toggle("Scatter as grid",USER_transform_isGrid);
            USER_transform_pos_grd = EditorGUILayout.Vector3Field("Grid Size",USER_transform_pos_grd);

        //===== now the actions

            EOwO.MakeSpace(2);

            if(GUILayout.Button("Spawn Choice"))
            {
                if (EOwO_checkChoiceIfNotNull())
                {
                    float chosenIndexFloat = UnityEngine.Random.Range(0f,1f);
                    int chosenIndexInt = 0;

                    while(chosenIndexFloat > 0 && chosenIndexInt < USER_Pwefabs_prob.Count)
                    {
                        chosenIndexFloat = chosenIndexFloat - USER_Pwefabs_prob[chosenIndexInt];
                        chosenIndexInt++;
                    }

                    //if chosenIndexInt is out of range
                    chosenIndexInt = Math.Min(chosenIndexInt, USER_Pwefabs_objects.Count);
                    chosenIndexInt--;

                    GameObject KopyObject = Instantiate(USER_Pwefabs_objects[chosenIndexInt],USER_ParentObject.transform);

                    //set position and rotation
                    KopyObject.transform.localPosition = new 
                    Vector3
                    (
                        UnityEngine.Random.Range(USER_transform_pos_min.x,USER_transform_pos_max.x),
                        UnityEngine.Random.Range(USER_transform_pos_min.y,USER_transform_pos_max.y),
                        UnityEngine.Random.Range(USER_transform_pos_min.z,USER_transform_pos_max.z)
                    );

                    KopyObject.transform.eulerAngles = new
                    Vector3
                    (
                        UnityEngine.Random.Range(USER_transform_rot_min.x,USER_transform_rot_max.x),
                        UnityEngine.Random.Range(USER_transform_rot_min.y,USER_transform_rot_max.y),
                        UnityEngine.Random.Range(USER_transform_rot_min.z,USER_transform_rot_max.z)
                    );

                    //grid snap ?
                    if (USER_transform_isGrid)
                    {
                        KopyObject.transform.localPosition = new 
                        Vector3
                        (
                            FOwO.float_GridSnap(USER_transform_pos_grd.x,KopyObject.transform.localPosition.x),
                            FOwO.float_GridSnap(USER_transform_pos_grd.y,KopyObject.transform.localPosition.y),
                            FOwO.float_GridSnap(USER_transform_pos_grd.z,KopyObject.transform.localPosition.z)
                        );
                    }

                    //index naming ?
                    if (USER_Pwefabs_Name_bool)
                    {
                        KopyObject.name = USER_Pwefabs_objects[chosenIndexInt].name + USER_Pwefabs_Name_L + USER_Pwefabs_Name_Index + USER_Pwefabs_Name_R;
                        USER_Pwefabs_Name_Index++;
                    }
                }

                
            }

        EditorGUILayout.EndScrollView();

    }

    

    private void EOwO_GUI_MakeVec3(string Lable, ref Vector3 Vek3, float FieldWidth)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(Lable);
        Vek3.x = EditorGUILayout.FloatField("x",Vek3.x,GUILayout.Width(FieldWidth));
        Vek3.y = EditorGUILayout.FloatField("y",Vek3.y,GUILayout.Width(FieldWidth));
        Vek3.z = EditorGUILayout.FloatField("z",Vek3.z,GUILayout.Width(FieldWidth));
        EditorGUILayout.EndHorizontal();
    }

    private bool EOwO_checkChoiceIfNotNull()
    {
        bool Answer = true;

        for(int i = 0 ; i < USER_Pwefabs_objects.Count ; i++)
        {
            if (USER_Pwefabs_objects[i] == null)
            {
                Answer = false;
            }
        }

        return Answer;
    }

    
}

