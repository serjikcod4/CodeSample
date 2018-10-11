using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCreatorContoller : MonoBehaviour {

    public bool isCanChangeBlockScale;
    public bool isTapUPFirst;

    private GameObject block;
    private GameObject block_GO;
    private GameObject[] blocks;
    private Rigidbody block_Rigidbody;
    private PogruzhikMoveController pogruzhikMoveController;
    private BlockFlyingTimer blockFlyingTimer;
    private float timer;
    private float delay = 0.2f;
    private float recharge;
    private float maxBlockScale = 6f;
    private float minBlockScale = 1f;
    private float minusDelta = 1f;  
    private bool isTapUPSecond;
    private bool isMaxBlockScale;
    private bool isMax;
    private bool isMin;



    private void Start()
    {
        block = GameObject.FindGameObjectWithTag("block");
        block_GO = GameObject.FindGameObjectWithTag("block_GO");
        block_Rigidbody = block_GO.GetComponent<Rigidbody>();
        pogruzhikMoveController = FindObjectOfType<PogruzhikMoveController>();
        blockFlyingTimer = FindObjectOfType<BlockFlyingTimer>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
       
        //Cheking Second Tap Up
        if (Input.GetMouseButtonUp(0) && isTapUPFirst)
        {
            ActiveGravityAndParentOfBlock();
            pogruzhikMoveController.isMoveToPosLeft = false;
            pogruzhikMoveController.isMoveToPosRight = false;
            pogruzhikMoveController.isCanMovePogruzhicktoCameraCenter = false;
            blockFlyingTimer.isFly = true;
        }

        //BlockScale changing
        if (Input.GetMouseButton(0) && !isTapUPFirst  && isCanChangeBlockScale && timer>=recharge)
        {
            BlockScaleChanging();
            CheckMaxBLockScale();
            CheckMinBlockScale();
        }

        //Checking First Tap Up
        if (Input.GetMouseButtonUp(0) && isCanChangeBlockScale)
        {
            isTapUPFirst = true; 
        }  
    }


    private void BlockScaleChanging()
    {
        block_GO.transform.localScale += new Vector3(0.2f, 0.2f, 0) * minusDelta;
        recharge = timer + delay;
    }

    private void CheckMaxBLockScale()
    {
        if (block_GO.transform.localScale.x >= maxBlockScale && !isMax)
        {
            isMaxBlockScale = true;
            isMax = true;
            minusDelta = -1f;
        }
    }

    private void CheckMinBlockScale()
    {
        if (block_GO.transform.localScale.x <= minBlockScale &&  isMaxBlockScale)
        {
            isMax = false;
            isMaxBlockScale = false;
            minusDelta = 1f;
        }
    }

    private void ActiveGravityAndParentOfBlock()
    {
        block_GO.transform.parent = null;
        block_Rigidbody.useGravity = true;
    }

    public void ReferenceToBlock()
    {
        blocks = GameObject.FindGameObjectsWithTag("block_GO");
        block_GO = blocks[blocks.Length - 1];
        block_Rigidbody = block_GO.GetComponent<Rigidbody>();
    }
}
