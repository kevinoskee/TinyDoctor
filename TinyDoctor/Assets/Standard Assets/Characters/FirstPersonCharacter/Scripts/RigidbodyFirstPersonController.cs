using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using System.IO;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof (Rigidbody))]
    [RequireComponent(typeof (CapsuleCollider))]
    public class RigidbodyFirstPersonController : MonoBehaviour
    {
        public AudioClip colEnemy;
        public AudioClip die;
        public AudioClip coinCollect;
        public AudioClip heartCollect;
        private AudioSource source;

        public Camera cam;
        public MovementSettings movementSettings = new MovementSettings();
        public MouseLook mouseLook = new MouseLook();
        public AdvancedSettings advancedSettings = new AdvancedSettings();


        private Rigidbody m_RigidBody;
        private CapsuleCollider m_Capsule;
        private float m_YRotation;
        private Vector3 m_GroundContactNormal;
        private bool m_Jump, m_PreviouslyGrounded, m_Jumping, m_IsGrounded;

        [HideInInspector]
        public Vector2 RunAxis;
        [HideInInspector]
        public bool JumpAxis;
        public int totalCoins = 0;
        public GameObject Boss;


        public float health = 100f;
        public float oxygen = 100f;
        public Image HealthBar;
        public Image OxygenBar;
        public Text Coin;

        public GameObject GameOverUI;
        public AudioSource Mainsource;

        string path;
        string jsonString;
        Doctor doctor;

        void OnCollisionEnter(Collision collision)
        {

            switch (collision.gameObject.tag)
            {
                case "Target":
                    float force = 100;
                    // Calculate Angle Between the collision point and the player
                    Vector3 dir = collision.contacts[0].point - transform.position;
                    // We then get the opposite (-Vector3) and normalize it
                    dir = -dir.normalized;
                    // And finally we add force in the direction of dir and multiply it by force. 
                    // This will push back the player
                    JumpAxis = true;
                    m_RigidBody.AddForce(dir * force, ForceMode.Impulse);


                    health -= 10f;

                    if (health <= 0)
                    {
                        Die();
                    }
                    else
                        source.PlayOneShot(colEnemy);
                    break;
                case "Coin":
                    totalCoins += 10;
                    source.PlayOneShot(coinCollect);
                    break;
                case "Heart":
                    if(health+10f <= 100f)
                        health += 10f;
                    source.PlayOneShot(heartCollect);
                    break;
            }
           
        }



        void Die()
        {
            enabled = false;
            source.PlayOneShot(die);
            GameOverUI.SetActive(true);
            Time.timeScale = 0f;
            Mainsource.Stop();
        }

        [Serializable]
        public class MovementSettings
        {
            public float ForwardSpeed = 8.0f;   // Speed when walking forward
            public float BackwardSpeed = 4.0f;  // Speed when walking backwards
            public float StrafeSpeed = 4.0f;    // Speed when walking sideways
            public float RunMultiplier = 2.0f;   // Speed when sprinting
	        public KeyCode RunKey = KeyCode.LeftShift;
            public float JumpForce = 30f;
            public AnimationCurve SlopeCurveModifier = new AnimationCurve(new Keyframe(-90.0f, 1.0f), new Keyframe(0.0f, 1.0f), new Keyframe(90.0f, 0.0f));
            [HideInInspector] public float CurrentTargetSpeed = 8f;


#if !MOBILE_INPUT
            private bool m_Running;
#endif

            public void UpdateDesiredTargetSpeed(Vector2 input)
            {
	            if (input == Vector2.zero) return;
				if (input.x > 0 || input.x < 0)
				{
					//strafe
					CurrentTargetSpeed = StrafeSpeed;
				}
				if (input.y < 0)
				{
					//backwards
					CurrentTargetSpeed = BackwardSpeed;
				}
				if (input.y > 0)
				{
					//forwards
					//handled last as if strafing and moving forward at the same time forwards speed should take precedence
					CurrentTargetSpeed = ForwardSpeed;
				}
#if !MOBILE_INPUT
	            if (Input.GetKey(RunKey))
	            {
		            CurrentTargetSpeed *= RunMultiplier;
		            m_Running = true;
	            }
	            else
	            {
		            m_Running = false;
	            }
#endif
            }

#if !MOBILE_INPUT
            public bool Running
            {
                get { return m_Running; }
            }
#endif
        }


        [Serializable]
        public class AdvancedSettings
        {
            public float groundCheckDistance = 0.01f; // distance for checking if the controller is grounded ( 0.01f seems to work best for this )
            public float stickToGroundHelperDistance = 0.5f; // stops the character
            public float slowDownRate = 20f; // rate at which the controller comes to a stop when there is no input
            public bool airControl; // can the user control the direction that is being moved in the air
            [Tooltip("set it to 0.1 or more if you get stuck in wall")]
            public float shellOffset; //reduce the radius by that ratio to avoid getting stuck in wall (a value of 0.1f is nice)
        }

        public Vector3 Velocity
        {
            get { return m_RigidBody.velocity; }
        }

        public bool Grounded
        {
            get { return m_IsGrounded; }
        }

        public bool Jumping
        {
            get { return m_Jumping; }
        }

        public bool Running
        {
            get
            {
 #if !MOBILE_INPUT
				return movementSettings.Running;
#else
	            return false;
#endif
            }
        }


        private void Start()
        {

            path = Application.streamingAssetsPath + "/Doctor.json";
            jsonString = File.ReadAllText(path);
            doctor = JsonUtility.FromJson<Doctor>(jsonString);
            totalCoins = doctor.Coins;


            m_RigidBody = GetComponent<Rigidbody>();
            m_Capsule = GetComponent<CapsuleCollider>();
            mouseLook.Init (transform, cam.transform);
            source = GetComponent<AudioSource>();
        }


        private void Update()
        {
            RotateView();

            if (JumpAxis && !m_Jump)
            {
                m_Jump = true;
            }

            UpdateHealthBar();
            UpdateOxygenBar();
            UpdateCoins();

            if (!Boss.activeSelf)
            {
                UpdateDoctor();
                enabled = false;
            }
        }

        void UpdateDoctor()
        {
            doctor.Coins = totalCoins;
            string newDoctor = JsonUtility.ToJson(doctor,true);
            File.WriteAllText(path, newDoctor);
             
        }

        void UpdateHealthBar()
        {
            HealthBar.fillAmount = Mathf.Lerp(HealthBar.fillAmount, health / 100f, 0.5f);
        }

        void UpdateOxygenBar()
        {
            if(oxygen > 0)
            {
                oxygen -= 1 * Time.deltaTime;
                OxygenBar.fillAmount = Mathf.Lerp(OxygenBar.fillAmount, oxygen / 100f, 0.5f);
            }
            if (oxygen <= 0)
                Die();
        }

        void UpdateCoins()
        {
            Coin.text = totalCoins.ToString();
        }

        private void FixedUpdate()
        {
            GroundCheck();
            Vector2 input = GetInput();

            if ((Mathf.Abs(input.x) > float.Epsilon || Mathf.Abs(input.y) > float.Epsilon) && (advancedSettings.airControl || m_IsGrounded))
            {
                // always move along the camera forward as it is the direction that it being aimed at
                Vector3 desiredMove = cam.transform.forward*input.y + cam.transform.right*input.x;
                desiredMove = Vector3.ProjectOnPlane(desiredMove, m_GroundContactNormal).normalized;

                desiredMove.x = desiredMove.x*movementSettings.CurrentTargetSpeed;
                desiredMove.z = desiredMove.z*movementSettings.CurrentTargetSpeed;
                desiredMove.y = desiredMove.y*movementSettings.CurrentTargetSpeed;
                if (m_RigidBody.velocity.sqrMagnitude <
                    (movementSettings.CurrentTargetSpeed*movementSettings.CurrentTargetSpeed))
                {
                    m_RigidBody.AddForce(desiredMove*SlopeMultiplier(), ForceMode.Impulse);
                }
            }

            if (m_IsGrounded)
            {
                m_RigidBody.drag = 5f;

                if (m_Jump)
                {
                    m_RigidBody.drag = 0f;
                    m_RigidBody.velocity = new Vector3(m_RigidBody.velocity.x, 0f, m_RigidBody.velocity.z);
                    m_RigidBody.AddForce(new Vector3(0f, movementSettings.JumpForce, 0f), ForceMode.Impulse);
                    m_Jumping = true;
                }

                if (!m_Jumping && Mathf.Abs(input.x) < float.Epsilon && Mathf.Abs(input.y) < float.Epsilon && m_RigidBody.velocity.magnitude < 1f)
                {
                   // m_RigidBody.Sleep();
                }
            }
            else
            {
                m_RigidBody.drag = 0f;
                if (m_PreviouslyGrounded && !m_Jumping)
                {
                    StickToGroundHelper();
                }
            }
            m_Jump = false;
        }


        private float SlopeMultiplier()
        {
            float angle = Vector3.Angle(m_GroundContactNormal, Vector3.up);
            return movementSettings.SlopeCurveModifier.Evaluate(angle);
        }


        private void StickToGroundHelper()
        {
            RaycastHit hitInfo;
            if (Physics.SphereCast(transform.position, m_Capsule.radius * (1.0f - advancedSettings.shellOffset), Vector3.down, out hitInfo,
                                   ((m_Capsule.height/2f) - m_Capsule.radius) +
                                   advancedSettings.stickToGroundHelperDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                if (Mathf.Abs(Vector3.Angle(hitInfo.normal, Vector3.up)) < 85f)
                {
                    m_RigidBody.velocity = Vector3.ProjectOnPlane(m_RigidBody.velocity, hitInfo.normal);
                }
            }
        }


        private Vector2 GetInput()
        {
            
            Vector2 input = new Vector2
                {
                    x = RunAxis.x,
                    y = RunAxis.y
                };
			movementSettings.UpdateDesiredTargetSpeed(input);
            return input;
        }


        private void RotateView()
        {
            //avoids the mouse looking if the game is effectively paused
            if (Mathf.Abs(Time.timeScale) < float.Epsilon) return;

            // get the rotation before it's changed
            float oldYRotation = transform.eulerAngles.y;

            mouseLook.LookRotation (transform, cam.transform);

            if (m_IsGrounded || advancedSettings.airControl)
            {
                // Rotate the rigidbody velocity to match the new direction that the character is looking
                Quaternion velRotation = Quaternion.AngleAxis(transform.eulerAngles.y - oldYRotation, Vector3.up);
                m_RigidBody.velocity = velRotation*m_RigidBody.velocity;
            }
        }

        /// sphere cast down just beyond the bottom of the capsule to see if the capsule is colliding round the bottom
        private void GroundCheck()
        {
            m_PreviouslyGrounded = m_IsGrounded;
            RaycastHit hitInfo;
            if (Physics.SphereCast(transform.position, m_Capsule.radius * (1.0f - advancedSettings.shellOffset), Vector3.down, out hitInfo,
                                   ((m_Capsule.height/2f) - m_Capsule.radius) + advancedSettings.groundCheckDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                m_IsGrounded = true;
                m_GroundContactNormal = hitInfo.normal;
            }
            else
            {
                m_IsGrounded = false;
                m_GroundContactNormal = Vector3.up;
            }
            if (!m_PreviouslyGrounded && m_IsGrounded && m_Jumping)
            {
                m_Jumping = false;
            }
        }
    }



    [Serializable]
    public class Doctor
    {
        public int Coins;
        public bool[] Cards;
    }
}
