using CustomQuest;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using System;
using System.Collections;
using UnityEngine.Events;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
using UnityStandardAssets.Characters.FirstPerson;

/// <summary>
/// Contains a sample player logic to:
/// Move with arrow keys or "WASD".
/// Opening and using the quest wheel.
/// Clicking on a quest giver
/// And picking up an item
/// </summary>
/// 
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class CQExamplePlayer : MonoBehaviour
{
    #region Private Members

    private Animator _animator;

    private CharacterController m_characterController;


    private InventoryItemBase mCurrentItem = null;

    private bool mCanTakeDamage = true;
    [SerializeField] private MouseLook m_MouseLook;
    private Camera m_Camera;
    [SerializeField] private bool m_UseFovKick;
    [SerializeField] private FOVKick m_FovKick = new FOVKick();
    [SerializeField] private bool m_UseHeadBob;
    [SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
    [SerializeField] private LerpControlledBob m_JumpBob = new LerpControlledBob();
    private Vector3 m_OriginalCameraPosition;
    [SerializeField] private bool m_IsWalking;
    [SerializeField] private float m_WalkSpeed;
    [SerializeField] private float m_RunSpeed;

    [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
    [SerializeField] private float m_JumpSpeed;
    [SerializeField] private float m_StickToGroundForce;
    [SerializeField] private float m_GravityMultiplier;
    [SerializeField] private float m_StepInterval;
    [SerializeField] private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.
    [SerializeField] private AudioClip m_JumpSound;           // the sound played when character leaves the ground.
    [SerializeField] private AudioClip m_LandSound;           // the sound played when character touches back on ground.


    #endregion

    #region Public Members

    public Inventory Inventory;

    public GameObject Hand;

    public HUD Hud;

    public event EventHandler PlayerDied;


    #endregion

    public UnityEvent QuestCompleted;
    private bool m_Jump;
    private float m_YRotation;
    private Vector2 m_Input;
    private Vector3 m_MoveDir = Vector3.zero;
    [SerializeField] private CharacterController m_CharacterController;
    private CollisionFlags m_CollisionFlags;
    private bool m_PreviouslyGrounded;
    private float m_StepCycle;
    private float m_NextStep;
    private bool m_Jumping;
    private AudioSource m_AudioSource;
    public float resources;
    #region Field

    /// <summary>
    /// The dmg this player does
    /// </summary>
    [SerializeField]
    private int damage = 25;

    /// <summary>
    /// The dmg this player does
    /// </summary>
    public int Damage { get { return damage; } set { damage = value; } }

    public float movementSpeed;

    public float rotationSpeed;


    /// <summary>
    /// Float for resources, use your own resources here.
    /// </summary>

    /// <summary>
    /// A list of the items this player has picked up
    /// </summary>
    public List<Item> items = new List<Item>();

    /// <summary>
    /// Whether this player is currently attacking or not
    /// </summary>
    public bool attacking;

    /// <summary>
    /// The timer, used for attacking
    /// </summary>
    private float attackTimer;

    private CQPlayerObject cQPlayer;

    #endregion Field

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        QuestHandler.StartListening("GiveReward", GetReward);
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled() or inactive.
    /// </summary>
    private void OnDisable()
    {
        QuestHandler.StopListening("GiveReward", GetReward);
    }

    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void Start()
    {
        cQPlayer = GetComponent<CQPlayerObject>();
        _animator = GetComponent<Animator>();
        m_characterController = GetComponent<CharacterController>();
        m_Camera = Camera.main;
        m_MouseLook.Init(transform, m_Camera.transform);
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>


    void FixedUpdate()
    {

        float speed;
        GetInput(out speed);
        // always move along the camera forward as it is the direction that it being aimed at
        Vector3 desiredMove = transform.forward * m_Input.y + transform.right * m_Input.x;

        // get a normal for the surface that is being touched to move along it
        RaycastHit hitInfo;
        Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                           m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
        desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

        m_MoveDir.x = desiredMove.x * speed;
        m_MoveDir.z = desiredMove.z * speed;


        if (m_CharacterController.isGrounded)
        {
            m_MoveDir.y = -m_StickToGroundForce;

            if (m_Jump)
            {
                m_MoveDir.y = m_JumpSpeed;
                m_Jump = false;
                m_Jumping = true;
            }
        }
        else
        {
            m_MoveDir += Physics.gravity * m_GravityMultiplier * Time.fixedDeltaTime;
        }
        m_CollisionFlags = m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);

        ProgressStepCycle(speed);
        UpdateCameraPosition(speed);

        m_MouseLook.UpdateCursorLock();
    }
    public bool mIsControlEnabled = true;
    private void GetInput(out float speed)
    {
        // Read input
        float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        float vertical = CrossPlatformInputManager.GetAxis("Vertical");

        bool waswalking = m_IsWalking;

#if !MOBILE_INPUT
        // On standalone builds, walk/run speed is modified by a key press.
        // keep track of whether or not the character is walking or running
        m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
#endif
        // set the desired speed to be walking or running
        speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
        m_Input = new Vector2(horizontal, vertical);

        // normalize input if it exceeds 1 in combined length:
        if (m_Input.sqrMagnitude > 1)
        {
            m_Input.Normalize();
        }

        // handle speed change to give an fov kick
        // only if the player is going to a run, is running and the fovkick is to be used
        if (m_IsWalking != waswalking && m_UseFovKick && m_characterController.velocity.sqrMagnitude > 0)
        {
            StopAllCoroutines();
            StartCoroutine(!m_IsWalking ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
        }
    }
    public void EnableControl()
    {
        mIsControlEnabled = true;
    }

    public void DisableControl()
    {
        mIsControlEnabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (attacking)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                attacking = false;
            }
        }
        else if (Input.GetKey(KeyCode.F))
        {
            attacking = true;
            attackTimer = 1.2f;
            //GetComponent<Animator>().SetTrigger("Attack");
        }
        if (mIsControlEnabled)
        {
            // Interact with the ite

            // Execute action with item
            if (mCurrentItem != null && Input.GetMouseButtonDown(0))
            {
                // Dont execute click if mouse pointer is over uGUI element
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    // TODO: Logic which action to execute has to come from the particular item
                    _animator.SetTrigger("attack_1");
                }
            }

            RotateView();
            // the jump state needs to read here to make sure it is not missed
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

            if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
            {
                StartCoroutine(m_JumpBob.DoBobCycle());
                m_MoveDir.y = 0f;
                m_Jumping = false;
            }
            if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
            {
                m_MoveDir.y = 0f;
                _animator.SetBool("is_in_air", true);
            }

            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
            {
                _animator.SetBool("is_in_air", false);
                _animator.SetBool("run", true);
            }
            else
            {
                _animator.SetBool("run", false);
            }


            m_PreviouslyGrounded = m_CharacterController.isGrounded;
        }
    }

    private void ProgressStepCycle(float speed)
    {
        if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
        {
            m_StepCycle += (m_CharacterController.velocity.magnitude + (speed * (m_IsWalking ? 1f : m_RunstepLenghten))) *
                         Time.fixedDeltaTime;
        }

        if (!(m_StepCycle > m_NextStep))
        {
            return;
        }

        m_NextStep = m_StepCycle + m_StepInterval;

    }

    private void UpdateCameraPosition(float speed)
    {
        Vector3 newCameraPosition;
        if (!m_UseHeadBob)
        {
            return;
        }
        if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
        {
            m_Camera.transform.localPosition =
                m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude +
                                  (speed * (m_IsWalking ? 1f : m_RunstepLenghten)));
            newCameraPosition = m_Camera.transform.localPosition;
            newCameraPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset();
        }
        else
        {
            newCameraPosition = m_Camera.transform.localPosition;
            newCameraPosition.y = m_OriginalCameraPosition.y - m_JumpBob.Offset();
        }
        m_Camera.transform.localPosition = newCameraPosition;
    }

    /// <summary>
    /// Logic for picking up and item
    /// </summary>
    /// <param name="item">The item picked up by the player</param>
    public void pickUpItem(Item item)
    {
        foreach (SpawnZone zone in item.GetComponent<QuestObject>().criteria.spawnZones)
        {
            if (zone.spawnedObjects.Contains(item.gameObject))
            {
                zone.spawnedObjects.Remove(item.gameObject);
            }
        }
        if (item.GetComponent<QuestObject>().criteria.Quest.activeOptionalCriterias[cQPlayer].Contains(item.GetComponent<QuestObject>().criteria)) //Only picks up an item, if player is currently on the quest
        {
            items.Add(item);
            if (item.GetComponent<QuestObject>().criteria.type == criteriaType.Gather)
            {
                item.GetComponent<QuestObject>().criteria.Progress(cQPlayer, this);
            }
        }

        if (item.GetComponent<QuestObject>().criteria.Quest.activeCriterias[cQPlayer].Contains(item.GetComponent<QuestObject>().criteria)) //Only picks up an item, if player is currently on the quest
        {
            items.Add(item);
            if (item.GetComponent<QuestObject>().criteria.type == criteriaType.Gather)
            {
                item.GetComponent<QuestObject>().criteria.Progress(cQPlayer, this);
            }
        }
        item.gameObject.SetActive(false);
    }


    /// <summary>
    /// An example method for handling rewards
    /// </summary>
    /// <param name="info">Holds the info about the reward</param>
    private void GetReward(EventInfoHolder info)
    {
        switch (info.reward.type)
        {
            case rewardType.Resource:
                this.resources += info.reward.amount;
                break;

            case rewardType.Item:
                for (int i = 0; i < info.reward.amount; i++)
                {
                    //This will just spawn the reward on the ground. If you wish to give it direcly, this is where you add that
                    Instantiate(info.reward.rewardObject, this.transform.position, this.transform.rotation);
                }
                break;
        }
    }

    private void RotateView()
    {
        m_MouseLook.LookRotation(transform, m_Camera.transform);
    }
}