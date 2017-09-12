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

			if (Random.value < 0.5f)
				cooperator1 = cooperator1.YieldCoroutine(cooperator2);
			else
				cooperator1 = cooperator1.YieldCoroutine(cooperator3);

			cooperator1.Start(this);
		}

		private void Sample3()
		{
			Cooperator cooperator = this.AsCoroutine().Action(() => Debug.Log("Sample3")).Repeat(() => true).Start(this);
			this.AsCoroutine()
				.YieldWaitForSeconds(2f)
				.Action(() => cooperator.Stop())
				.Action(() => Debug.Log("Stop"))
				.Start(this);
		}

		private void Sample4()
		{
			Cooperator cooperator = this.AsCoroutine().Action(() => Debug.Log("Sample4")).Repeat(() => true);
			StartCoroutine(cooperator);

			this.AsCoroutine()
				.YieldWaitForSeconds(2f)
				.Action(() => StopCoroutine(cooperator))
				.Action(() => Debug.Log("Stop"))
				.Start(this);
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