namespace Abilities.Description
{
    public class IntRangeDescriptionVariable : DescriptionVariable
    {
        private readonly VariableName _name;
        private readonly IntRange _nextValue;
        private readonly IntRange _value;

        public IntRangeDescriptionVariable(VariableName name, IntRange value, IntRange nextValue) : base(name)
        {
            _value = value;
            _nextValue = nextValue;
            _name = name;
        }

        public override string GetDescription()
        {
            var averageValue = (_value.Min + _value.Max) / 2;
            var averageNextValue = (_nextValue.Min + _nextValue.Max) / 2;

            return new FloatDescriptionVariable(_name, averageValue, averageNextValue).GetDescription();
        }
    }
}