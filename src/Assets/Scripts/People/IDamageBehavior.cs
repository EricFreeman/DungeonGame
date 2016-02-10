namespace Assets.Scripts.People
{
    public interface IDamageBehavior
    {
        void OnHit(HitContext hitContext);
        void OnDeath();
    }
}