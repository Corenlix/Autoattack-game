using System;
using UnityEngine;

namespace Abilities
{
    public class TimerAction
    {
        private float _remainTimeToUse;
        private Func<float> _getReloadTimeFunc;
        private Action _action;
    
        public TimerAction(Func<float> getReloadTimeFunc, Action action)
        {
            _getReloadTimeFunc = getReloadTimeFunc;
            _action = action;
        }
    
        public void Update()
        {
            _remainTimeToUse -= Time.deltaTime;
            if ((_remainTimeToUse > 0)) return;
            _action.Invoke();
            _remainTimeToUse = _getReloadTimeFunc.Invoke();
        }
    }
}
