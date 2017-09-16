using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsCoroutine.Example
{
    public class ExampleBasic : MonoBehaviour
    {
        private void Example1()
        {
            this.AsCoroutine()
                .YieldWaitForSeconds(1f).Action(() => Debug.Log("WaitForSeconds"))
                .YieldWaitForSecondsRealtime(1f).Action(() => Debug.Log("WaitForSecondsRealtime"))
                .YieldWaitForNextFrame().Action(() => Debug.Log("WaitForNextFrame"))
                .YieldWaitForEndOfFrame().Action(() => Debug.Log("WaitForEndOfFrame"))
                .YieldWaitForFixedUpdate().Action(() => Debug.Log("WaitForFixedUpdate"))
                .Start(this);
        }

        private void Example2()
        {
            this.AsCoroutine()
                .Action(() => Debug.Log("RepeatAction"))
                .Repeat(3)
                .YieldAction(() =>
                {
                    Debug.Log("RepeatYieldAction");
                    return new WaitForSecondsRealtime(1f);
                })
                .Repeat(5)
                .Start(this);
        }

        private void Example3()
        {
            Cooperator cooperator = this.AsCoroutine().Action(() => Debug.Log("Example3")).Repeat(() => true).Start(this);
            this.AsCoroutine()
                .YieldWaitForSeconds(2f)
                .Action(() => cooperator.Stop())
                .Action(() => Debug.Log("Stop"))
                .Start(this);
        }

        private void Example4()
        {
            Cooperator cooperator = this.AsCoroutine().Action(() => Debug.Log("Example4")).Repeat(() => true);
            StartCoroutine(cooperator);

            this.AsCoroutine()
                .YieldWaitForSeconds(2f)
                .Action(() => StopCoroutine(cooperator))
                .Action(() => Debug.Log("Stop"))
                .Start(this);
        }

        private void OnGUI()
        {
            if (GUI.Button(GetRect(1, 4), "Example1"))
                Example1();

            if (GUI.Button(GetRect(2, 4), "Example2"))
                Example2();

            if (GUI.Button(GetRect(3, 4), "Example3"))
                Example3();

            if (GUI.Button(GetRect(4, 4), "Example4"))
                Example4();
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