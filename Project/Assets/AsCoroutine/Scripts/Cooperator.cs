using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsCoroutine
{
    public partial class Cooperator : IEnumerable
    {
        private List<Coroutine> _runningCoroutines = new List<Coroutine>();

        public MonoBehaviour MonoBehaviour { get; private set; }
        public Cooperator Parent { get; private set; }

        public Cooperator(MonoBehaviour monoBehaviour, Cooperator parent)
        {
            MonoBehaviour = monoBehaviour;
            Parent = parent;
        }

        public IEnumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        protected virtual bool Instruct(object prevInstruction, out object resultInstruction)
        {
            resultInstruction = null;
            return false;
        }

        protected virtual bool IsRepeat(object prevInstruction)
        {
            return false;
        }

        public Cooperator Start()
        {
            StartCoroutine();
            return this;
        }

        public Coroutine StartCoroutine()
        {
            Coroutine coroutine = MonoBehaviour.StartCoroutine(GetEnumerator());
            _runningCoroutines.Add(coroutine);
            return coroutine;
        }

        public void StopAll()
        {
            foreach (Coroutine coroutine in _runningCoroutines)
            {
                MonoBehaviour.StopCoroutine(coroutine);
            }

            Clear();
        }

        public void Clear()
        {
            _runningCoroutines.Clear();
        }
    }

    public class Cooperator<TCurrInstruction> : Cooperator
    {
        public TCurrInstruction Instruction { get; protected set; }

        public Cooperator(MonoBehaviour monoBehaviour, Cooperator parent, TCurrInstruction instruction)
            : base(monoBehaviour, parent)
        {
            Instruction = instruction;
        }

        protected override bool Instruct(object prevInstruction, out object resultInstruction)
        {
            resultInstruction = Instruction;
            return true;
        }
    }

    public class CooperatorAction : Cooperator
    {
        public Action Coroutine { get; private set; }

        public CooperatorAction(MonoBehaviour monoBehaviour, Cooperator parent, Action coroutine)
            : base(monoBehaviour, parent)
        {
            Coroutine = coroutine;
        }

        protected override bool Instruct(object prevInstruction, out object resultInstruction)
        {
            resultInstruction = null;
            if (Coroutine == null)
                return true;

            Coroutine();
            return false;
        }
    }

    public class CooperatorAction<TPrevInstruction> : Cooperator
    {
        public Action<TPrevInstruction> Coroutine { get; private set; }

        public CooperatorAction(MonoBehaviour monoBehaviour, Cooperator parent, Action<TPrevInstruction> coroutine)
            : base(monoBehaviour, parent)
        {
            Coroutine = coroutine;
        }

        protected override bool Instruct(object prevInstruction, out object resultInstruction)
        {
            resultInstruction = null;
            if (Coroutine == null)
                return true;

            TPrevInstruction prevInstuction =
                prevInstruction is TPrevInstruction ?
                (TPrevInstruction)prevInstruction :
                default(TPrevInstruction);

            Coroutine(prevInstuction);
            return false;
        }
    }

    public class CooperatorFunc<TCurrInstruction> : Cooperator<TCurrInstruction>
    {
        public Func<TCurrInstruction> Coroutine { get; private set; }

        public CooperatorFunc(MonoBehaviour monoBehaviour, Cooperator parent, Func<TCurrInstruction> coroutine)
            : base(monoBehaviour, parent, default(TCurrInstruction))
        {
            Coroutine = coroutine;
        }

        protected override bool Instruct(object prevInstruction, out object resultInstruction)
        {
            if (Coroutine == null)
                resultInstruction = Instruction = default(TCurrInstruction);
            else
                resultInstruction = Instruction = Coroutine();
            return true;
        }
    }

    public class CooperatorFunc<TPrevInstruction, TCurrInstruction> : Cooperator<TCurrInstruction>
    {
        public Func<TPrevInstruction, TCurrInstruction> Coroutine { get; private set; }

        public CooperatorFunc(MonoBehaviour monoBehaviour, Cooperator parent, Func<TPrevInstruction, TCurrInstruction> coroutine)
            : base(monoBehaviour, parent, default(TCurrInstruction))
        {
            Coroutine = coroutine;
        }

        protected override bool Instruct(object prevInstruction, out object resultInstruction)
        {
            if (Coroutine == null)
            {
                resultInstruction = default(TCurrInstruction);
            }
            else
            {
                TPrevInstruction prevInstuction =
                prevInstruction is TPrevInstruction ?
                (TPrevInstruction)prevInstruction :
                default(TPrevInstruction);

                resultInstruction = Instruction = Coroutine(prevInstuction);
            }
            return true;
        }
    }

    public class CooperatorRepeat : Cooperator
    {
        private Func<bool> _predicate;
        private int _repeatCount;

        public CooperatorRepeat(MonoBehaviour monoBehaviour, Cooperator parent, Func<bool> predicate)
            : base(monoBehaviour, parent)
        {
            _predicate = predicate;
        }

        public CooperatorRepeat(MonoBehaviour monoBehaviour, Cooperator parent, int repeatCount)
            : base(monoBehaviour, parent)
        {
            _repeatCount = repeatCount;
            _predicate = () =>
            {
                return --_repeatCount > 0;
            };
        }

        protected override bool IsRepeat(object prevInstruction)
        {
            if (_predicate == null)
                return false;
            return _predicate();
        }
    }
}