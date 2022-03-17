namespace Abilities
{
    public abstract class Projectile<T> : PoolableObject where T : IAbilityStats
    {
        public abstract void Init(T stats);
    }
}