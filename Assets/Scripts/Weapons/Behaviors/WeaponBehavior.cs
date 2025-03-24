
public interface WeaponBehavior<T>
{


    public abstract void Attack(T weapon);

    public abstract bool CanAttack();
}