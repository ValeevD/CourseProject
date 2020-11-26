
public interface IAttack {
    int Damage{get; set;}

    void DoDamage(IHealth target);
}
