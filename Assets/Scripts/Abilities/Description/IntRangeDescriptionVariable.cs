namespace Abilities.Description
{
    public class IntRangeDescriptionVariable : DescriptionVariable
    {
        private readonly IntRange _value;
        private readonly IntRange _previousValue;
        private readonly VariableName _name;
        
        public IntRangeDescriptionVariable(VariableName name, IntRange value, IntRange previousValue) : base(name)
        {
            _value = value;
            _previousValue = previousValue;
            _name = name;
        }

        public override string GetDescription()
        {
            int averageValue = (_value.Min + _value.Max) / 2;
            int averagePreviousValue = (_previousValue.Min + _previousValue.Max) / 2;

            return new FloatDescriptionVariable(_name, averageValue, averagePreviousValue).GetDescription();
        }
    }
}