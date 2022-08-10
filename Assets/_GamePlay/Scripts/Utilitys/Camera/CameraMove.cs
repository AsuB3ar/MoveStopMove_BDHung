using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraPosition
{
    MainMenu = 0,
    Gameplay = 1,
    ShopSkin = 2,
    ShopWeapon = 3,
}
public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    public readonly Vector3 MainMenuPosition = new Vector3(0, 1.5f, 3);
    public readonly Vector3 MainMenuRotation = new Vector3(15, 175, 0);

    public readonly Vector3 GameplayPosition = new Vector3(0.05f, 8.9f, -6.914f);
    public readonly Vector3 GameplayRotation = new Vector3(45, 0, 0);

    public readonly Vector3 ShopSkinPosition = new Vector3(0, 1.26f, 1.79f);
    public readonly Vector3 ShopSkinRotation = new Vector3(15, 180, 0);

    public readonly Vector3 ShopWeaponPosition = new Vector3(0.05f, 2.44f, 3.43f);
    public readonly Vector3 ShopWeaponRotation = new Vector3(30, 180, 0);



    private float speed = 2f;
    private Vector3 targetPos;
    private Vector3 targetRot;
    private bool IsReachDestination = true;
    private bool IsReachRotation = true;
    float rate = 0;

    // Update is called once per frame
    void Update()
    {
        if (!IsReachDestination)
        {
            Move();
        }

        if (!IsReachRotation)
        {
            Rotate();
        }
        
    }

    private void Move()
    {
        Vector3 newPos = Vector3.Lerp(transform.localPosition, targetPos, speed * Time.deltaTime);
        if((transform.localPosition - newPos).sqrMagnitude < 0.0000001f)
        {
            IsReachDestination = true;
        }
        transform.localPosition = newPos;
    }

    private void Rotate()
    {
        Vector3 newRot = Vector3.Lerp(transform.localRotation.eulerAngles, targetRot, speed * Time.deltaTime);
        if((transform.localRotation.eulerAngles - newRot).sqrMagnitude < 0.0000001f)
        {
            IsReachRotation = true;
        }
        transform.localRotation = Quaternion.Euler(newRot);
    }

    public void MoveTo(CameraPosition position)
    {
        IsReachDestination = false;
        IsReachRotation = false;

        if(position == CameraPosition.MainMenu)
        {
            targetPos = MainMenuPosition;
            targetRot = MainMenuRotation;
            speed = 2;
        }
        else if(position == CameraPosition.Gameplay)
        {
            targetPos = GameplayPosition;
            targetRot = GameplayRotation;
            speed = 6;
        }
        else if(position == CameraPosition.ShopSkin)
        {
            targetPos = ShopSkinPosition;
            targetRot = ShopSkinRotation;
            speed = 4;
        }
        else if(position == CameraPosition.ShopWeapon)
        {
            targetPos = ShopWeaponPosition;
            targetRot = ShopWeaponRotation;
            speed = 4;
        }
    }
}
