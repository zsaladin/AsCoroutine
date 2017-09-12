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

			this.AsCoroutine().YieldCoroutine(Example2Coroutine());
        }

		private IEnumerator Example2Coroutine()
		{
			if (Random.value < 0.5f)
				yield return new WaitForSeconds(1f);
			else
				yield return new WaitForSeconds(2f);

			Debug.Log("Example2Coroutine");
		}

        private void OnGUI()
        {
            if (GUI.Button(GetRect(1, 2), "Example1"))
                Example1();

            if (GUI.Button(GetRect(2, 2), "Example2"))
                Example2();
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