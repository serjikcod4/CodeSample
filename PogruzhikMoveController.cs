using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PogruzhikMoveController : MonoBehaviour {

    public bool isStartPosition;
    public bool isCanMovePogruzhicktoCameraCenter;
    public bool isMoveToPosRight;
    public bool isMoveToPosLeft;
    public Transform PosCenter;
    public Transform PosLeft;
    public Transform PosRight;
    public GameObject Pogruzhik_sprite;
    public Transform CameMain;

    private BlockCreatorContoller blockCreatorContoller;
    private float startPosY = 4.5f;
    private float speedPogruzchik = 5;
    private bool isMoveToPosCenter;
   
    private void Start()
    {
        blockCreatorContoller = FindObjectOfType<BlockCreatorContoller>();
    }

    private void Update()
    {
        if (isStartPosition)
        {
            transform.position -= new Vector3(0, 0.05f, 0);
            if (transform.position.y <= startPosY)
            {
                isStartPosition = false;
                isMoveToPosRight = true;
                blockCreatorContoller.isCanChangeBlockScale = true;
            }
        }

        if (isMoveToPosRight)
        {
            Pogruzhik_sprite.transform.position = Vector3.MoveTowards(Pogruzhik_sprite.transform.position, PosRight.position, Time.deltaTime * 2);
            if (Pogruzhik_sprite.transform.position.x >= PosRight.position.x)
            {
                isMoveToPosRight = false;
                isMoveToPosLeft = true;
            }
        }

        if (isMoveToPosLeft)
        {
            Pogruzhik_sprite.transform.position = Vector3.MoveTowards(Pogruzhik_sprite.transform.position, PosLeft.position, Time.deltaTime * 2);
            if (Pogruzhik_sprite.transform.position.x <= PosLeft.position.x)
            {
                isMoveToPosLeft = false;
                isMoveToPosRight = true;
            }
        }

        //Move Pogruzchik to Center
        if (isCanMovePogruzhicktoCameraCenter)
        {
            transform.position = Vector3.Lerp(transform.position,
                                                new Vector3(CameMain.position.x,
                                                            transform.position.y,
                                                            transform.position.z),
                                                Time.deltaTime * speedPogruzchik);
        }
    }

    public void MovePogruzhiktoStartPositionY()
    {
        isStartPosition = true;
    }
}
