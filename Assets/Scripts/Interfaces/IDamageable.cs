using Model;

namespace Interfaces
{
    public interface IDamageable
    {
        public UnitTeam Team { get; }
        public void Damage(float damage);
    }
}