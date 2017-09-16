# AsCoroutine

AsCouroutine is a unity asset that allows you to use Coroutine in a functional style. You can handle coroutine as first-class citizen so that you can use method chain and combine coroutines.

## How to use

You might face the case below. You would have to write an additional method because of using just one line of 'yield return' state related with some instructions like 'WaitForSeconds', 'WaitForEndOfFrame' and so on. Sometimes it is annoying and makes your code ugly.

```C#
private void OnEnable()
{
    StartCoroutine(UnityCoroutine());
}

private IEnumerator UnityCoroutine()
{
    yield return new WaitForSeconds(1f);
    Debug.Log("UnityCoroutine");
}
```

Now you are free from writing the additional method.

```C#
private void OnEnable()
{
    this.AsCoroutine()
        .YieldWaitForSeconds(1f).Action(() => Debug.Log("AsCoroutine"))
        .Start(this);
}
```

Also it makes 'Awake' and 'OnEnable' use Coroutine. Unlike 'Start' we could not use Coroutine in them but you can from now.
(Actually there is an another alternative in above situation. See [link](https://github.com/zsaladin/Asyncoroutine))

Use method chain:
```C#
private void OnEnable()
{
    this.AsCoroutine()
        .YieldWaitForSeconds(1f).Action(() => Debug.Log("WaitForSeconds"))
        .YieldWaitForNextFrame().Action(() => Debug.Log("WaitForNextFrame"))
        .YieldWaitForEndOfFrame().Action(() => Debug.Log("WaitForEndOfFrame"))
        .YieldWaitForFixedUpdate().Action(() => Debug.Log("WaitForFixedUpdate"))
        .Start(this);
}
```

It provides a way to combine coroutines:
```C#
private void OnEnable()
{
    Cooperator co1 = this.AsCoroutine().YieldWaitForSeconds(1f).Action(() => Debug.Log("co1"));
    Cooperator co2 = this.AsCoroutine().YieldWaitForNextFrame().Action(() => Debug.Log("co2"));
    Cooperator co3 = this.AsCoroutine().YieldWaitForEndOfFrame().Action(() => Debug.Log("co3"));

    Cooperator newCo =
        Random.value < 0.5f ?
        co1.YieldCoroutine(co2) :
        co1.YieldCoroutine(co3);

    newCo.Start(this);
}
```

You can use 'WWW' with AsCoroutine:
```C#
private void OnEnable()
{
    this.AsCoroutine()
        .YieldWWW(new WWW("http://ip.jsontest.com/?mime=5")).Action(www =>
        {
            Debug.Log(www.text);
        }).Start(this);
}
```

Handle it like an original coroutine:
```C#
private Cooperator _cooperator;
private void OnEnable()
{
    _cooperator = this.AsCoroutine().Action(() => Debug.Log("Example4")).Repeat(() => true);
    StartCoroutine(_cooperator);
    // _cooperator.Start(this);
}

private void OnMouseDown()
{
    StopCoroutine(_cooperator);
    // _cooperator.Stop();
}
```

## Author
- Kim Daehee, Software engineer in Korea.
- zsaladinz@gmail.com

## License
- This asset is under MIT License.
