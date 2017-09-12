using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsCoroutine.Example
{
	public class ExampleCombine : MonoBehaviour
	{
		private void Sample1()
		{
            Cooperator cooperator1 = this.AsCoroutine().YieldWaitForSeconds(1f).Action(() => Debug.Log("cooperator1"));
            Cooperator cooperator2 = this.AsCoroutine().YieldWaitForSecondsRealtime(1f).Action(() => Debug.Log("cooperator2"));

            cooperator1.YieldCoroutine(cooperator2).Start(this);
		}

		private void Sample2()
		{
			Cooperator cooperator1 = this.AsCoroutine().YieldWaitForSeconds(1f).Action(() => Debug.Log("cooperator1"));
			Cooperator cooperator2 = this.AsCoroutine().YieldWaitForSecondsRealtime(1f).Action(() => Debug.Log("cooperator2"));
			Cooperator cooperator3 = this.AsCoroutine().YieldWaitEndOfFrame().Action(() => Debug.Log("cooperator3"));

			Cooperator newCooperator = 
				Random.value < 0.5f ? 
				cooperator1.YieldCoroutine(cooperator2) : 
				cooperator1.YieldCoroutine(cooperator3);

			newCooperator.Start(this);
		}

		private void Sample3()
		{
			this.AsCoroutine()
				.YieldCoroutine(UnityCoroutine())
				.Start(this);
		}

		private void Sample4()
		{
			Cooperator cooperator = this.AsCoroutine().YieldWaitForSeconds(1f);
			Cooperator newCooperator = cooperator.YieldCoroutine(UnityCoroutine());
			newCooperator.Start(this);
		}

		private IEnumerator UnityCoroutine()
		{
			Debug.Log("UnityCoroutine Start");
			yield return new WaitForSeconds(1f);
			Debug.Log("UnityCoroutine End");
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