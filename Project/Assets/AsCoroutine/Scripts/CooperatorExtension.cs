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

        public static CooperatorAction Action(this Cooperator cooperator, Action action)
        {
            return new CooperatorAction(cooperator.MonoBehaviour, cooperator, action);
        }

        public static CooperatorAction<TPrevInstruction> Action<TPrevInstruction>(this Cooperator<TPrevInstruction> cooperator, Action<TPrevInstruction> action)
        {
            return new CooperatorAction<TPrevInstruction>(cooperator.MonoBehaviour, cooperator, action);
        }

        public static CooperatorFunc<TCurrInstruction> YieldAction<TCurrInstruction>(this Cooperator cooperator, Func<TCurrInstruction> func)
        {
            return new CooperatorFunc<TCurrInstruction>(cooperator.MonoBehaviour, cooperator, func);
        }

        public static CooperatorFunc<TPrevInstruction, TCurrInstruction> YieldAction<TPrevInstruction, TCurrInstruction>(this Cooperator<TPrevInstruction> cooperator, Func<TPrevInstruction, TCurrInstruction> func)
        {
            return new CooperatorFunc<TPrevInstruction, TCurrInstruction>(cooperator.MonoBehaviour, cooperator, func);
        }

        public static CooperatorRepeat Repeat(this CooperatorAction cooperator, Func<bool> predicate)
        {
            return new CooperatorRepeat(cooperator.MonoBehaviour, cooperator, predicate);
        }

        public static CooperatorRepeat Repeat(this CooperatorAction cooperator, int repeatCount)
        {
            return new CooperatorRepeat(cooperator.MonoBehaviour, cooperator, repeatCount);
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

        #region - Yield Instructions

        public static Cooperator YieldNextFrame(this Cooperator cooperator)
        {
            return cooperator.Yield((object)null);
        }

        public static Cooperator<WWW> YieldWWW(this Cooperator cooperator, WWW www)
        {
            return cooperator.Yield(www);
        }

        public static Cooperator<WaitForEndOfFrame> YieldWaitEndOfFrame(this Cooperator cooperator)
        {
            return cooperator.Yield(new WaitForEndOfFrame());
        }

        public static Cooperator<WaitForFixedUpdate> YieldWaitForFixedUpdate(this Cooperator cooperator)
        {
            return cooperator.Yield(new WaitForFixedUpdate());
        }

        public static Cooperator<WaitForSeconds> YieldWaitForSeconds(this Cooperator cooperator, float seconds)
        {
            return cooperator.Yield(new WaitForSeconds(seconds));
        }

        public static Cooperator<WaitForSecondsRealtime> YieldWaitForSecondsRealtime(this Cooperator cooperator, float seconds)
        {
            return cooperator.Yield(new WaitForSecondsRealtime(seconds));
        }

        public static Cooperator<WaitUntil> YieldWaitUntil(this Cooperator cooperator, Func<bool> predicate)
        {
            return cooperator.Yield(new WaitUntil(predicate));
        }

        public static Cooperator<WaitWhile> YieldWaitWhile(this Cooperator cooperator, Func<bool> predicate)
        {
            return cooperator.Yield(new WaitWhile(predicate));
        }

        public static Cooperator<AsyncOperation> YieldAsyncOperation(this Cooperator cooperator, AsyncOperation asyncOperation)
        {
            return cooperator.Yield(asyncOperation);
        }

        public static Cooperator<TCustomYieldInstruction> YieldCustom<TCustomYieldInstruction>(this Cooperator cooperator, TCustomYieldInstruction customYieldInstruction)
            where TCustomYieldInstruction : CustomYieldInstruction
        {
            return cooperator.Yield(customYieldInstruction);
        }

        #endregion

        #endregion
    }
}