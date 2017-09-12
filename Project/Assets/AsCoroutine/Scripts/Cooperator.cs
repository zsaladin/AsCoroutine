using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsCoroutine
{
    public partial class Cooperator
    {
        public Cooperator Parent { get; private set; }
        private MonoBehaviour _monoBehaviour;

        public Cooperator(Cooperator parent)
        {
            Parent = parent;
        }

        protected virtual bool Instruct(object prevInstruction, out object resultInstruction)
        {
            resultInstruction = null;
            return false;
        }

        protected virtual bool IsRepeat()
        {
            return false;
        }

        public virtual Cooperator Clone()
        {
            return new Cooperator(Parent);
        }

        public Cooperator Start(MonoBehaviour monoBehaviour)
        {
            _monoBehaviour = monoBehaviour;
            _monoBehaviour.StartCoroutine(this);
            return this;
        }

        public void Stop(MonoBehaviour monoBehaviour = null)
        {
            if (monoBehaviour)
            {
                monoBehaviour.StopCoroutine(this);
                return;
            }

            if (_monoBehaviour)
            {
                _monoBehaviour.StopCoroutine(this);
                return;
            }

            Debug.LogError("Start MonoBehaviour not found. Use MonoBehaviour.StopCoroutine or Cooperator.Stop(MonoBehaviour) if you started it by MonoBehaviour.StartCoroutine");
        }
    }

    public class Cooperator<TCurrInstruction> : Cooperator
    {
        public TCurrInstruction Instruction { get; protected set; }

        public Cooperator(Cooperator parent, TCurrInstruction instruction)
            : base(parent)
        {
            Instruction = instruction;
        }

        protected override bool Instruct(object prevInstruction, out object resultInstruction)
        {
            resultInstruction = Instruction;
            return true;
        }

        public override Cooperator Clone()
        {
            return new Cooperator<TCurrInstruction>(Parent, Instruction);
        }
    }

    public class CooperatorAction : Cooperator
    {
        public Action Coroutine { get; private set; }

        public CooperatorAction(Cooperator parent, Action coroutine)
            : base(parent)
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

        public override Cooperator Clone()
        {
            return new CooperatorAction(Parent, Coroutine);
        }
    }

    public class CooperatorAction<TPrevInstruction> : Cooperator
    {
        public Action<TPrevInstruction> Coroutine { get; private set; }

        public CooperatorAction(Cooperator parent, Action<TPrevInstruction> coroutine)
            : base(parent)
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

        public override Cooperator Clone()
        {
            return new CooperatorAction<TPrevInstruction>(Parent, Coroutine);
        }
    }

    public class CooperatorFunc<TCurrInstruction> : Cooperator<TCurrInstruction>
    {
        public Func<TCurrInstruction> Coroutine { get; private set; }

        public CooperatorFunc(Cooperator parent, Func<TCurrInstruction> coroutine)
            : base(parent, default(TCurrInstruction))
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

        public override Cooperator Clone()
        {
            return new CooperatorFunc<TCurrInstruction>(Parent, Coroutine);
        }
    }

    public class CooperatorFunc<TPrevInstruction, TCurrInstruction> : Cooperator<TCurrInstruction>
    {
        public Func<TPrevInstruction, TCurrInstruction> Coroutine { get; private set; }

        public CooperatorFunc(Cooperator parent, Func<TPrevInstruction, TCurrInstruction> coroutine)
            : base(parent, default(TCurrInstruction))
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

        public override Cooperator Clone()
        {
            return new CooperatorFunc<TPrevInstruction, TCurrInstruction>(Parent, Coroutine);
        }
    }

    public class CooperatorRepeat : Cooperator
    {
        private Func<bool> _predicate;
        private int _repeatCount;
        private int _repeatCurrentCount;

        public CooperatorRepeat(Cooperator parent, Func<bool> predicate)
            : base(parent)
        {
            _predicate = predicate;
        }

        public CooperatorRepeat(Cooperator parent, int repeatCount)
            : base(parent)
        {
            _repeatCount = repeatCount;
            _predicate = () =>
            {
                return _repeatCurrentCount++ < _repeatCount;
            };
        }

        protected override bool IsRepeat()
        {
            if (_predicate == null)
                return false;
            return _predicate();
        }

        protected override void Reset()
        {
            base.Reset();

            _repeatCurrentCount = 0;
        }

        public override Cooperator Clone()
        {
            CooperatorRepeat newCooperator = new CooperatorRepeat(Parent, _predicate);
            newCooperator._repeatCount = _repeatCount;
            return newCooperator;
        }
    }
}