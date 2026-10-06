using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using M2007U_UwUnity_OwOFunctions;
using UnityEngine.Animations;
using UnityEngine.UI;
using System.IO;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class M2007U_UwUnity_TextFileToUIText : EditorWindow
{
    M2007U_UwUnity_EOwObject EOwO = new M2007U_UwUnity_EOwObject();
    M2007U_UwUnity_FOwObject FOwO = new M2007U_UwUnity_FOwObject();

    GameObject USER_ParentObject ;
    int USER_FontSize = 200;

    [MenuItem("M2007U UwUnity tOwOls/Text File to UI Text")]
    private static void ShowWindow()
    {
        var window = GetWindow<M2007U_UwUnity_TextFileToUIText>();
        window.titleContent = new GUIContent("Text File to UI Text");
        window.Show();
    }

    private void OnEnable() 
    {
        
    }

    private void OnGUI()
    {
        USER_ParentObject = EditorGUILayout.ObjectField("Parent Object",USER_ParentObject,typeof(GameObject),true) as GameObject;

        if(GUILayout.Button("Generate UI Text"))
        {
            //is parent object assigned ?
            //no ? console error
            if (USER_ParentObject == null)
            {
                Debug.LogError("M2007U UwUnity tOwOls : Parent Game Object missing");
                return;
            }

            //pick a text file
            string FilePath = EditorUtility.OpenFilePanel( "Select a txt file" , "" , "txt" );
            string Content = "";
            if (string.IsNullOrEmpty(FilePath))
            {
                Debug.LogError("M2007U UwUnity tOwOls : no txt file selected");
                return;
            }
            
            //get text
            Content = File.ReadAllText(FilePath);
            
            //split by line
            string[] TextLines = Content.Split(new string[] { "\r\n", "\n" },System.StringSplitOptions.None);

            //based on every line
            //generate UI Text onto Parent
            for(int i = 0 ; i < TextLines.Length ; i++)
            {
                // make a new gameobject (empty) with the name of the line
                GameObject newObject = new GameObject( TextLines[i] , typeof(RectTransform)); 

                //attach to root
                newObject.transform.SetParent( USER_ParentObject.transform );

                //add UI Text component on it
                Text komponentText = newObject.AddComponent<Text>();

                //set the text
                komponentText.text = TextLines[i];

            }
        }
    }
}

