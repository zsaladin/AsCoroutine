using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsCoroutine
{
    public static partial class CooperatorExtension
    {
        public static Cooperator AsCoroutine(this object monoBehaviour)
        {
            return new Cooperator(null);
        }

        public static Cooperator<TCurrInstruction> Yield<TCurrInstruction>(this Cooperator cooperator, TCurrInstruction instruction)
        {
            return new Cooperator<TCurrInstruction>(cooperator, instruction);
        }

        public static CooperatorAction Action(this Cooperator cooperator, Action action)
        {
            return new CooperatorAction(cooperator, action);
        }

        public static CooperatorAction<TPrevInstruction> Action<TPrevInstruction>(this Cooperator<TPrevInstruction> cooperator, Action<TPrevInstruction> action)
        {
            return new CooperatorAction<TPrevInstruction>(cooperator, action);
        }

        public static CooperatorFunc<TCurrInstruction> YieldAction<TCurrInstruction>(this Cooperator cooperator, Func<TCurrInstruction> func)
        {
            return new CooperatorFunc<TCurrInstruction>(cooperator, func);
        }

        public static CooperatorFunc<TPrevInstruction, TCurrInstruction> YieldAction<TPrevInstruction, TCurrInstruction>(this Cooperator<TPrevInstruction> cooperator, Func<TPrevInstruction, TCurrInstruction> func)
        {
            return new CooperatorFunc<TPrevInstruction, TCurrInstruction>(cooperator, func);
        }


        #region - Repeat

        public static CooperatorRepeat Repeat(this CooperatorAction cooperator, Func<bool> predicate)
        {
            return new CooperatorRepeat(cooperator, predicate);
        }

        public static CooperatorRepeat Repeat(this CooperatorAction cooperator, int repeatCount)
        {
            return new CooperatorRepeat(cooperator, repeatCount);
        }

        public static CooperatorRepeat Repeat<TCurrInstruction>(this Cooperator<TCurrInstruction> cooperator, Func<bool> predicate)
        {
            return new CooperatorRepeat(cooperator, predicate);
        }

        public static CooperatorRepeat Repeat<TCurrInstruction>(this Cooperator<TCurrInstruction> cooperator, int repeatCount)
        {
            return new CooperatorRepeat(cooperator, repeatCount);
        }

        public static CooperatorRepeat Repeat<TPrevInstruction>(this CooperatorAction<TPrevInstruction> cooperator, Func<bool> predicate)
        {
            return new CooperatorRepeat(cooperator, predicate);
        }

        public static CooperatorRepeat Repeat<TPrevInstruction>(this CooperatorAction<TPrevInstruction> cooperator, int repeatCount)
        {
            return new CooperatorRepeat(cooperator, repeatCount);
        }

        #endregion

        #region - New

        public static Cooperator New(this Cooperator cooperator)
        {
            return cooperator.Clone();
        }

        public static Cooperator<TCurrInstruction> New<TCurrInstruction>(this Cooperator<TCurrInstruction> cooperator)
        {
            return cooperator.Clone() as Cooperator<TCurrInstruction>;
        }

        public static CooperatorAction New(this CooperatorAction cooperator)
        {
            return cooperator.Clone() as CooperatorAction;
        }

        public static CooperatorAction<TPrevInstruction> New<TPrevInstruction>(this CooperatorAction<TPrevInstruction> cooperator)
        {
            return cooperator.Clone() as CooperatorAction<TPrevInstruction>;
        }

        public static CooperatorFunc<TCurrInstruction> New<TCurrInstruction>(this CooperatorFunc<TCurrInstruction> cooperator)
        {
            return cooperator.Clone() as CooperatorFunc<TCurrInstruction>;
        }

        public static CooperatorFunc<TPrevInstruction, TCurrInstruction> New<TPrevInstruction, TCurrInstruction>(this CooperatorFunc<TPrevInstruction, TCurrInstruction> cooperator)
        {
            return cooperator.Clone() as CooperatorFunc<TPrevInstruction, TCurrInstruction>;
        }

        #endregion

        #region - Combine

        public static Cooperator YieldCoroutine(this Cooperator cooperator, Coroutine appendCoroutine)
        {
            return cooperator.Yield(appendCoroutine);
        }

        public static Cooperator YieldCoroutine(this Cooperator cooperator, IEnumerator appendCoroutine)
        {
            Cooperator appendCooperator = appendCoroutine as Cooperator;
            if (appendCooperator != null)
                cooperator.Yield(appendCooperator.New());

            return cooperator.Yield(appendCoroutine);
        }

        #endregion

        #region - Yield Instructions

        public static Cooperator YieldWaitForNextFrame(this Cooperator cooperator)
        {
            return cooperator.Yield((object)null);
        }

        public static Cooperator<WWW> YieldWWW(this Cooperator cooperator, WWW www)
        {
            return cooperator.Yield(www);
        }

        public static Cooperator<WaitForEndOfFrame> YieldWaitForEndOfFrame(this Cooperator cooperator)
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
            return cooperator.YieldAction(() => new WaitForSecondsRealtime(seconds));
        }

        public static Cooperator<WaitUntil> YieldWaitUntil(this Cooperator cooperator, Func<bool> predicate)
        {
            return cooperator.YieldAction(() => new WaitUntil(predicate));
        }

        public static Cooperator<WaitWhile> YieldWaitWhile(this Cooperator cooperator, Func<bool> predicate)
        {
            return cooperator.YieldAction(() => new WaitWhile(predicate));
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
    }
}