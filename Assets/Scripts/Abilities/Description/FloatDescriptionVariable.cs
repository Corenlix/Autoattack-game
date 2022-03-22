namespace Abilities.Description
{
    public class FloatDescriptionVariable : DescriptionVariable
    {
        private float _value;
        private float _previousValue;
        
        public FloatDescriptionVariable(VariableName name, float value, float previousValue) : base(name)
        {
            _value = value;
            _previousValue = previousValue;
        }
        
        public override string GetDescription()
        {
            float delta = _value - _previousValue;
            if (delta == 0)
                return null;

            string result = GetVariableName() + ": ";
            if (delta > 0)
                result += "+";
            result += delta;

            return result;
        }
    }
}