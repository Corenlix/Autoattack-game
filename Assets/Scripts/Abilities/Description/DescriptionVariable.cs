using System.ComponentModel;

namespace Abilities.Description
{
    public abstract class DescriptionVariable
    {
        protected string GetVariableName()
        {
            return _name switch
            {
                VariableName.ReloadTime => "Cooldown",
                VariableName.Damage => "Damage",
                VariableName.ProjectileSpeed => "ProjectileSpeed",
                _ => throw new InvalidEnumArgumentException()
            };
        }

        protected DescriptionVariable(VariableName name)
        {
            _name = name;
        }
        
        private readonly VariableName _name;
        public abstract string GetDescription();
    }
    
    public enum VariableName
    {
        ReloadTime,
        Damage,
        ProjectileSpeed,
    }
}