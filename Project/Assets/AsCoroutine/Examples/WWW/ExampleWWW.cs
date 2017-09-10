using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsCoroutine.Example
{
    public class ExampleWWW : MonoBehaviour
    {
        private void Sample1()
        {
            this.AsCoroutine()
                .YieldWWW(new WWW("http://google.com"))
                .YieldAction(www =>
                {
                    Debug.Log(www.text);
                }).Start();
        }

        private void Sample2()
        {
            this.AsCoroutine()
                .YieldWWW(new WWW("http://google.com"), www => 
                {
                    Debug.Log(www.text);
                }).Start();
        }

        private void Sample3()
        {
            this.AsCoroutine()
                .YieldWWW(new WWW("http://google.com"), www =>
                {
                    Debug.Log(www.text);
                    return new WWW("http://unity3d.com");
                })
                .YieldAction(www =>
                {
                    Debug.Log(www.text);
                }).Start();
        }

        private void Sample4()
        {
            this.AsCoroutine()
                .YieldWWW(new WWW("http://google.com"), www =>
                {
                    Debug.Log(www.text);
                    return new WWW("http://unity3d.com");
                })
                .YieldFunc(www =>
                {
                    Debug.Log(www.text);
                    return new WaitForSeconds(3f);
                })
                .YieldNextFrame(() =>
                {
                    Debug.Log("Complete");
                })
                .Start();
        }

        private void OnGUI()
        {
            if (GUI.Button(GetRect(1, 4), "Sample1"))
                Sample1();

            if (GUI.Button(GetRect(2, 4), "Sample2"))
                Sample2();

            if (GUI.Button(GetRect(3, 4), "Sample3"))
                Sample3();

            if (GUI.Button(GetRect(4, 4), "Sample4"))
                Sample4();
        }

        private Rect GetRect(int order, int totalOrder)
        {
            float width = Screen.width * 0.3f;
            float x = Screen.width * 0.5f - width * 0.5f;

            float height = Screen.height / totalOrder;
            float y = (order - 1) * height;

            return new Rect(x, y, width, height);
        }
    }
}