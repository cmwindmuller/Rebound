using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace C33
{
    [System.Serializable]
    public struct Range
    {
        public float min, max;

        public Range(float min, float max)
        {
            this.min = min;
            this.max = max;
        }
    };

    [System.Serializable]
    public struct IVector
    {
        public int x, y, z;

        public float sqrMagnitude
        {
            get
            {
                return x * x + y * y + z * z;
            }
        }
    };

    public class Maths
    {
        public static float Mod(float x, float y)
        {
            y = Mathf.Clamp01(y);
            return Mathf.Round(x / y) * y;
        }

        public static float Pow(float x, float y = 2)
        {
            return Mathf.Pow(x, y);
        }

        public static float SPow(float x, float y)
        {
            return Mathf.Pow(Mathf.Abs(x), y) * Mathf.Sign(x);
        }

        public static Vector3 SPow(Vector3 v, float y)
        {
            v.x = SPow(v.x, y);
            v.y = SPow(v.y, y);
            v.z = SPow(v.z, y);

            return v;
        }

        public static Vector3 Pow(Vector3 v, float y)
        {
            v.x = Mathf.Pow(v.x, y);
            v.y = Mathf.Pow(v.y, y);
            v.z = Mathf.Pow(v.z, y);

            return v;
        }

        public static Vector3 Mult(Vector3 a, Vector3 b)
        {
            Vector3 c = new Vector3();
            c.x = a.x * b.x;
            c.y = a.y * b.y;
            c.z = a.z * b.z;
            return c;
        }

        public static Vector3 Div(Vector3 a, Vector3 b)
        {
            b.x = b.x == 0 ? 1 : b.x;
            b.y = b.y == 0 ? 1 : b.y;
            b.z = b.z == 0 ? 1 : b.z;
            Vector3 c = new Vector3();
            c.x = a.x / b.x;
            c.y = a.y / b.y;
            c.z = a.z / b.z;
            return c;
        }

        public static Vector3 Linear(Vector3 v, float a, float b)
        {
            v.x = v.x * a + b;
            v.y = v.y * a + b;
            v.z = v.z * a + b;

            return v;
        }

        public static Vector3 ILinear(Vector3 v, float a, float b)
        {
            v.x = (v.x + b) * a;
            v.y = (v.y + b) * a;
            v.z = (v.z + b) * a;

            return v;
        }
    }
}