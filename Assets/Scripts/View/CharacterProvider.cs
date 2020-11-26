using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProvider : MonoBehaviour, ISynchronize
{
    [SerializeField] private Transform body;
    [SerializeField] private MeshRenderer bodyRenderer;
    [SerializeField] private Color color;
    [SerializeField] private Transform borders;
    [SerializeField] private HealthIndicator healthIndicator;
    [SerializeField] private Bullet bullet;

    private Animator animator;
    private ICharacter characterData;
    private Transform curTransform;

    private Vector3 newPosition;
    private Vector3 newDirection;

    private float moveSpeed;

    public bool isMoving;

    private void Awake() {
        curTransform = transform;
        borders.parent = null;
        moveSpeed = 5.0f;

        animator = GetComponentInChildren<Animator>();
    }

    private void Start() {
        healthIndicator.CurrentHealth = characterData.Health;
    }

    public void Initialize(ICharacter _characterData)
    {
        characterData = _characterData;
    }

    private void Update()
    {
        // if(!characterData.isEnemy)
        // {
        Vector3 dist = newPosition - curTransform.position;

        if(dist.magnitude <= 0.15f)
        {
            curTransform.position = newPosition;
            isMoving = false;
            animator.SetBool("isMoving", false);
        }
        else
        {
            curTransform.position += dist.normalized * Time.deltaTime * moveSpeed;
            isMoving = true;
            animator.SetBool("isMoving", true);
        }

        curTransform.LookAt(newDirection);//TODO: сделать плавный поворот
        //}

        if(healthIndicator.CurrentHealth != characterData.Health)
            healthIndicator.ChangeHealthText(characterData.Health);
    }

    private void SetPositionFromData()
    {
        Vector2 pos = characterData.GetPosition();
        Vector2 dir = characterData.GetDirection();

        //curTransform.position = new Vector3(pos.x, 0, pos.y);
        newPosition = new Vector3(pos.x, 0, pos.y);
        newDirection = new Vector3(dir.x, 0, dir.y);
        curTransform.LookAt(newDirection);

    }

    public void Synchronize()
    {
        if(!characterData.isEnemy)
        {
            characterData.SetPosition(GetVector2Position());
            characterData.SetDirection(GetDirection());
        }

        SetPositionFromData();

        if(borders.gameObject.activeInHierarchy)
        {
            borders.parent = curTransform;
            borders.position = new Vector3(curTransform.position.x, borders.position.y, curTransform.position.z);
        }

        //Debug.Log(newDirection)
    }

    public void EndRound(BattleState state)
    {
        if(state == BattleState.FigthPlanning)
        {
            bullet.SpawnBullet(newDirection);
            animator.SetTrigger("Shoot");

            if(borders.gameObject.activeInHierarchy)
            {
                borders.parent = null;
                borders.position = new Vector3(curTransform.position.x, borders.position.y, curTransform.position.z);
            }
        }

        if(!borders.gameObject.activeInHierarchy)
        {
            borders.gameObject.SetActive(true);
            borders.position = new Vector3(curTransform.position.x, borders.position.y, curTransform.position.z);
        }

        if(characterData.Health <= 0)
        {
            borders.parent = curTransform;
            animator.SetTrigger("Die");
        }

    }

    public void MoveTo(Vector2 point)
    {
        Vector2 newPosition2 = PositionChecker.CheckPosition(characterData.GetPosition(), point, 15, 5);

        newPosition = new Vector3(newPosition2.x, 0, newPosition2.y);
    }

    public void RotateTo(Vector2 point)
    {
        newDirection = new Vector3(point.x, 0, point.y);
    }


    private Vector2 GetVector2Position()
    {
        return new Vector2(newPosition.x, newPosition.z);
    }

    private Vector2 GetDirection()
    {
        return new Vector2(newDirection.x, newDirection.z);
    }

    public bool UnitReady()
    {
        //Debug.Log($"is moving: {isMoving}");
        return !isMoving;
    }

    private void OnDestroy() {
        if(borders != null)
            Destroy(borders.gameObject);
    }
}
