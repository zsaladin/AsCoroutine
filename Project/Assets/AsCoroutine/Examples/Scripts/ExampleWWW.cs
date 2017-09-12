using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsCoroutine.Example
{
    public class ExampleWWW : MonoBehaviour
    {
        private void Example1()
        {
            this.AsCoroutine()
                .YieldWWW(new WWW("http://ip.jsontest.com/?mime=5")).Action(www =>
                {
                    Debug.Log(www.text);
                }).Start(this);
        }

        private void Example2()
        {
            this.AsCoroutine()
                .YieldWaitForSeconds(1f).Action(() => Debug.Log("Example2 WaitForSeconds"))
                .YieldAction(() =>
                {
                    WWW www = new WWW("https://api.github.com/users/zsaladin/repos");
                    return www;
                })
                .Action(www => Debug.Log(www.text))
                .Start(this);
        }

        private void Example3()
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
                .Start(this);
        }

        private void OnGUI()
        {
            if (GUI.Button(GetRect(1, 3), "Example1"))
                Example1();

            if (GUI.Button(GetRect(2, 3), "Example2"))
                Example2();

            if (GUI.Button(GetRect(3, 3), "Example3"))
                Example3();
        }

        private Rect GetRect(int order, int totalOrder)
        {
            float width = Screen.width;
            float x = 0f;

            float height = Screen.height / totalOrder;
            float y = (order - 1) * height;

            return new Rect(x, y, width, height);
        }
    }
}