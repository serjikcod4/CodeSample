using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    public bool isResetAllVar;
    public bool isCanMoveCamera;

    private GameObject gameObjectPlayer;
    private Vector3 difference;
    private BlockCreatorContoller blockCreatorContoller;
    private BlockResetTransform blockResetTransform;
    private CreatePlatformsController createPlatformsController;
    private BlockInstantiate blockInstantiate;
    private Vector3 summ;
    private float offsetX = 2f;

    private void Start()
    {
        createPlatformsController = FindObjectOfType<CreatePlatformsController>();
        blockResetTransform = FindObjectOfType<BlockResetTransform>();
        blockCreatorContoller = FindObjectOfType<BlockCreatorContoller>();
        blockInstantiate = FindObjectOfType<BlockInstantiate>();
        gameObjectPlayer = GameObject.FindGameObjectWithTag("Player");

        difference = transform.position - gameObjectPlayer.transform.position;
        difference = new Vector3(difference.x + offsetX, difference.y, difference.z);  
    }

    private void LateUpdate()
    {
        summ = gameObjectPlayer.transform.position + difference;
        if (isCanMoveCamera)
        {
            transform.position = Vector3.MoveTowards(transform.position, gameObjectPlayer.transform.position + difference, 4 * Time.deltaTime);                   
        }

        //Reset all Touch bool variables in BlockCreatoController script
        if(transform.position == gameObjectPlayer.transform.position + difference && isResetAllVar)           
        {
            blockResetTransform.isResetTransform = true;
            isCanMoveCamera = false;
            blockCreatorContoller.isTapUPFirst = false;
            isResetAllVar = false;
        }
    }
}