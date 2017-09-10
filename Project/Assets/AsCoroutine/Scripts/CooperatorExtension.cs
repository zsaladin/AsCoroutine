using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsCoroutine
{
    public static partial class CooperatorExtension
    {
        public static Cooperator AsCoroutine(this MonoBehaviour monoBehaviour)
        {
            return new Cooperator(monoBehaviour, null);
        }

        public static Cooperator<TCurrInstruction> Yield<TCurrInstruction>(this Cooperator cooperator, TCurrInstruction instruction)
        {
            return new Cooperator<TCurrInstruction>(cooperator.MonoBehaviour, cooperator, instruction);
        }

        public static CooperatorAction YieldAction(this Cooperator cooperator, Action action)
        {
            return new CooperatorAction(cooperator.MonoBehaviour, cooperator, action);
        }

        public static CooperatorAction<TPrevInstruction> YieldAction<TPrevInstruction>(this Cooperator<TPrevInstruction> cooperator, Action<TPrevInstruction> action)
        {
            return new CooperatorAction<TPrevInstruction>(cooperator.MonoBehaviour, cooperator, action);
        }

        public static CooperatorFunc<TCurrInstruction> YieldFunc<TCurrInstruction>(this Cooperator cooperator, Func<TCurrInstruction> func)
        {
            return new CooperatorFunc<TCurrInstruction>(cooperator.MonoBehaviour, cooperator, func);
        }

        public static CooperatorFunc<TPrevInstruction, TCurrInstruction> YieldFunc<TPrevInstruction, TCurrInstruction>(this Cooperator<TPrevInstruction> cooperator, Func<TPrevInstruction, TCurrInstruction> func)
        {
            return new CooperatorFunc<TPrevInstruction, TCurrInstruction>(cooperator.MonoBehaviour, cooperator, func);
        }


        #region Helper

        #region - Combine

        public static Cooperator YieldCoroutine(this Cooperator cooperator, Cooperator appendCooperator)
        {
            return cooperator.YieldCoroutine(appendCooperator.GetEnumerator());
        }

        public static Cooperator YieldCoroutine(this Cooperator cooperator, Coroutine appendCoroutine)
        {
            return cooperator.Yield<Coroutine>(appendCoroutine);
        }

        public static Cooperator YieldCoroutine(this Cooperator cooperator, IEnumerator appendCoroutine)
        {
            return cooperator.Yield<IEnumerator>(appendCoroutine);
        }

        #endregion

        #region - NextFrame
        public static Cooperator YieldNextFrame(this Cooperator cooperator)
        {
            return cooperator.Yield((object)null);
        }

        public static CooperatorAction YieldNextFrame(this Cooperator cooperator, Action action)
        {
            return cooperator.YieldNextFrame().YieldAction(action);
        }

        public static CooperatorFunc<TCurrInstruction> YieldNextFrame<TCurrInstruction>(this Cooperator cooperator, Func<TCurrInstruction> func)
        {
            return cooperator.YieldNextFrame().YieldFunc(func);
        }
        #endregion

        #region - WWW
        public static Cooperator<WWW> YieldWWW(this Cooperator cooperator, WWW www)
        {
            return cooperator.Yield(www);
        }

        public static CooperatorAction YieldWWW(this Cooperator cooperator, WWW www, Action action)
        {
            return cooperator.YieldWWW(www).YieldAction(action);
        }

        public static CooperatorAction<WWW> YieldWWW(this Cooperator cooperator, WWW www, Action<WWW> action)
        {
            return cooperator.YieldWWW(www).YieldAction(action);
        }

        public static CooperatorFunc<TCurrInstruction> YieldWWW<TCurrInstruction>(this Cooperator cooperator, WWW www, Func<TCurrInstruction> func)
        {
            return cooperator.YieldWWW(www).YieldFunc(func);
        }

        public static CooperatorFunc<WWW, TCurrInstruction> YieldWWW<TCurrInstruction>(this Cooperator cooperator, WWW www, Func<WWW, TCurrInstruction> func)
        {
            return cooperator.YieldWWW(www).YieldFunc(func);
        }

        #endregion

        #region - WaitEndOfFrame
        public static Cooperator<WaitForEndOfFrame> YieldWaitEndOfFrame(this Cooperator cooperator)
        {
            return cooperator.Yield(new WaitForEndOfFrame());
        }

        public static CooperatorAction YieldWaitEndOfFrame(this Cooperator cooperator, Action action)
        {
            return cooperator.YieldWaitEndOfFrame().YieldAction(action);
        }

        public static CooperatorAction<WaitForEndOfFrame> YieldWaitEndOfFrame(this Cooperator cooperator, Action<WaitForEndOfFrame> action)
        {
            return cooperator.YieldWaitEndOfFrame().YieldAction(action);
        }

        public static CooperatorFunc<TCurrInstruction> YieldWaitEndOfFrame<TCurrInstruction>(this Cooperator cooperator, Func<TCurrInstruction> func)
        {
            return cooperator.YieldWaitEndOfFrame().YieldFunc(func);
        }

        public static CooperatorFunc<WaitForEndOfFrame, TCurrInstruction> YieldWaitEndOfFrame<TCurrInstruction>(this Cooperator cooperator, Func<WaitForEndOfFrame, TCurrInstruction> func)
        {
            return cooperator.YieldWaitEndOfFrame().YieldFunc(func);
        }

        #endregion

        #region - WaitForFixedUpdate
        public static Cooperator<WaitForFixedUpdate> YieldWaitForFixedUpdate(this Cooperator cooperator)
        {
            return cooperator.Yield(new WaitForFixedUpdate());
        }

        public static CooperatorAction YieldWaitForFixedUpdate(this Cooperator cooperator, Action action)
        {
            return cooperator.YieldWaitForFixedUpdate().YieldAction(action);
        }

        public static CooperatorAction<WaitForFixedUpdate> YieldWaitForSecondsRealtime(this Cooperator cooperator, Action<WaitForFixedUpdate> action)
        {
            return cooperator.YieldWaitForFixedUpdate().YieldAction(action);
        }

        public static CooperatorFunc<TCurrInstruction> YieldWaitForSecondsRealtime<TCurrInstruction>(this Cooperator cooperator, Func<TCurrInstruction> func)
        {
            return cooperator.YieldWaitForFixedUpdate().YieldFunc(func);
        }

        public static CooperatorFunc<WaitForFixedUpdate, TCurrInstruction> YieldWaitForSeconds<TCurrInstruction>(this Cooperator cooperator, Func<WaitForFixedUpdate, TCurrInstruction> func)
        {
            return cooperator.YieldWaitForFixedUpdate().YieldFunc(func);
        }
        #endregion

        #region - WaitForSeconds
        public static Cooperator<WaitForSeconds> YieldWaitForSeconds(this Cooperator cooperator, float seconds)
        {
            return cooperator.Yield(new WaitForSeconds(seconds));
        }

        public static CooperatorAction YieldWaitForSeconds(this Cooperator cooperator, float seconds, Action action)
        {
            return cooperator.YieldWaitForSeconds(seconds).YieldAction(action);
        }

        public static CooperatorAction<WaitForSeconds> YieldWaitForSeconds(this Cooperator cooperator, float seconds, Action<WaitForSeconds> action)
        {
            return cooperator.YieldWaitForSeconds(seconds).YieldAction(action);
        }

        public static CooperatorFunc<TCurrInstruction> YieldWaitForSeconds<TCurrInstruction>(this Cooperator cooperator, float seconds, Func<TCurrInstruction> func)
        {
            return cooperator.YieldWaitForSeconds(seconds).YieldFunc(func);
        }

        public static CooperatorFunc<WaitForSeconds, TCurrInstruction> YieldWaitForSeconds<TCurrInstruction>(this Cooperator cooperator, float seconds, Func<WaitForSeconds, TCurrInstruction> func)
        {
            return cooperator.YieldWaitForSeconds(seconds).YieldFunc(func);
        }
        #endregion

        #region - WaitForSecondsRealtime
        public static Cooperator<WaitForSecondsRealtime> YieldWaitForSecondsRealtime(this Cooperator cooperator, float seconds)
        {
            return cooperator.Yield(new WaitForSecondsRealtime(seconds));
        }

        public static CooperatorAction YieldWaitForSecondsRealtime(this Cooperator cooperator, float seconds, Action action)
        {
            return cooperator.YieldWaitForSecondsRealtime(seconds).YieldAction(action);
        }

        public static CooperatorAction<WaitForSecondsRealtime> YieldWaitForSecondsRealtime(this Cooperator cooperator, float seconds, Action<WaitForSecondsRealtime> action)
        {
            return cooperator.YieldWaitForSecondsRealtime(seconds).YieldAction(action);
        }

        public static CooperatorFunc<TCurrInstruction> YieldWaitForSecondsRealtime<TCurrInstruction>(this Cooperator cooperator, float seconds, Func<TCurrInstruction> func)
        {
            return cooperator.YieldWaitForSecondsRealtime(seconds).YieldFunc(func);
        }

        public static CooperatorFunc<WaitForSecondsRealtime, TCurrInstruction> YieldWaitForSeconds<TCurrInstruction>(this Cooperator cooperator, float seconds, Func<WaitForSecondsRealtime, TCurrInstruction> func)
        {
            return cooperator.YieldWaitForSecondsRealtime(seconds).YieldFunc(func);
        }
        #endregion

        #region - WaitUntil
        public static Cooperator<WaitUntil> YieldWaitUntil(this Cooperator cooperator, Func<bool> predicate)
        {
            return cooperator.Yield(new WaitUntil(predicate));
        }

        public static CooperatorAction YieldWaitUntil(this Cooperator cooperator, Func<bool> predicate, Action action)
        {
            return cooperator.YieldWaitUntil(predicate).YieldAction(action);
        }

        public static CooperatorAction<WaitUntil> YieldWaitUntil(this Cooperator cooperator, Func<bool> predicate, Action<WaitUntil> action)
        {
            return cooperator.YieldWaitUntil(predicate).YieldAction(action);
        }

        public static CooperatorFunc<TCurrInstruction> YieldWaitUntil<TCurrInstruction>(this Cooperator cooperator, Func<bool> predicate, Func<TCurrInstruction> func)
        {
            return cooperator.YieldWaitUntil(predicate).YieldFunc(func);
        }

        public static CooperatorFunc<WaitUntil, TCurrInstruction> YieldWaitUntil<TCurrInstruction>(this Cooperator cooperator, Func<bool> predicate, Func<WaitUntil, TCurrInstruction> func)
        {
            return cooperator.YieldWaitUntil(predicate).YieldFunc(func);
        }
        #endregion

        #region - WaitWhile
        public static Cooperator<WaitWhile> YieldWaitWhile(this Cooperator cooperator, Func<bool> predicate)
        {
            return cooperator.Yield(new WaitWhile(predicate));
        }

        public static CooperatorAction YieldWaitWhile(this Cooperator cooperator, Func<bool> predicate, Action action)
        {
            return cooperator.YieldWaitWhile(predicate).YieldAction(action);
        }

        public static CooperatorAction<WaitWhile> YieldWaitWhile(this Cooperator cooperator, Func<bool> predicate, Action<WaitWhile> action)
        {
            return cooperator.YieldWaitWhile(predicate).YieldAction(action);
        }

        public static CooperatorFunc<TCurrInstruction> YieldWaitWhile<TCurrInstruction>(this Cooperator cooperator, Func<bool> predicate, Func<TCurrInstruction> func)
        {
            return cooperator.YieldWaitWhile(predicate).YieldFunc(func);
        }

        public static CooperatorFunc<WaitWhile, TCurrInstruction> YieldWaitUntil<TCurrInstruction>(this Cooperator cooperator, Func<bool> predicate, Func<WaitWhile, TCurrInstruction> func)
        {
            return cooperator.YieldWaitWhile(predicate).YieldFunc(func);
        }
        #endregion

        #region - AsyncOperation
        public static Cooperator<AsyncOperation> YieldAsyncOperation(this Cooperator cooperator, AsyncOperation asyncOperation)
        {
            return cooperator.Yield(asyncOperation);
        }

        public static CooperatorAction YieldWaitWhile(this Cooperator cooperator, AsyncOperation asyncOperation, Action action)
        {
            return cooperator.YieldAsyncOperation(asyncOperation).YieldAction(action);
        }

        public static CooperatorAction<AsyncOperation> YieldWaitWhile(this Cooperator cooperator, AsyncOperation asyncOperation, Action<AsyncOperation> action)
        {
            return cooperator.YieldAsyncOperation(asyncOperation).YieldAction(action);
        }

        public static CooperatorFunc<TCurrInstruction> YieldWaitWhile<TCurrInstruction>(this Cooperator cooperator, AsyncOperation asyncOperation, Func<TCurrInstruction> func)
        {
            return cooperator.YieldAsyncOperation(asyncOperation).YieldFunc(func);
        }

        public static CooperatorFunc<AsyncOperation, TCurrInstruction> YieldWaitUntil<TCurrInstruction>(this Cooperator cooperator, AsyncOperation asyncOperation, Func<AsyncOperation, TCurrInstruction> func)
        {
            return cooperator.YieldAsyncOperation(asyncOperation).YieldFunc(func);
        }
        #endregion

        #region - CustomYieldInstruction
        public static Cooperator<TCustomYieldInstruction> YieldCustom<TCustomYieldInstruction>(this Cooperator cooperator, TCustomYieldInstruction customYieldInstruction)
            where TCustomYieldInstruction : CustomYieldInstruction
        {
            return cooperator.Yield(customYieldInstruction);
        }

        public static CooperatorAction YieldCustom<TCustomYieldInstruction>(this Cooperator cooperator, TCustomYieldInstruction customYieldInstruction, Action action)
            where TCustomYieldInstruction : CustomYieldInstruction
        {
            return cooperator.YieldCustom(customYieldInstruction).YieldAction(action);
        }

        public static CooperatorAction<TCustomYieldInstruction> YieldCustom<TCustomYieldInstruction>(this Cooperator cooperator, TCustomYieldInstruction customYieldInstruction, Action<TCustomYieldInstruction> action) 
            where TCustomYieldInstruction : CustomYieldInstruction
        {
            return cooperator.YieldCustom(customYieldInstruction).YieldAction(action);
        }

        public static CooperatorFunc<TCurrInstruction> YieldCustom<TCustomYieldInstruction, TCurrInstruction>(this Cooperator cooperator, TCustomYieldInstruction customYieldInstruction, Func<TCurrInstruction> func)
            where TCustomYieldInstruction : CustomYieldInstruction
        {
            return cooperator.YieldCustom(customYieldInstruction).YieldFunc(func);
        }

        public static CooperatorFunc<TCustomYieldInstruction, TCurrInstruction> YieldCustom<TCustomYieldInstruction, TCurrInstruction>(this Cooperator cooperator, TCustomYieldInstruction customYieldInstruction, Func<TCustomYieldInstruction, TCurrInstruction> func)
            where TCustomYieldInstruction : CustomYieldInstruction
        {
            return cooperator.YieldCustom(customYieldInstruction).YieldFunc(func);
        }
        #endregion

        #endregion
    }
}