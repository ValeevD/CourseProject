using UnityEngine;

public interface ICharacter
{
    void SetRandomPosition(float radius);
    void SetRandomStartPosition(float radius, int partOfCircle);

    Vector2 GetPosition();
    Vector2 GetDirection();

    void SetPosition(Vector2 newPosition);
    void SetDirection(Vector2 newDirection);

    bool isEnemy{get; set;}
    int Health { get; set; }
    int Damage { get; set; }

    void Cleanup();
}
