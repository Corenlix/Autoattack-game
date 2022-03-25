namespace Abilities.Description
{
    public class FloatDescriptionVariable : DescriptionVariable
    {
        private readonly float _nextValue;
        private readonly float _value;

        public FloatDescriptionVariable(VariableName name, float value, float nextValue) : base(name)
        {
            _value = value;
            _nextValue = nextValue;
        }

        public override string GetDescription()
        {
            var delta = _nextValue - _value;
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