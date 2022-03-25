namespace Abilities.Description
{
    public class IntDescriptionVariable : DescriptionVariable
    {
        private readonly int _nextValue;
        private readonly int _value;
        private readonly VariableName _name;

        public IntDescriptionVariable(VariableName name, int value, int nextValue) : base(name)
        {
            _value = value;
            _nextValue = nextValue;
            _name = name;
        }

        public override string GetDescription()
        {
            return new FloatDescriptionVariable(_name, _value, _nextValue).GetDescription();
        }
    }
}