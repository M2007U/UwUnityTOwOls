using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace M2007U_UwUnity_OwOFunctions
{
    public class M2007U_UwUnity_FOwObject
    {
        
        public float float_GridSnap(float GridConstant, float GivenFloat)
        {
            /*
            p
            |--m--|
            |----------------|
            nk              (n+1)k

            need to know what is n

            n = (p / k) as int
            m = p % k

            if m < k/2
            then p snap to nk
            else let p snap to (n+1)k
            */
            float Answer;

            if (GridConstant != 0)
            {
                int GridQuantity = (int)(GivenFloat / GridConstant);
                float GridRemain = GivenFloat % GridConstant;
                

                if(GridRemain < GridConstant/2)
                {
                    Answer = GridQuantity * GridConstant;
                }
                else
                {
                    Answer = (GridQuantity + 1) * GridConstant;
                }
            }
            else
            {
                Answer = GivenFloat;
            }

            return Answer;
        }

        public string vector3_AxisOrder (Vector3 Vek, int TargetOrder)
        {
            //figure it out which axis is the user asking for
            //eg : if Vek is (12,8,4)
            // if TargetOrder is 0, give the smallest one : return "z"
            // if TargetOrder is 1, give the middle one : return "y"
            // if targetOrder is 2, give the largest one : return "x"
            // if any 2 axis is the same, or Target Order is out of bounds, then return "n"

            string Answer = "";

            if (Vek.x == Vek.y || Vek.y == Vek.z || Vek.z == Vek.x || TargetOrder < 0 || TargetOrder > 2)
            {
                Answer = "n";
            }
            else
            {
                if(TargetOrder == 0)
                {
                    //look for min
                    if      (Vek.x < Vek.y && Vek.x < Vek.z){Answer = "x";}
                    else if (Vek.y < Vek.x && Vek.y < Vek.z){Answer = "y";}
                    else if (Vek.z < Vek.x && Vek.z < Vek.y){Answer = "z";}
                }
                else if (TargetOrder == 1)
                {
                    //look for mid
                    if      ((Vek.y < Vek.x && Vek.x < Vek.z) || (Vek.z < Vek.x && Vek.x < Vek.y) ){Answer = "x";}
                    else if ((Vek.x < Vek.y && Vek.y < Vek.z) || (Vek.z < Vek.y && Vek.y < Vek.x) ){Answer = "y";}
                    else if ((Vek.x < Vek.z && Vek.z < Vek.y) || (Vek.y < Vek.z && Vek.z < Vek.x) ){Answer = "z";}
                }
                else if (TargetOrder == 2)
                {
                    //look for max
                    if      (Vek.y < Vek.x && Vek.z < Vek.x){Answer = "x";}
                    else if (Vek.x < Vek.y && Vek.z < Vek.y){Answer = "y";}
                    else if (Vek.x < Vek.z && Vek.y < Vek.z){Answer = "z";}
                }
            }

            return Answer;
        }

        public Quaternion Quaternion_Add(Quaternion QuaA, Quaternion QuaB)
        {
            return new Quaternion( QuaA.x + QuaB.x , QuaA.y + QuaB.y , QuaA.z + QuaB.z , QuaA.w + QuaB.w);
        }

        public Quaternion Quaternion_Sub(Quaternion QuaA, Quaternion QuaB)
        {
            return new Quaternion( QuaA.x - QuaB.x , QuaA.y - QuaB.y , QuaA.z - QuaB.z , QuaA.w - QuaB.w);
        }

        public Quaternion Quaternion_Dot(Quaternion QuaA, Quaternion QuaB)
        {
            return new Quaternion( QuaA.x * QuaB.x , QuaA.y * QuaB.y , QuaA.z * QuaB.z , QuaA.w * QuaB.w);
        }

        public Quaternion Quaternion_Mul(Quaternion QuaA, float f)
        {
            return new Quaternion( QuaA.x * f , QuaA.y * f , QuaA.z * f , QuaA.w * f);
        }

        public Quaternion Quaternion_Div(Quaternion QuaA, float d)
        {
            return new Quaternion( QuaA.x / d , QuaA.y / d , QuaA.z / d , QuaA.w / d);
        }
    }

    public class M2007U_UwUnity_EOwObject
    {
        public void MakeSpace(int size)
        {
            for(int i = 0 ; i < size ; i++)
            {
                EditorGUILayout.LabelField(" ");
            }
        }
    }
}


