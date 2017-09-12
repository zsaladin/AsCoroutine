using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsCoroutine.Example
{
    public class ExampleComparison : MonoBehaviour
    {
        private void Example1()
        {
            StartCoroutine(Example1Coroutine());

            this.AsCoroutine().YieldWaitForSeconds(1f).Action(() => Debug.Log("Example1")).Start(this);
            this.AsCoroutine().YieldCoroutine(Example1Coroutine()).Start(this);
        }

        private IEnumerator Example1Coroutine()
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("Example1Coroutine");
        }

        private void Example2()
        {
            StartCoroutine(Example2Coroutine());

            this.AsCoroutine()
                .YieldAction(() => Random.value < 0.5f ? new WaitForSeconds(1f) : new WaitForSeconds(2f))
                .Action(() => Debug.Log("Example2"))
                .Start(this);

            this.AsCoroutine().YieldCoroutine(Example2Coroutine()).Start(this);
        }

        private IEnumerator Example2Coroutine()
        {
            yield return Random.value < 0.5f ? new WaitForSeconds(1f) : new WaitForSeconds(2f);
            Debug.Log("Example2Coroutine");
        }

        private void Example3()
        {
            StartCoroutine(Example3Coroutine());

            this.AsCoroutine()
                .YieldAction<object>(() =>
                {
                    if (Random.value < 0.5f)
                        return new WaitForSeconds(1f);
                    else
                        return new WaitForSecondsRealtime(2f);
                })
                .Action(() => Debug.Log("Example3"))
                .Start(this);

            this.AsCoroutine().YieldCoroutine(Example3Coroutine()).Start(this);
        }

        private IEnumerator Example3Coroutine()
        {
            if (Random.value < 0.5f)
                yield return new WaitForSeconds(1f);
            else
                yield return new WaitForSecondsRealtime(2f);

            Debug.Log("Example3Coroutine");
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