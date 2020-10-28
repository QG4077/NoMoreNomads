using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Nomads.Entities.Controllers
{
    public class BaseEntityController : MonoBehaviour
    {
        [Header("Movement Variables")]
        [Range(0, 1)] [SerializeField] public float JumpForce = 1f;
        [Range(0, 1)] [SerializeField] public float MoveSpeed = 1f;
        [Range(1, 10)] [SerializeField] public float AccelerationSpeed = 1f;
        [SerializeField] public bool isAwake = true;
        [SerializeField] public bool FacingRight = true;
        [SerializeField] public bool isGrounded = false;

        [Header("Control Options")]
        [SerializeField] public bool AirControl = false;

        [Header("Object references")]        
        public Rigidbody2D entity_RigidBody;
        public Transform GroundTransform;
        public Transform CeilingTransform;
        public Collider2D RightCollider;
        public Tilemap LevelMap;

        [HideInInspector] public Vector2 CurrentVelocity = Vector2.zero;
        [HideInInspector] public const float GroundCheckRadius = 0.2f;

        public virtual void Start() {
            entity_RigidBody = GetComponent<Rigidbody2D>();
        }

        public virtual void Update()
        {

        }

        public virtual void FixedUpdate()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundTransform.position, GroundCheckRadius);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    isGrounded = true;
            }
        }
    }
}
