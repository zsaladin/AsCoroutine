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
                .YieldWWW(new WWW("http://ip.jsontest.com/?mime=5")).Action(www =>
                {
                    Debug.Log(www.text);
                }).Start();
        }

        private void Sample2()
        {
            WWW www = null;
            this.AsCoroutine()
                .Action(() => www = new WWW("https://api.github.com/users/zsaladin/repos"))
                .Action(() => Debug.Log(www.progress))
                .Repeat(() => www.isDone == false)
                .Action(() =>
                {
                    Debug.Log(www.progress);
                    Debug.Log(www.text);
                })
                .Start();
        }

        private void OnGUI()
        {
            if (GUI.Button(GetRect(1, 2), "Sample1"))
                Sample1();

            if (GUI.Button(GetRect(2, 2), "Sample2"))
                Sample2();
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