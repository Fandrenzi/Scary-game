    using UnityEngine;
    using UnityEngine.InputSystem.Processors;
    using UnityEngine.UIElements;
    using UnityEngine.SceneManagement;

    public class Monster : MonoBehaviour
    {
        public Transform Player;
        public float teleportDistance = 10f;
        public float teleportCooldown = 5f;
        public float returnCooldown = 10f;

        public float chaseProbability = 0.65f;

        public float rotationSpeed = 5f;

        private Vector3 baseTeleportSpot;
        private float teleportTimer;
        private bool returningToBase;
        public AudioClip teleportSound;
        public AudioClip SpawnSound;
        private AudioSource audioSource;

        public GameObject staticObject;
        public float staticActivationRange = 5f;

        public string DeathScene = "DeadScene";
        public float sceneChangeRange = 3.0f;
        public float timetoChangeScene = 5f;
        private float proximityTimer = 0f;  


         void Start()
         {
            baseTeleportSpot = transform.position;
            teleportTimer = teleportCooldown;

            audioSource = GetComponent<AudioSource>();
            if (audioSource == null )
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }

            audioSource.clip = teleportSound;

            if (staticObject != null)
            {
                staticObject.SetActive(false);
            }



         }

        // Update is called once per frame
        void Update()
        {
            if (Player == null)
            {
                return;
            }
            teleportTimer -= Time.deltaTime;

            if (teleportTimer <= 0f)
            {
                if (returningToBase)
                {
                    teleportToBaseSpot();
                    teleportTimer = returnCooldown;
                    returningToBase = false;
                }
                else
                {
                    DecideTeleportAction();
                    teleportTimer = teleportCooldown;
                }
            }

            RotateTowardsPlayer();

            float distanceToPlayer = Vector3.Distance(transform.position, Player.position);
            if (distanceToPlayer <= staticActivationRange)
            {
                if (staticObject != null && !staticObject.activeSelf)
                {
                    staticObject.SetActive(true);
                }
            }
            else
            {
                if (staticObject != null && staticObject.activeSelf)
                {
                    staticObject.SetActive(false);
                }


            }

            if (distanceToPlayer <= sceneChangeRange)
            {
            
            proximityTimer += Time.deltaTime;


            if (proximityTimer >= timetoChangeScene) 
            {
                SceneManager.LoadScene(DeathScene); 
            }

            }
        else
        {
            proximityTimer = 0f;
        }
    }


        private void DecideTeleportAction()
        {
            float randomValue = Random.value;

            if (randomValue <= chaseProbability)
            {
                TeleportNearPlayer();
            }
            else
            {
                teleportToBaseSpot();
            }
        }

        private void TeleportNearPlayer()
        {
            Vector3 randomPosition = Player.position + Random.onUnitSphere * teleportDistance;
            randomPosition.y = transform.position.y;
            transform.position = randomPosition;

            audioSource.PlayOneShot(teleportSound);
        }

        private void teleportToBaseSpot()
        {
            transform.position = baseTeleportSpot;
            returningToBase = true;

            audioSource.PlayOneShot(SpawnSound);
        }

        private void RotateTowardsPlayer()
        {
            Vector3 directionToPlayer = Player.position - transform.position;
            directionToPlayer.y = 0f;

            if (directionToPlayer != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
