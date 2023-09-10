using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public class Transformation : MonoBehaviour
{
    public static Vector3[] Vector2ToVector3(Vector2[] FirstArray)
    {
        Vector3[] main = new Vector3[0];
        foreach (Vector4 Selected in FirstArray)
        {
            Array.Resize(ref main, main.Length + 1);
            main[main.Length - 1] = new Vector3(Selected.x, Selected.y, 0);
        }
        return main;
    }
    public static Texture2D RandomizedTexture(Color MidColor, Vector2 Resolution, float Seed, float Size)
    {
        MidColor = new Color(MidColor.r, MidColor.g, MidColor.b, 1);
        Texture2D Main = new Texture2D((int)Resolution.x, (int)Resolution.y);
        float minimum = 0;
        if (Resolution.x <= Resolution.y) minimum = Resolution.x;
        else minimum = Resolution.y;
        for (int x = 0; x < (int)Resolution.x; x++)
        {
            for (int y = 0; y < (int)Resolution.y; y++)
            {
                Main.SetPixel(x, y, new Color(Mathf.PerlinNoise(x / minimum * Size + Seed, y / minimum * Size + Seed), Mathf.PerlinNoise(x / minimum * Size + Seed, y / minimum * Size + Seed), Mathf.PerlinNoise(x / minimum * Size + Seed, y / minimum * Size + Seed)) / 2 + MidColor);
            }
        }
        Main.Apply();
        return Main;
    }
    public static Sprite TextureToSprite(Texture2D texture) => Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 50f, 0, SpriteMeshType.FullRect);
    public static int HalfRandomized(int Border)
    {
        float Number = UnityEngine.Random.Range(0f, 1f);
        int Selected = 0;
        for (int x = Border; x > -1; x--)
        {
            Selected = x;
            if (Number > Mathf.Sqrt((float)x / Border))
            {
                break;
            }
        }
        return Selected;
    }
    public static bool RandomBool()
    {
        return new bool[] { true, false }[UnityEngine.Random.Range(0, 2)];
    }
    public static bool IsIntInArray(int[] Array, int Int)
    {
        bool main = false;
        foreach (int Selected in Array)
        {
            if (Selected == Int) main = true;
        }
        return main;
    }
    public static Transform Closest(Vector3 MyPosition, Transform[] OtherPositions)
    {
        Transform Main = null;
        float Dist = 500;
        foreach (Transform Selected in OtherPositions)
        {
            if (Vector3.Distance(MyPosition, Selected.position) < Dist)
            {
                Dist = Vector3.Distance(MyPosition, Selected.transform.position);
                Main = Selected;
            }
        }
        return Main;
    }
    public static Transform Closest(Vector3 MyPosition, GameObject[] OtherPositions)
    {
        Transform Main = null;
        float Dist = 500;
        foreach (GameObject Selected in OtherPositions)
        {
            if (Vector3.Distance(MyPosition, Selected.transform.position) < Dist)
            {
                Dist = Vector3.Distance(MyPosition, Selected.transform.position);
                Main = Selected.transform;
            }
        }
        return Main;
    }
    public static bool CheckSprite(Sprite[] AllSprites, Sprite SelectedSprite)
    {
        bool main = false;
        foreach (Sprite Selected in AllSprites)
        {
            if (Selected.texture == SelectedSprite.texture) main = true;
        }
        return main;
    }
    public static int FindSprite(Sprite[] AllSprites, Sprite SelectedSprite)
    {
        int main = -1;
        for (int i = 0; i < AllSprites.Length; i++)
        {
            if (AllSprites[i].texture == SelectedSprite.texture) main = i;
        }
        return main;
    }
    public static Transform FindX(Transform[] transforms, float xPosition)
    {
        Transform main = null;
        foreach (Transform Selected in transforms)
        {
            if (Selected.localScale.x == xPosition) main = Selected;
        }
        return main;
    }
    public static Transform[] MotherOfFucker(Transform Father)
    {
        Transform[] Main = new Transform[0];
        for (int i = 0; i < Father.childCount; i++)
        {
            Array.Resize(ref Main, Main.Length + 1);
            Main[Main.Length - 1] = Father.GetChild(i);
        }
        return Main;
    }
    public static TextAsset FindByName(TextAsset[] assets,string Name)
    {
        foreach(TextAsset Nowly in assets)
        {
            if (Nowly.name == Name)
            {
                return Nowly;
            }
        }
        return null;
    }
    public static int FindIndexByName(TextAsset[] assets, string Name)
    {
        for (int i = 0; i < assets.Length; i++)
        {
            if (assets[i].name == Name)
            {
                return i;
            }
        }
        return -1;
    }
    public static int Sum(int[] Array)
    {
        int Main = 0;
        foreach(int Nowly in Array)
        {
            Main += Nowly;
        }
        return Main;
    }
    public static float StringToFloat(string RawString)
    {
        Debug.Log(RawString);
        if (!int.TryParse(RawString,out int result)) return (float)int.Parse(RawString.Split('.')[0]) + Mathf.Sign(int.Parse(RawString.Split('.')[0])) * ((float)int.Parse(RawString.Split('.')[1]) / Mathf.Pow(10,RawString.Split('.')[1].Length));
        else return result;
    }


    public static bool RoomActivity()
    {
        return GameObject.FindGameObjectWithTag("Clef").transform.localPosition.x > 0;
    }



    public static bool FloatToBoolian(float Value)
    {
        return Mathf.Sign(Value) >= 1;
    }



    public static TextMeshProUGUI Content()
    {
        return GameObject.FindGameObjectWithTag("Cont").GetComponent<TextMeshProUGUI>();
    }



    public static float MinColor(Color Value)
    {
        if(Value.r<Value.g && Value.r < Value.b)
        {
            return Value.r;
        }
        else if (Value.g < Value.a && Value.g < Value.b)
        {
            return Value.g;
        }
        else
        {
            return Value.b;
        }
    }
    public static float MaxColor(Color Value)
    {
        if (Value.r > Value.g && Value.r > Value.b)
        {
            return Value.r;
        }
        else if (Value.g > Value.a && Value.g > Value.b)
        {
            return Value.g;
        }
        else
        {
            return Value.b;
        }
    }
}