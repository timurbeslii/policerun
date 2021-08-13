
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using RootMotion.Dynamics;
using static RootMotion.Dynamics.PuppetMaster;
using TMPro;

public class EventManager : MonoBehaviour
{

    private static EventManager _instance;
    public static EventManager eventManager { get { return _instance; } }

    public ParticleSystem poofFX;
   
    public MeshRenderer[] target_1_MeshRenderers;
    public MeshRenderer[] target_2_MeshRenderers;
    public MeshRenderer[] target_3_MeshRenderers;
    public MeshRenderer[] target_4_MeshRenderers;
    public MeshRenderer[] target_5_MeshRenderers;

    public Transform[] chainHoldPositions;
    public FixedJoint[] targetFixedJoints;
    
    public Transform[] criminalPositions;

    public int currentArrestedCount;
    public int maximumArrestCount;
    public int currentCriminalPos;

    public Animator playerAnimator;
    public PlayerMovement playerMovementScript;
    public TMP_Text handcuffAmountText;
    public Camera mainCamera;

    public TMP_Text badgeCountText;
   
    [Header("Drone Section")]
    private bool isOnDroneTile;
    public Transform DronePosition;
    public Transform DroneFlyPosition;
    public Transform DroneExitPosition;

    bool airCooldown;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        
    }
    public void OnPrisonReached()
    {
        GameManager.gm.finished = true;
        GameObject prison = GameObject.FindGameObjectWithTag("Prison");
        Transform door = prison.transform.parent.GetChild(0);
        Transform chainInPrisonPos = prison.transform.parent.GetChild(1);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.DORotate(new Vector3(0, 180, 0), 1);
        door.DOLocalRotate(new Vector3(0, -45, 0), 1).OnComplete(()=>
        {

            foreach (Transform transform in chainHoldPositions)
            {
                transform.parent = null;
                transform.DOMove(chainInPrisonPos.position, 1);
            }

        });

        GameManager.gm.ShowLevelEndUI(true);

        playerMovementScript.SlowDownThePlayer();
        DoRandomDance();
        //Camera.main.GetComponent<CameraFollow2>().GameFinishCam();

    }
    public void OnFinishStarted()
    {
        if (!GameManager.gm.finishReached)
        {
            GameManager.gm.finishReached = true;
            playerMovementScript.SpeedUpThePlayer();
        }
       
    }
    public void OnBadgeTrigger()
    {
        badgeCountText.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), .75f).OnComplete(()=>
        {
            badgeCountText.transform.DOScale(new Vector3(1f, 1f, 1f), .75f);
        });
        PlayerPrefs.SetInt("badgeCountCollected", PlayerPrefs.GetInt("badgeCountCollected") + 1 );
       // PlayerPrefs.SetInt("badgeCount", PlayerPrefs.GetInt("badgeCount") + 1);
        badgeCountText.text = PlayerPrefs.GetInt("badgeCountCollected").ToString();
    }
    private void DoRandomDance()
    {
        string animClipName = "dance";
        int index = Random.Range(1, 5);
        animClipName += index;

        playerAnimator.Play(animClipName);
    }
    public void OnTriggerDroneTile(){

        if (!airCooldown)
        {
         

            airCooldown = true;
            Invoke("ResetAirCooldown", 1);

            if (isOnDroneTile)
            {
                playerMovementScript.isOnDroneTile = false;
                isOnDroneTile = false;
                playerAnimator.SetBool("isHoldingDrone", false);
                DronePosition.DOLocalMove(DroneExitPosition.localPosition, .6f).SetEase(Ease.InBack);
                DronePosition.DOLocalRotateQuaternion(DroneExitPosition.localRotation, .6f).SetEase(Ease.InBack).OnComplete(() =>
                {

                });
            }
            else
            {
                
                isOnDroneTile = true;
                DronePosition.DOLocalMove(DroneFlyPosition.localPosition, 1).SetEase(Ease.InOutSine);
                DronePosition.DOLocalRotateQuaternion(DroneFlyPosition.localRotation, 1).SetEase(Ease.InOutSine).OnComplete(() =>
                {
                    playerMovementScript.isOnDroneTile = true;
                    playerAnimator.SetBool("isHoldingDrone", true);
                });


            }
        }
    }
    public void OnTriggerAirTile()
    {
        if (!airCooldown)
        {
            airCooldown = true;
            Invoke("ResetAirCooldown", 1);
          
            if (playerAnimator.GetBool("isOnAir"))
            {
                Debug.LogError("AIR SET TO FALSE");
                playerAnimator.SetBool("isOnAir", false);
            }
            else
            {
                Debug.LogError("AIR SET TO TRUE");
                playerAnimator.SetBool("isOnAir", true);

            }
        }
   
    }
    private void ResetAirCooldown()
    {
        airCooldown = false;
    }
    private void Start()
    {
        OnLevelStart();
    }
    public void OnObstacleTrigger()
    {
        mainCamera.DOShakeRotation(.25f, 1, 10, 90);
        if (currentArrestedCount <= 0)
        {
           
           
                playerMovementScript.SlowDownAndDie();

            
        }
        else
        {
            if (GameManager.gm.finishReached && currentArrestedCount == 1)
            {
                GameManager.gm.ShowLevelEndUI(true);
                GameManager.gm.finished = true;
                DoRandomDance();
                playerMovementScript.SlowDownThePlayer();
                Camera.main.GetComponent<CameraFollow2>().GameFinishCam();
                poofFX.Play();
                currentCriminalPos -= 2;

                if (criminalPositions[currentCriminalPos].gameObject.activeInHierarchy)
                {
                    criminalPositions[currentCriminalPos].gameObject.SetActive(false);
                }
                if (criminalPositions[currentCriminalPos + 1].gameObject.activeInHierarchy)
                {
                    criminalPositions[currentCriminalPos + 1].gameObject.SetActive(false);
                }
                AppearTargetChains(currentArrestedCount, false);
                currentArrestedCount--;
                UpdateHandcuffAmountText();
            }
            else
            {


                playerMovementScript.PlayerCrashed();
                poofFX.Play();
                currentCriminalPos -= 2;

                if (criminalPositions[currentCriminalPos].gameObject.activeInHierarchy)
                {
                    criminalPositions[currentCriminalPos].gameObject.SetActive(false);
                }
                if (criminalPositions[currentCriminalPos + 1].gameObject.activeInHierarchy)
                {
                    criminalPositions[currentCriminalPos + 1].gameObject.SetActive(false);
                }
                AppearTargetChains(currentArrestedCount, false);
                currentArrestedCount--;
                UpdateHandcuffAmountText();
            }
        }
       
    }
    private void UpdateHandcuffAmountText()
    {
        handcuffAmountText.text = currentArrestedCount + "/" + maximumArrestCount;
        if (currentArrestedCount == maximumArrestCount)
        {
            handcuffAmountText.DOColor(Color.red, 1);
            handcuffAmountText.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), .75f).SetEase(Ease.OutBounce).OnComplete(()=>
            {
                handcuffAmountText.transform.DOScale(Vector3.one, .5f);
            });
        }
        else
        {
            handcuffAmountText.DOColor(Color.green, 1);
        }
    }
    public void StartTheGame()
    {
        playerMovementScript.StartWalking();
    }
    public void OnLevelStart()
    {
        currentArrestedCount = 0;
        currentCriminalPos = 0;
        maximumArrestCount = GameManager.gm.levels[PlayerPrefs.GetInt("levelIndex")].maximumArrestCount;
        
        
        #region Making Chains Invisible
        // Making Chains Invisible.
        for (int i = 1; i <= 5; i++)
        {
            AppearTargetChains(i,false);
        }
        #endregion

        #region Making Criminals Invisible

        foreach (var obj in criminalPositions)
        {
           obj.gameObject.SetActive(false);
        }
        #endregion
    }

    private void AppearTargetChains(int index,bool option)
    {
        if (index == 1)
        {
            foreach (var mr in target_1_MeshRenderers)
            {
                mr.enabled = option;
            }
        }else if (index == 2)
        {
            foreach (var mr in target_2_MeshRenderers)
            {
                mr.enabled = option;
            }
        }else if (index == 3)
        {
            foreach (var mr in target_3_MeshRenderers)
            {
                mr.enabled = option;
            }
        }
        else if (index == 4)
        {
            foreach (var mr in target_4_MeshRenderers)
            {
                mr.enabled = option;
            }
        }
        else if (index == 5)
        {
            foreach (var mr in target_5_MeshRenderers)
            {
                mr.enabled = option;
            }
        }
    }

    public void OnCriminalTrigger(Transform TriggeredCriminal)
    {
        
        if (currentArrestedCount + 1 <= maximumArrestCount)
        {
            
            poofFX.Play();
          
            TriggeredCriminal.gameObject.SetActive(false);
            if (TriggeredCriminal.CompareTag("FreeSlimCriminal")){

                criminalPositions[currentCriminalPos].gameObject.SetActive(true);
            }
            else if (TriggeredCriminal.CompareTag("FreeThickCriminal"))
            {
                criminalPositions[currentCriminalPos+1].gameObject.SetActive(true);
            }
            currentCriminalPos += 2;
         
            currentArrestedCount++;
            UpdateHandcuffAmountText();
            AppearTargetChains(currentArrestedCount,true);
            TriggeredCriminal.tag = "ArrestedCriminal";
            //   //Player still has the capacity to arrest new triggered criminal.
            //   
            //   currentArrestedCount++;
            //  
            //   TriggeredCriminal.GetChild(2).localPosition = criminalPositions[currentArrestedCount].localPosition;
            //   TriggeredCriminal.GetChild(2).localRotation = criminalPositions[currentArrestedCount].localRotation;
            //   TriggeredCriminal.GetChild(2).position = criminalPositions[currentArrestedCount].position;
            //   TriggeredCriminal.GetChild(2).rotation = criminalPositions[currentArrestedCount].rotation;
            //   PuppetMaster triggeredCriminalPuppetMaster = TriggeredCriminal.GetChild(0).GetComponent<PuppetMaster>();
            //   triggeredCriminalPuppetMaster.muscleWeight = 0.5f;
            //   triggeredCriminalPuppetMaster.pinWeight = 0;
            // //  DOTween.To(()=>triggeredCriminalPuppetMaster.pinWeight, x=> triggeredCriminalPuppetMaster.pinWeight = x, 0, 1);
            //   TriggeredCriminal.GetChild(2).GetComponent<Animator>().enabled = false;
            //   // TriggeredCriminal.GetChild(2).DOLocalMove(criminalPositions[currentArrestedCount].localPosition, .5f).OnComplete((() =>
            //   // {
            //   //     AppearTargetChains(currentArrestedCount,true);
            //   //     
            //   //     GameObject rightHand = TriggeredCriminal.GetChild(0).GetChild(TriggeredCriminal.GetChild(0).childCount-1).GetChild(2).GetChild(2).GetChild(0).GetChild(0).gameObject;
            //   //     targetFixedJoints[currentArrestedCount].connectedBody=rightHand.GetComponent<Rigidbody>();
            //   // }));
            //  
            //   AppearTargetChains(currentArrestedCount,true);
            //   GameObject rightHand = TriggeredCriminal.GetChild(0).GetChild(TriggeredCriminal.GetChild(0).childCount-1).GetChild(2).GetChild(2).GetChild(0).GetChild(0).gameObject;
            //   targetFixedJoints[currentArrestedCount].connectedBody=rightHand.GetComponent<Rigidbody>();

        }
        else
        {
            // If the player cannot arrest anymore criminals because he/she reached the limit(3 by default), Then new triggered Criminals will be considered as Obstacles.
            TriggeredCriminal.GetComponent<Animator>().Play("attack");
            TriggeredCriminal.GetComponent<BoxCollider>().enabled = false;

           
           
                OnObstacleTrigger();
            
          

            

        }
    }
}
