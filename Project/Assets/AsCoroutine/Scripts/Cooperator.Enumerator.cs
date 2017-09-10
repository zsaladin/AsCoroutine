using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsCoroutine
{
    public partial class Cooperator
    {
        public class Enumerator : IEnumerator
        {
            private Stack<Cooperator> _cooperators;
            private Cooperator _tail;

            public object Current { get; private set; }

            public Enumerator(Cooperator tail)
            {
                _tail = tail;
                GetCooperators();
            }

            public virtual bool MoveNext()
            {
                while (_cooperators.Count > 0)
                {
                    Cooperator currCooperator = _cooperators.Pop();

                    object prevInstruction = Current;
                    object resultInstruction;
                    if (currCooperator.Instruct(prevInstruction, out resultInstruction))
                    {
                        Current = resultInstruction;
                        return true;
                    }
                }
                return false;
            }

            public virtual void Reset()
            {
                _cooperators.Clear();
                GetCooperators();
            }

            private void GetCooperators()
            {
                _cooperators = new Stack<Cooperator>();

                Cooperator curr = _tail;
                while (curr != null)
                {
                    _cooperators.Push(curr);
                    curr = curr.Parent;
                }
            }
        }
    }
}