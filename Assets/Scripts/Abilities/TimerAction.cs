using System;
using UnityEngine;

namespace Abilities
{
    public class TimerAction
    {
        private readonly Action _action;
        private readonly Func<float> _getReloadTimeFunc;
        private float _remainTimeToUse;

        public TimerAction(Func<float> getReloadTimeFunc, Action action)
        {
            _getReloadTimeFunc = getReloadTimeFunc;
            _action = action;
        }

        public void Update()
        {
            _remainTimeToUse -= Time.deltaTime;
            if (_remainTimeToUse > 0) return;
            _action.Invoke();
            _remainTimeToUse = _getReloadTimeFunc.Invoke();
        }
    }
}