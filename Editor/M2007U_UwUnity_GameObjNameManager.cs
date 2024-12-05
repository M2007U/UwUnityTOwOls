using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using M2007U_UwUnity_OwOFunctions;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class M2007U_UwUnity_GameObjNameManager : EditorWindow
{
    M2007U_UwUnity_EOwObject EOwO = new M2007U_UwUnity_EOwObject();

    Vector2 SkwollingVektor = new Vector2(0f,0f);

    string USER_prefix = "";

    bool USER_index_use = true;
    int USER_index_current = 0;
    int USER_index_step = 1;
    string USER_index_format = "";
    string USER_Index_L = "";
    string USER_Index_R = "";

    string USER_postfix = "";

    [MenuItem("M2007U UwUnity tOwOls/Game Obj Name Manager")]
    private static void ShowWindow()
    {
        var window = GetWindow<M2007U_UwUnity_GameObjNameManager>();
        window.titleContent = new GUIContent("Game Obj Name Manager");
        window.Show();
    }

    private void OnEnable()
    {

    }

    private void OnGUI()
    {
        SkwollingVektor = EditorGUILayout.BeginScrollView(SkwollingVektor);


        EditorGUILayout.LabelField("Renames the Selected GameObjects with Index");

        EOwO.MakeSpace(2);
        EditorGUILayout.LabelField("Prefix");
        USER_prefix = EditorGUILayout.TextField("Name Prefix",USER_prefix);

        EOwO.MakeSpace(2);
        EditorGUILayout.LabelField("Indexing");
        USER_index_use = EditorGUILayout.Toggle("Use indexing",USER_index_use);
        USER_index_current = EditorGUILayout.IntField("Starting Index",USER_index_current);
        USER_index_step = EditorGUILayout.IntField("Index Step",USER_index_step);
        USER_index_format = EditorGUILayout.TextField("Index Format String",USER_index_format);
        USER_Index_L = EditorGUILayout.TextField("L Bracket",USER_Index_L);
        USER_Index_R = EditorGUILayout.TextField("R Bracket",USER_Index_R);

        EOwO.MakeSpace(2);
        EditorGUILayout.LabelField("Postfix");
        USER_postfix = EditorGUILayout.TextField("Name Postfix",USER_postfix);


        EOwO.MakeSpace(2);

        if(GUILayout.Button("Rename Selected Game Objects"))
        {
            if (Selection.gameObjects.Length > 0)
            {
                GameObject[] ListGO = Selection.gameObjects;
                
                for(int i = 0 ; i < ListGO.Length ; i++)
                {
                    string LOCAL_Part_Index = "";
                    if(USER_index_use)
                    {
                        if(USER_index_format == "")
                        {
                            LOCAL_Part_Index = USER_index_current.ToString(USER_index_format);
                        }
                        else
                        {
                            LOCAL_Part_Index = USER_Index_L + USER_index_current.ToString(USER_index_format) + USER_Index_R;
                        }
                    }

                    ListGO[i].name = USER_prefix + LOCAL_Part_Index + USER_postfix;
                    USER_index_current = USER_index_current + USER_index_step;
                }
                
                
                
            }
        }

        if(GUILayout.Button("Append name to Selected Game Objects"))
        {
            if (Selection.gameObjects.Length > 0)
            {
                GameObject[] ListGO = Selection.gameObjects;
                
                for(int i = 0 ; i < ListGO.Length ; i++)
                {
                    string LOCAL_Part_Index = "";
                    if(USER_index_use)
                    {
                        if(USER_index_format == "")
                        {
                            LOCAL_Part_Index = USER_index_current.ToString(USER_index_format);
                        }
                        else
                        {
                            LOCAL_Part_Index = USER_Index_L + USER_index_current.ToString(USER_index_format) + USER_Index_R;
                        }
                    }

                    ListGO[i].name = ListGO[i].name + USER_prefix + LOCAL_Part_Index + USER_postfix;
                    USER_index_current = USER_index_current + USER_index_step;
                }
            }
        }

        EditorGUILayout.EndScrollView();
    }
}
