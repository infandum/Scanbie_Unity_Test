using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Assets.Scripts.Helpers
{
    [Serializable]
    public class ObjectColor
    {
        public float R = 0;
        public float G = 0;
        public float B = 0;
        public float A = 255;

        public Color ToColor()
        {
            return new Color(R,G,B,A);
        }
        public void SetColor(Color color)
        {
            this.R = color.r;
            this.G = color.g;
            this.B = color.b;
            this.A = color.a;
        }
    }
    [Serializable]
    public class ObjectEditableData
    {
        public string GUID = "";
        public string Name = "Editable Object";
        public string InfoFile = "Lorem";

        //public Bounds MeshBounds;

        public ObjectColor MainColor;
        public float Metallic;
        public float Smoothness;
        public int ShadowCasting = (int)ShadowCastingMode.On;
        public bool ReceiveShadow = true;

        public string ToJson()
        {
            var json = JsonUtility.ToJson(this);
            return json;
        }

        public ObjectEditableData FromJson(string json)
        {
            var data = JsonUtility.FromJson<ObjectEditableData>(json);
            return data;
        }
    }


}
