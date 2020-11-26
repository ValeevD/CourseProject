using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Transform bulletHole;
    [SerializeField] private Transform unitTransform;

    private float speed;
    private Transform currentTransform;
    private Vector3 direction;
    private float lifeTime;

    private float lifeTimeRemains;
    private bool muted;

    private void OnEnable() {
        lifeTime = 1;
        speed = 40;
        currentTransform = transform;

        muted = true;
    }

    // private void Awake()
    // {
    //     lifeTime = 1;
    //     speed = 40;
    //     currentTransform = transform;

    //     muted = true;
    // }

    void Update()
    {
        //Debug.Log(muted);

        if(muted)
            return;

        if(lifeTimeRemains < 0){
            HideBullet();
            return;
        }

        currentTransform.position += direction.normalized * Time.deltaTime * speed;

        lifeTimeRemains -= Time.deltaTime;
    }

    private void HideBullet()
    {
        muted = true;
        currentTransform.parent = bulletHole;
        currentTransform.localPosition = Vector3.zero;
        gameObject.SetActive(false);

    }

    public void SpawnBullet(Vector3 _direction)
    {
        lifeTimeRemains = lifeTime;
        gameObject.SetActive(true);

        muted = false;
        currentTransform.parent = null;
        direction = _direction - unitTransform.position;

        direction.y = 0;

    }
}
