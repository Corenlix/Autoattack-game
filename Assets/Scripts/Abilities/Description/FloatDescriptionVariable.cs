namespace Abilities.Description
{
    public class FloatDescriptionVariable : DescriptionVariable
    {
        private readonly float _previousValue;
        private readonly float _value;

        public FloatDescriptionVariable(VariableName name, float value, float previousValue) : base(name)
        {
            _value = value;
            _previousValue = previousValue;
        }

        public override string GetDescription()
        {
            var delta = _value - _previousValue;
            if (delta == 0)
                return null;

            var result = GetVariableName() + ": ";
            if (delta > 0)
                result += "+";
            result += delta;

            return result;
        }
    }
}