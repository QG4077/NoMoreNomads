using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Nomads.Entities.Controllers{
    public class NomadController : BaseEntityController
    {
        [Header("Nomad Abilities")]
        [SerializeField] public bool canDestroy;

        [HideInInspector] public const float DestructionRangeRadius = 0.1f;
        public override void Start()
        {
            base.Start();
        }


        public override void FixedUpdate() 
        {
            base.FixedUpdate();
            if (isAwake)
            {
                if (isGrounded || AirControl) //Can player move
                {
                    if (FacingRight) //Move Right
                    {
                        Vector3 targetVelocity = new Vector2(MoveSpeed * 10f, entity_RigidBody.velocity.y);
                        entity_RigidBody.velocity = Vector2.SmoothDamp(entity_RigidBody.velocity, targetVelocity, ref CurrentVelocity, 1 / AccelerationSpeed);
                    }
                    else //Move Left
                    {
                        Vector3 targetVelocity = new Vector2(-MoveSpeed * 10f, entity_RigidBody.velocity.y);
                        entity_RigidBody.velocity = Vector2.SmoothDamp(entity_RigidBody.velocity, targetVelocity, ref CurrentVelocity, 1 / AccelerationSpeed);
                    }
                }
            }
        }

        void OnCollisionEnter2D(Collision2D Coll)
        {
            if(Coll.otherCollider == RightCollider)
            {
                if(Coll.collider.gameObject.name == "Terrain")
                {
                    Debug.Log("Collision detected");
                    for(int i = 0; i < Coll.contacts.Length; i++)
                    {
                        var position = Coll.contacts[i].point;
                        Coll.collider.gameObject.GetComponent<Tilemap>().SetTile(Coll.gameObject.GetComponent<Tilemap>().WorldToCell(position), null);
                    }
                }
            }
        }
    }
}
