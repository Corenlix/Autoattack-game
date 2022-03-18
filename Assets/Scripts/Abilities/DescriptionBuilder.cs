using System;
using System.Collections.Generic;
using System.Text;

namespace Abilities
{
    public class DescriptionBuilder
    {
        private StringBuilder _currentDescriptionBuilder;
        private Func<bool>[] _appendActions;
        private bool _appended;

        public DescriptionBuilder(params Func<bool>[] appendActions)
        {
            _appendActions = appendActions;
        }
        
        public string Build()
        {
            _currentDescriptionBuilder = new StringBuilder();
            _appended = false;
            foreach (var action in _appendActions)
            {
                _appended = action();
            }

            return _currentDescriptionBuilder.ToString();
        }

        public bool AppendFloatValueDescription(string valueName, Func<float> valueFunc, Func<float> previousValueFunc)
        {
            return AppendFloatValueDescription(valueName, valueFunc(), previousValueFunc());
        }

        private bool AppendFloatValueDescription(string valueName, float value, float previousValue)
        {
            float delta = value - previousValue;
            if (delta == 0)
                return false;
            
            if(_appended)
                AppendSpace();
            _currentDescriptionBuilder.Append(valueName);
            _currentDescriptionBuilder.Append(": ");
            if (delta > 0)
                _currentDescriptionBuilder.Append("+");
            _currentDescriptionBuilder.Append(delta);
            return true;
        }

        public bool AppendIntRangeValueDescription(string valueName, Func<IntRange> valueFunc,
            Func<IntRange> previousValueFunc)
        {
            IntRange value = valueFunc();
            int averageValue = (value.Min + value.Max) / 2;

            IntRange previousValue = previousValueFunc();
            int averagePreviousValue = (previousValue.Min + previousValue.Max) / 2;
            
            return AppendFloatValueDescription(valueName, averageValue, averagePreviousValue);
        }

        private void AppendSpace()
        {
            _currentDescriptionBuilder.Append(", ");
        }
    }

    public static class DescriptionVariableNames
    {
        public const string ReloadTime = "Cooldown";
        public const string Damage = "Damage";
        public const string ProjectileSpeed = "Projectile speed";
        public const string KnockbackModifier = "Knockback modifier";
    }
}
