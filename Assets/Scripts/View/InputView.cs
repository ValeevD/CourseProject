using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputView : MonoBehaviour
{
    [SerializeField] private PlayerManagerProvider playerManager;

    //private CharacterProvider currentUnit;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if(playerManager.freeze)
            return;

        if(Input.GetMouseButtonDown(1))
        {
            if(playerManager.CurrentUnit == null)
                return;

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit = new RaycastHit();

            if (!Physics.Raycast(ray, out raycastHit, 100))
                return;


            if(Input.GetKey(KeyCode.LeftShift))
            {
                playerManager.CurrentUnit.RotateTo(new Vector2(raycastHit.point.x, raycastHit.point.z));
            }
            else
            {
                playerManager.CurrentUnit.MoveTo(new Vector2(raycastHit.point.x, raycastHit.point.z));
            }
        }
    }
}
