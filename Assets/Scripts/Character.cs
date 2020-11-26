using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : ICharacter, IHealth, IAttack
{
    private Vector2 positionOnMap;
    private Vector2 direction;

    private float moveRadius;

    public bool isEnemy {get; set;}
    public int Health { get; set; }
    public int Damage { get; set; }

    public Character(bool _isEnemy)
    {
        positionOnMap = new Vector2();
        direction = new Vector2();

        Health = 1;
        Damage = 1;

        moveRadius = 5;
        isEnemy = _isEnemy;
    }

    public void SetRandomStartPosition(float radius, int partOfCircle)
    {
        Vector2 newPosition = new Vector2(Random.Range(-radius, radius), Random.Range(-radius, radius));
        positionOnMap = PositionChecker.CheckStartPosition(newPosition, radius, partOfCircle);
    }

    public void SetRandomPosition(float radius)
    {
        Vector2 newPosition = new Vector2(Random.Range(-radius, radius), Random.Range(-radius, radius));
        positionOnMap = PositionChecker.CheckPosition(positionOnMap, newPosition, radius, moveRadius);

        Vector2 newDirection = new Vector2(Random.Range(-radius, radius), Random.Range(-radius, radius));
        direction = PositionChecker.CheckPosition(positionOnMap, newPosition, radius, moveRadius);
    }

    public Vector2 GetPosition()
    {
        return positionOnMap;
    }

    public Vector2 GetDirection()
    {
        return direction;
    }

    public void SetPosition(Vector2 newPosition)
    {
        positionOnMap = PositionChecker.CheckPosition(positionOnMap, newPosition, 15, moveRadius);
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
    }

    public void GetDamage(int amount)
    {
        Health -= amount;
    }

    public void DoDamage(IHealth target)
    {
        throw new System.NotImplementedException();
    }

    public void Cleanup()
    {
    }
}
