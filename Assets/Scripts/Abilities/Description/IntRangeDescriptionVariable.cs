namespace Abilities.Description
{
    public class IntRangeDescriptionVariable : DescriptionVariable
    {
        private readonly VariableName _name;
        private readonly IntRange _previousValue;
        private readonly IntRange _value;

        public IntRangeDescriptionVariable(VariableName name, IntRange value, IntRange previousValue) : base(name)
        {
            _value = value;
            _previousValue = previousValue;
            _name = name;
        }

        public override string GetDescription()
        {
            var averageValue = (_value.Min + _value.Max) / 2;
            var averagePreviousValue = (_previousValue.Min + _previousValue.Max) / 2;

            return new FloatDescriptionVariable(_name, averageValue, averagePreviousValue).GetDescription();
        }
    }
}