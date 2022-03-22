using System.Text;

namespace Abilities.Description
{
    public class DescriptionBuilder
    {
        private readonly DescriptionVariable[] _variables;

        public DescriptionBuilder(DescriptionVariable[] variables)
        {
            _variables = variables;
        }

        public string Build()
        {
            var builder = new StringBuilder();
            var appended = false;
            foreach (var variable in _variables)
            {
                var variableDescription = variable.GetDescription();
                if (string.IsNullOrEmpty(variableDescription))
                    continue;

                if (appended)
                    builder.Append(", ");

                appended = true;
                builder.Append(variableDescription);
            }

            return builder.ToString();
        }
    }
}