using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsCoroutine.Example
{
    public class ExampleBasic : MonoBehaviour
    {
        private Cooperator _cooperator;
        private void Sample1()
        {
            if (_cooperator == null)
                _cooperator = this.AsCoroutine()
                    .YieldWaitForSecondsRealtime(1f).Action(() => Debug.Log("WaitForSeconds"))
                    .YieldWaitForSecondsRealtime(1f).Action(() => Debug.Log("WaitForSecondsRealtime"))
                    .YieldNextFrame().Action(() => Debug.Log("NextFrame"))
                    .YieldWaitEndOfFrame().Action(() => Debug.Log("WaitEndOfFrame"))
                    .Start(this);
            else
                _cooperator.New().Start(this);
        }

        private void Sample2()
        {
            this.AsCoroutine()
                .Action(() =>
                {
                    Debug.Log("RepeatAction");
                })
                .Repeat(3)
                .YieldAction(() =>
                {
                    Debug.Log("RepeatYieldAction");
                    return new WaitForSecondsRealtime(1f);
                })
                .Repeat(5)
                .Start(this);
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