using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using M2007U_UwUnity_OwOFunctions;
using UnityEngine.Animations;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class M2007U_UwUnity_transfOwOrmController : EditorWindow
{
    M2007U_UwUnity_EOwObject EOwO = new M2007U_UwUnity_EOwObject();
    M2007U_UwUnity_FOwObject FOwO = new M2007U_UwUnity_FOwObject();

    Vector2 SkwollingVektor = new Vector2(0f,0f);


    GameObject USER_ParentObject;

    //scatter objects in a box 
    bool Foldout_Scatter_Box = false;
    Vector3 USER_Scatter_Box_Range_Min = new Vector3(0f,0f,0f);
    Vector3 USER_Scatter_Box_Range_Max = new Vector3(0f,0f,0f);

    ///scatter objects in a ring
    bool Foldout_Scatter_Ring = false;
    float USER_Scatter_Ring_Radius_Min = 0f;
    float USER_Scatter_Ring_Radius_Max = 16f;
    string USER_Scatter_Ring_Axis = "y";

    //scatter objects in a sphere
    bool Foldout_Scatter_Sphere = false;
    float USER_Scatter_Sphere_Radius_Min = 0f;
    float USER_Scatter_Sphere_Radius_Max = 16f;

    //grid snap
    bool Foldout_GridSnap = false;
    bool    USER_GridSnap_UseX = false;
    bool    USER_GridSnap_UseY = false;
    bool    USER_GridSnap_UseZ = false;
    Vector3 USER_GridSnap_Grid = new Vector3 (1f,1f,1f);

    //arrange objects into grids
    bool Foldout_GridArrange = false;
    Vector3 USER_GridArrange_AxisOrder = new Vector3(0f,1f,2f);
    Vector3Int USER_GridArrange_AxisSize = new Vector3Int(8,8,8); //the number of rows/columns along that axis
    Vector3 USER_GridArrange_Step = new Vector3(1f,1f,1f);
    Vector3Int LOCAL_GridArrange_Counter = new Vector3Int(0,0,0);

    //scale the local position of a list of Object
    bool Foldout_PositionScale = false;
    Vector3 USER_PositionScale_Scaler = new Vector3(1f,1f,1f);

    //surround the parent with the selected Objects
    bool Foldout_Surround = false;
    float USER_Surround_Radius = 1f;
    float USER_Surround_Phase = 0f;
    float USER_Surround_Step = 0f;
    string USER_Surround_Axis = "y";

    //lerping from a transform to another transform
    bool Foldout_LerpTransform = false;
    GameObject USER_LerpTransform_ObjectA;
    GameObject USER_LerpTransform_ObjectB;

    //having a starting refference, then iterate through a list, like array modifier in Blender
    bool Foldout_ArrayIterate = false;
    GameObject USER_ArrayIterate_Object;
    Vector3 USER_ArrayIterate_Step_Pos = new Vector3(0f, 0f, 0f);
    Vector3 USER_ArrayIterate_Step_Rot = new Vector3(0f, 0f, 0f);
    Vector3 USER_ArrayIterate_Step_Scl = new Vector3(0f, 0f, 0f);

    //let a list of GameObject has a random scale
    bool Foldout_ScaleRandom = false;
    Vector3 USER_ScaleRandom_min = new Vector3(1f,1f,1f);
    Vector3 USER_ScaleRandom_max = new Vector3(16f,16f,16f);

    

    [MenuItem("M2007U UwUnity tOwOls/Transform Controller")]
    private static void ShowWindow()
    {
        var window = GetWindow<M2007U_UwUnity_transfOwOrmController>();
        window.titleContent = new GUIContent("TransfOwOrm Controller");
        window.Show();
    }

    private void OnEnable() 
    {
        
    }

    private void OnGUI()
    {
        
        USER_ParentObject = EditorGUILayout.ObjectField("Parent Object",USER_ParentObject,typeof(GameObject),true) as GameObject;

        SkwollingVektor = EditorGUILayout.BeginScrollView(SkwollingVektor);




        EOwO.MakeSpace(1);
        Foldout_Scatter_Box = EditorGUILayout.Foldout(Foldout_Scatter_Box, "Scatter : Box", true);
        if(Foldout_Scatter_Box)
        {
            USER_Scatter_Box_Range_Min = EditorGUILayout.Vector3Field("Range : Min",USER_Scatter_Box_Range_Min);
            USER_Scatter_Box_Range_Max = EditorGUILayout.Vector3Field("Range : Max",USER_Scatter_Box_Range_Max);
            
            if(GUILayout.Button("Scatter Selected Objects") && Selection.gameObjects.Length > 0)
            {
                
                GameObject[] ListGO = Selection.gameObjects;

                for(int i = 0 ; i < ListGO.Length ; i++)
                {

                    float Vek3X = UnityEngine.Random.Range(USER_Scatter_Box_Range_Min.x,USER_Scatter_Box_Range_Max.x);
                    float Vek3Y = UnityEngine.Random.Range(USER_Scatter_Box_Range_Min.y,USER_Scatter_Box_Range_Max.y);
                    float Vek3Z = UnityEngine.Random.Range(USER_Scatter_Box_Range_Min.z,USER_Scatter_Box_Range_Max.z);

                    ListGO[i].transform.localPosition = new Vector3(Vek3X,Vek3Y,Vek3Z);
                }
                
            }
        }
        

        EOwO.MakeSpace(1);
        Foldout_Scatter_Ring = EditorGUILayout.Foldout(Foldout_Scatter_Ring, "Scatter : Ring", true);
        if(Foldout_Scatter_Ring)
        {
            USER_Scatter_Ring_Radius_Min = EditorGUILayout.FloatField("Radius : Min",USER_Scatter_Ring_Radius_Min);
            USER_Scatter_Ring_Radius_Max = EditorGUILayout.FloatField("Radius : Min",USER_Scatter_Ring_Radius_Max);
            USER_Scatter_Ring_Axis = EditorGUILayout.TextField("Axis",USER_Scatter_Ring_Axis);

            if (GUILayout.Button("Scatter Selected Objects") && Selection.gameObjects.Length > 0)
            {
                GameObject[] ListGO = Selection.gameObjects;

                if(USER_Scatter_Ring_Axis == "x")
                {
                    for(int i = 0 ; i < ListGO.Length ; i++)
                    {
                        Vector2 VekR = new Vector2(UnityEngine.Random.Range(-1f,1f),UnityEngine.Random.Range(-1f,1f));
                        VekR.Normalize();
                        VekR = VekR * UnityEngine.Random.Range(USER_Scatter_Ring_Radius_Min,USER_Scatter_Ring_Radius_Max);

                        ListGO[i].transform.localPosition = new Vector3(0,VekR.x,VekR.y);
                    }
                }
                else if(USER_Scatter_Ring_Axis == "y")
                {
                    for(int i = 0 ; i < ListGO.Length ; i++)
                    {
                        Vector2 VekR = new Vector2(UnityEngine.Random.Range(-1f,1f),UnityEngine.Random.Range(-1f,1f));
                        VekR.Normalize();
                        VekR = VekR * UnityEngine.Random.Range(USER_Scatter_Ring_Radius_Min,USER_Scatter_Ring_Radius_Max);

                        ListGO[i].transform.localPosition = new Vector3(VekR.x,0,VekR.y);
                    }
                }
                else if(USER_Scatter_Ring_Axis == "z")
                {
                    for(int i = 0 ; i < ListGO.Length ; i++)
                    {
                        Vector2 VekR = new Vector2(UnityEngine.Random.Range(-1f,1f),UnityEngine.Random.Range(-1f,1f));
                        VekR.Normalize();
                        VekR = VekR * UnityEngine.Random.Range(USER_Scatter_Ring_Radius_Min,USER_Scatter_Ring_Radius_Max);

                        ListGO[i].transform.localPosition = new Vector3(VekR.x,VekR.y,0);
                    }
                }
                else
                {
                    Debug.LogError("M2007U UwUnity tOwOls : Axis must be either \"x\" or \"y\" or \"z\"");
                }
            }
        }
        
        
        EOwO.MakeSpace(1);
        Foldout_Scatter_Sphere = EditorGUILayout.Foldout(Foldout_Scatter_Sphere, "Scatter : Sphere", true);
        if(Foldout_Scatter_Sphere)
        {
            USER_Scatter_Sphere_Radius_Min = EditorGUILayout.FloatField("Radius : Min",USER_Scatter_Sphere_Radius_Min);
            USER_Scatter_Sphere_Radius_Max = EditorGUILayout.FloatField("Radius : Max",USER_Scatter_Sphere_Radius_Max);

            if (GUILayout.Button("Scatter Selected Objects") && Selection.gameObjects.Length > 0)
            {
                
                GameObject[] ListGO = Selection.gameObjects;

                for(int i = 0 ; i < ListGO.Length ; i++)
                {
                    Vector3 VekR = new Vector3(UnityEngine.Random.Range(-1f,1f),UnityEngine.Random.Range(-1f,1f),UnityEngine.Random.Range(-1f,1f));
                    VekR.Normalize();
                    VekR = VekR * UnityEngine.Random.Range(USER_Scatter_Sphere_Radius_Min,USER_Scatter_Sphere_Radius_Max);

                    ListGO[i].transform.localPosition = VekR;
                }
                
            }
        }


        EOwO.MakeSpace(1);
        Foldout_GridSnap = EditorGUILayout.Foldout(Foldout_GridSnap, "Grid Snap", true);
        if(Foldout_GridSnap)
        {
            USER_GridSnap_UseX = EditorGUILayout.Toggle("along X",USER_GridSnap_UseX);
            USER_GridSnap_UseY = EditorGUILayout.Toggle("along Y",USER_GridSnap_UseY);
            USER_GridSnap_UseZ = EditorGUILayout.Toggle("along Z",USER_GridSnap_UseZ);
            USER_GridSnap_Grid = EditorGUILayout.Vector3Field("Scatter Grid Size",USER_GridSnap_Grid);

            if(GUILayout.Button("Snap Selected Objects") && Selection.gameObjects.Length > 0)
            {
                List<GameObject> ListGO = new List<GameObject> ();
                List<float> ListPosX = new List<float> ();
                List<float> ListPosY = new List<float> ();
                List<float> ListPosZ = new List<float> ();

                for(int i = 0 ; i < Selection.gameObjects.Length ; i++)
                {
                    ListGO.Add(Selection.gameObjects[i]);
                    ListPosX.Add(ListGO[i].transform.localPosition.x);
                    ListPosY.Add(ListGO[i].transform.localPosition.y);
                    ListPosZ.Add(ListGO[i].transform.localPosition.z);
                }

                if (USER_GridSnap_UseX)
                {
                    for(int i = 0 ; i < ListGO.Count ; i++)
                    {
                        ListPosX[i] = FOwO.float_GridSnap(USER_GridSnap_Grid.x,ListPosX[i]);
                    }
                }

                if (USER_GridSnap_UseY)
                {
                    for(int i = 0 ; i < ListGO.Count ; i++)
                    {
                        ListPosY[i] = FOwO.float_GridSnap(USER_GridSnap_Grid.y,ListPosY[i]);
                    }
                }

                if (USER_GridSnap_UseZ)
                {
                    for(int i = 0 ; i < ListGO.Count ; i++)
                    {
                        ListPosZ[i] = FOwO.float_GridSnap(USER_GridSnap_Grid.z,ListPosZ[i]);
                    }
                }

                for(int i = 0 ; i < ListGO.Count ; i++)
                {
                    ListGO[i].transform.localPosition = new Vector3(ListPosX[i],ListPosY[i],ListPosZ[i]);
                } 
            }
        }


        EOwO.MakeSpace(1);
        Foldout_GridArrange = EditorGUILayout.Foldout(Foldout_GridArrange, "Grid Arrange", true);
        if(Foldout_GridArrange)
        {
            USER_GridArrange_AxisOrder = EditorGUILayout.Vector3Field("Axis Order",USER_GridArrange_AxisOrder);
            USER_GridArrange_AxisSize = EditorGUILayout.Vector3IntField("Axis Size",USER_GridArrange_AxisSize);
            USER_GridArrange_Step = EditorGUILayout.Vector3Field("Step",USER_GridArrange_Step);
            
            if(GUILayout.Button("Grid Selected Objects") && Selection.gameObjects.Length > 0)
            {
                /*
                Selection.Objects[i].localPos = Counter * Step;

                increment Counter :
                    look for TargetOrder 0
                        increment that Counter
                        if Counter reached Size
                            set it to 0
                            look for targetOrder 1
                            increment that counter
                            if Counter reached Size
                                set it to 0
                                look for targetOrder 2
                                increment that counter
                */

                if
                ( 
                    USER_GridArrange_AxisOrder.x != USER_GridArrange_AxisOrder.y &&
                    USER_GridArrange_AxisOrder.y != USER_GridArrange_AxisOrder.z &&
                    USER_GridArrange_AxisOrder.x != USER_GridArrange_AxisOrder.z 
                )
                {
                    LOCAL_GridArrange_Counter = new Vector3Int(0,0,0);
                    GameObject[] ListGO = Selection.gameObjects;

                    for(int i = 0 ; i < ListGO.Length ; i++)
                    {
                        ListGO[i].transform.localPosition = new Vector3
                        (
                            LOCAL_GridArrange_Counter.x * USER_GridArrange_Step.x,
                            LOCAL_GridArrange_Counter.y * USER_GridArrange_Step.y,
                            LOCAL_GridArrange_Counter.z * USER_GridArrange_Step.z
                        );

                        //increment AxisCounter0
                        string AxisOrder0 = FOwO.vector3_AxisOrder(USER_GridArrange_AxisOrder,0);
                        bool AxisOrder0Carry = false;
                        if (AxisOrder0 == "x")
                        {
                            LOCAL_GridArrange_Counter.x++;
                            if(LOCAL_GridArrange_Counter.x >= USER_GridArrange_AxisSize.x)
                            {
                                LOCAL_GridArrange_Counter.x = 0;
                                AxisOrder0Carry = true;
                            }
                        }
                        else if (AxisOrder0 == "y")
                        {
                            LOCAL_GridArrange_Counter.y++;
                            if(LOCAL_GridArrange_Counter.y >= USER_GridArrange_AxisSize.y)
                            {
                                LOCAL_GridArrange_Counter.y = 0;
                                AxisOrder0Carry = true;
                            }
                        }
                        else if (AxisOrder0 == "z")
                        {
                            LOCAL_GridArrange_Counter.z++;
                            if(LOCAL_GridArrange_Counter.z >= USER_GridArrange_AxisSize.z)
                            {
                                LOCAL_GridArrange_Counter.z = 0;
                                AxisOrder0Carry = true;
                            }
                        }

                        //increment AxisCounter1 if need to carry
                        string AxisOrder1 = FOwO.vector3_AxisOrder(USER_GridArrange_AxisOrder,1);
                        bool AxisOrder1Carry = false;
                        if (AxisOrder0Carry)
                        {
                            if (AxisOrder1 == "x")
                            {
                                LOCAL_GridArrange_Counter.x++;
                                if(LOCAL_GridArrange_Counter.x >= USER_GridArrange_AxisSize.x)
                                {
                                    LOCAL_GridArrange_Counter.x = 0;
                                    AxisOrder1Carry = true;
                                }
                            }
                            else if (AxisOrder1 == "y")
                            {
                                LOCAL_GridArrange_Counter.y++;
                                if(LOCAL_GridArrange_Counter.y >= USER_GridArrange_AxisSize.y)
                                {
                                    LOCAL_GridArrange_Counter.y = 0;
                                    AxisOrder1Carry = true;
                                }
                            }
                            else if (AxisOrder1 == "z")
                            {
                                LOCAL_GridArrange_Counter.z++;
                                if(LOCAL_GridArrange_Counter.z >= USER_GridArrange_AxisSize.z)
                                {
                                    LOCAL_GridArrange_Counter.z = 0;
                                    AxisOrder1Carry = true;
                                }
                            }
                        }

                        //increment AxisCounter2 if need to carry
                        string AxisOrder2 = FOwO.vector3_AxisOrder(USER_GridArrange_AxisOrder,2);
                        if (AxisOrder1Carry)
                        {
                            if (AxisOrder2 == "x")
                            {
                                LOCAL_GridArrange_Counter.x++;
                            }
                            else if (AxisOrder2 == "y")
                            {
                                LOCAL_GridArrange_Counter.y++;
                            }
                            else if (AxisOrder2 == "z")
                            {
                                LOCAL_GridArrange_Counter.z++;
                            }
                        }

                    }
                }
                else
                {
                    Debug.LogError("M2007U UwUnity tOwOls : the XYZ component for Axis Order must be all different to determine Axis Order");
                }
            }
        }


        EOwO.MakeSpace(1);
        Foldout_PositionScale = EditorGUILayout.Foldout(Foldout_PositionScale, "Local Position Scale", true);
        if(Foldout_PositionScale)
        {
            USER_PositionScale_Scaler = EditorGUILayout.Vector3Field("Scale Factor",USER_PositionScale_Scaler);

            if (GUILayout.Button("Grid Selected Objects") && Selection.gameObjects.Length > 0)
            {
                GameObject[] ListGO = Selection.gameObjects;

                for(int i = 0 ; i < ListGO.Length ; i++)
                {
                    ListGO[i].transform.localPosition = new Vector3
                    (
                        ListGO[i].transform.localPosition.x * USER_PositionScale_Scaler.x,
                        ListGO[i].transform.localPosition.y * USER_PositionScale_Scaler.y,
                        ListGO[i].transform.localPosition.z * USER_PositionScale_Scaler.z
                    );
                }
            }
        }


        EOwO.MakeSpace(1);
        Foldout_Surround = EditorGUILayout.Foldout(Foldout_Surround, "Surround Parent", true);
        if(Foldout_Surround)
        {
            USER_Surround_Radius = EditorGUILayout.FloatField("Radius",USER_Surround_Radius);
            USER_Surround_Phase = EditorGUILayout.FloatField("Phase",USER_Surround_Phase);
            USER_Surround_Step = EditorGUILayout.FloatField("Step",USER_Surround_Step);
            USER_Surround_Axis = EditorGUILayout.TextField("Axis",USER_Surround_Axis);

            if (GUILayout.Button("Surround Parent") && Selection.gameObjects.Length > 0)
            {
                //2 pi rad = 360 deg
                // rad = 360 deg /2 /pi

                //? rad = x deg
                //? * 360 * deg /2 /pi = x deg
                //? * 360 /2 /pi = x
                //? = x /360 *2 *pi

                GameObject[] ListGO = Selection.gameObjects;

                
                if (USER_Surround_Axis == "x")
                {
                    for(int i = 0 ; i < ListGO.Length ; i++)
                    {
                        float Theta = (USER_Surround_Phase + i * USER_Surround_Step) / 360 * 2 * Mathf.PI;

                        ListGO[i].transform.localPosition = new Vector3
                        (
                            0,
                            Mathf.Sin(Theta) * USER_Surround_Radius,
                            Mathf.Cos(Theta) * USER_Surround_Radius
                        );
                    }
                }
                else if (USER_Surround_Axis == "y")
                {
                    for(int i = 0 ; i < ListGO.Length ; i++)
                    {
                        float Theta = (USER_Surround_Phase + i * USER_Surround_Step) / 360 * 2 * Mathf.PI;

                        ListGO[i].transform.localPosition = new Vector3
                        (
                            Mathf.Cos(Theta) * USER_Surround_Radius,
                            0,
                            Mathf.Sin(Theta) * USER_Surround_Radius
                        );
                    }
                }
                else if (USER_Surround_Axis == "z")
                {
                    for(int i = 0 ; i < ListGO.Length ; i++)
                    {
                        float Theta = (USER_Surround_Phase + i * USER_Surround_Step) / 360 * 2 * Mathf.PI;

                        ListGO[i].transform.localPosition = new Vector3
                        (
                            Mathf.Cos(Theta) * USER_Surround_Radius,
                            Mathf.Sin(Theta) * USER_Surround_Radius,
                            0
                        );
                    }
                }
                else
                {
                    Debug.LogError("M2007U UwUnity tOwOls : the Axis must be either \"x\" or \"y\" or \"z\"");
                }
            }
        }

        EOwO.MakeSpace(1);
        Foldout_LerpTransform = EditorGUILayout.Foldout(Foldout_LerpTransform, "Transform Lerp", true);
        if(Foldout_LerpTransform)
        {
            USER_LerpTransform_ObjectA = EditorGUILayout.ObjectField("Pin Object A",USER_LerpTransform_ObjectA,typeof(GameObject),true) as GameObject;
            USER_LerpTransform_ObjectB = EditorGUILayout.ObjectField("Pin Object B",USER_LerpTransform_ObjectB,typeof(GameObject),true) as GameObject;

            if (GUILayout.Button("Position Lerp"))
            {

                if(Selection.gameObjects.Length > 0 && USER_LerpTransform_ObjectA != null && USER_LerpTransform_ObjectB != null)
                {
                    GameObject[] ListGO = Selection.gameObjects;

                    Vector3 LerpPos_Segment = (USER_LerpTransform_ObjectB.transform.position - USER_LerpTransform_ObjectA.transform.position) / (ListGO.Length - 1);

                    for(int i = 0 ; i < ListGO.Length ; i++)
                    {
                        ListGO[i].transform.position = LerpPos_Segment * i + USER_LerpTransform_ObjectA.transform.position;
                    }
                }
                else
                {
                    if(Selection.gameObjects.Length <= 0)
                    {
                        Debug.LogError("M2007U UwUnity tOwOls : A single GameObject or a list of GameObjects must be selected");
                    }

                    if(USER_LerpTransform_ObjectA == null)
                    {
                        Debug.LogError("M2007U UwUnity tOwOls : Lerp Starting Refference Missing");
                    }

                    if(USER_LerpTransform_ObjectB == null)
                    {
                        Debug.LogError("M2007U UwUnity tOwOls : Lerp Ending Refference Missing");
                    }
                }

            }

            if (GUILayout.Button("Rotation (Quaternion) Lerp"))
            {
                if(Selection.gameObjects.Length > 0 && USER_LerpTransform_ObjectA != null && USER_LerpTransform_ObjectB != null)
                {
                    GameObject[] ListGO = Selection.gameObjects;

                    Quaternion LerpRot_Segment = FOwO.Quaternion_Div(FOwO.Quaternion_Sub(USER_LerpTransform_ObjectB.transform.rotation,USER_LerpTransform_ObjectA.transform.rotation),(ListGO.Length - 1));

                    for(int i = 0 ; i < ListGO.Length ; i++)
                    {
                        ListGO[i].transform.rotation = (FOwO.Quaternion_Add(FOwO.Quaternion_Mul(LerpRot_Segment,i),USER_LerpTransform_ObjectA.transform.rotation)).normalized;
                    }
                }
                else
                {
                    if(Selection.gameObjects.Length <= 0)
                    {
                        Debug.LogError("M2007U UwUnity tOwOls : A single GameObject or a list of GameObjects must be selected");
                    }

                    if(USER_LerpTransform_ObjectA == null)
                    {
                        Debug.LogError("M2007U UwUnity tOwOls : Lerp Starting Refference Missing");
                    }

                    if(USER_LerpTransform_ObjectB == null)
                    {
                        Debug.LogError("M2007U UwUnity tOwOls : Lerp Ending Refference Missing");
                    }
                }
            }

            if (GUILayout.Button("Rotation (Euler) Lerp"))
            {
                if(Selection.gameObjects.Length > 0 && USER_LerpTransform_ObjectA != null && USER_LerpTransform_ObjectB != null)
                {
                    GameObject[] ListGO = Selection.gameObjects;

                    Vector3 LerpRot_Segment = (USER_LerpTransform_ObjectB.transform.localEulerAngles - USER_LerpTransform_ObjectA.transform.localEulerAngles) / (ListGO.Length - 1);

                    for(int i = 0 ; i < ListGO.Length ; i++)
                    {
                        ListGO[i].transform.localEulerAngles = LerpRot_Segment * i + USER_LerpTransform_ObjectA.transform.localEulerAngles;
                    }
                }
                else
                {
                    if(Selection.gameObjects.Length <= 0)
                    {
                        Debug.LogError("M2007U UwUnity tOwOls : A single GameObject or a list of GameObjects must be selected");
                    }

                    if(USER_LerpTransform_ObjectA == null)
                    {
                        Debug.LogError("M2007U UwUnity tOwOls : Lerp Starting Refference Missing");
                    }

                    if(USER_LerpTransform_ObjectB == null)
                    {
                        Debug.LogError("M2007U UwUnity tOwOls : Lerp Ending Refference Missing");
                    }
                }
            }

            if (GUILayout.Button("Local Scale Lerp"))
            {
                if(Selection.gameObjects.Length > 0 && USER_LerpTransform_ObjectA != null && USER_LerpTransform_ObjectB != null)
                {
                    GameObject[] ListGO = Selection.gameObjects;

                    Vector3 LerpScale_Segment = (USER_LerpTransform_ObjectB.transform.localScale - USER_LerpTransform_ObjectA.transform.localScale) / (ListGO.Length - 1);

                    for(int i = 0; i < ListGO.Length ; i++)
                    {
                        ListGO[i].transform.localScale = LerpScale_Segment * i + USER_LerpTransform_ObjectA.transform.localScale;
                    }
                }
                else
                {
                    if(Selection.gameObjects.Length <= 0)
                    {
                        Debug.LogError("M2007U UwUnity tOwOls : A single GameObject or a list of GameObjects must be selected");
                    }

                    if(USER_LerpTransform_ObjectA == null)
                    {
                        Debug.LogError("M2007U UwUnity tOwOls : Lerp Starting Refference Missing");
                    }

                    if(USER_LerpTransform_ObjectB == null)
                    {
                        Debug.LogError("M2007U UwUnity tOwOls : Lerp Ending Refference Missing");
                    }
                }
            }

        }

        EOwO.MakeSpace(1);
        Foldout_ArrayIterate = EditorGUILayout.Foldout(Foldout_ArrayIterate, "Array Iterate", true);
        if(Foldout_ArrayIterate)
        {
            USER_ArrayIterate_Object = EditorGUILayout.ObjectField("Starting Object",USER_ArrayIterate_Object,typeof(GameObject),true) as GameObject;
            USER_ArrayIterate_Step_Pos = EditorGUILayout.Vector3Field("Step : Position",USER_ArrayIterate_Step_Pos);
            USER_ArrayIterate_Step_Rot = EditorGUILayout.Vector3Field("Step : Rotation",USER_ArrayIterate_Step_Rot);
            USER_ArrayIterate_Step_Scl = EditorGUILayout.Vector3Field("Step : Local Scale",USER_ArrayIterate_Step_Scl);

            if (GUILayout.Button("Iterate"))
            {
                if(Selection.gameObjects.Length > 0 && USER_ArrayIterate_Object != null)
                {
                    GameObject[] ListGO = Selection.gameObjects;

                    for(int i = 0 ; i < ListGO.Length ; i++)
                    {
                        ListGO[i].transform.position = USER_ArrayIterate_Object.transform.position + USER_ArrayIterate_Step_Pos * i;
                        ListGO[i].transform.eulerAngles = USER_ArrayIterate_Object.transform.eulerAngles + USER_ArrayIterate_Step_Rot * i;
                        ListGO[i].transform.localScale = USER_ArrayIterate_Object.transform.localScale + USER_ArrayIterate_Step_Scl * i;
                    }
                }
                else
                {
                    if(Selection.gameObjects.Length <= 0)
                    {
                        Debug.LogError("M2007U UwUnity tOwOls : A single GameObject or a list of GameObjects must be selected");
                    }

                    if(USER_ArrayIterate_Object == null)
                    {
                        Debug.LogError("M2007U UwUnity tOwOls : Starting Refference Missing");
                    }
                }
            }

            

        }

        EOwO.MakeSpace(1);
        Foldout_ScaleRandom = EditorGUILayout.Foldout(Foldout_ScaleRandom, "Scale Random", true);
        if(Foldout_ScaleRandom)
        {
            USER_ScaleRandom_min = EditorGUILayout.Vector3Field("local Scale : min",USER_ScaleRandom_min);
            USER_ScaleRandom_max = EditorGUILayout.Vector3Field("local Scale : max",USER_ScaleRandom_max);

            if (GUILayout.Button("Random Scale"))
            {
                GameObject[] ListGO = Selection.gameObjects;

                for(int i = 0 ; i < ListGO.Length ; i++)
                {
                    ListGO[i].transform.localScale = new Vector3
                    (
                        UnityEngine.Random.Range(USER_ScaleRandom_min.x, USER_ScaleRandom_max.x),
                        UnityEngine.Random.Range(USER_ScaleRandom_min.y, USER_ScaleRandom_max.y),
                        UnityEngine.Random.Range(USER_ScaleRandom_min.z, USER_ScaleRandom_max.z)
                    );
                }
            }
        }

        EditorGUILayout.EndScrollView();
    }
}

/*
    Roadmap :
*/
