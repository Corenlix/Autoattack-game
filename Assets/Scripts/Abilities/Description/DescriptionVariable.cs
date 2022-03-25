using System.ComponentModel;

namespace Abilities.Description
{
    public abstract class DescriptionVariable
    {
        private readonly VariableName _name;

        protected DescriptionVariable(VariableName name)
        {
            _name = name;
        }

        protected string GetVariableName()
        {
            return _name switch
            {
                VariableName.ReloadTime => "Cooldown",
                VariableName.Damage => "Damage",
                VariableName.ProjectileSpeed => "ProjectileSpeed",
                VariableName.OrbitRadius => "Orbit radius",
                VariableName.TurnoverPeriod => "Turnover time",
                VariableName.ProjectilesCount => "Projectiles",
                _ => throw new InvalidEnumArgumentException()
            };
        }

        public abstract string GetDescription();
    }

    public enum VariableName
    {
        ReloadTime,
        Damage,
        ProjectileSpeed,
        OrbitRadius,
        TurnoverPeriod,
        ProjectilesCount,
    }
}