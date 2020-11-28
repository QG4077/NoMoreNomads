using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Nomads.Entities.Constants;

namespace Nomads.Entities.Controllers{
    public class NomadController : BaseEntityController
    {
        [Header("Debug")]
        public GameObject collisionPoint;
        public GameObject transformedPoint;
        [Header("Nomad Abilities")]
        [SerializeField] public bool CanTunnel; //Has the ability to dig sideways
        [SerializeField] public bool canDig; //Has the ability to dig down
        [SerializeField] public bool canClimb; //Fairly self explanatory

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
                        Vector3 targetVelocity = new Vector2(MoveSpeed * NomadConstants.MOVEMENT_SPEED_FACTOR, entity_RigidBody.velocity.y);
                        entity_RigidBody.velocity = Vector2.SmoothDamp(entity_RigidBody.velocity, targetVelocity, ref CurrentVelocity, 1 / AccelerationSpeed);
                    }
                    else //Move Left
                    {
                        Vector3 targetVelocity = new Vector2(-MoveSpeed * NomadConstants.MOVEMENT_SPEED_FACTOR, entity_RigidBody.velocity.y);
                        entity_RigidBody.velocity = Vector2.SmoothDamp(entity_RigidBody.velocity, targetVelocity, ref CurrentVelocity, 1 / AccelerationSpeed);
                    }
                }
            }

            Debug.Log(entity_RigidBody.velocity);
        }

        void OnCollisionEnter2D(Collision2D Coll)
        {
            if(isGrounded)
            {
                if(CanTunnel &&(Coll.otherCollider == LeftCollider || Coll.otherCollider == RightCollider)) {
                    NomadTunnelCollision(Coll);
                }
            }
        }

        private void NomadTunnelCollision(Collision2D Coll) 
        {
            if(((Coll.otherCollider == RightCollider && FacingRight) || (Coll.otherCollider == LeftCollider && !FacingRight)) && Coll.collider.gameObject.name == "Terrain")
            {
                for(int i = 0; i < Coll.contacts.Length; i++)
                {
                    var position = Coll.contacts[i].point;
                    Vector2 posToDelete = Vector2.zero;

                    if(Coll.otherCollider == RightCollider && FacingRight) {
                        posToDelete = new Vector2(position.x + NomadConstants.TILE_DESTROY_DISPLACEMENT, position.y);
                        }
                    else if(Coll.otherCollider == LeftCollider && !FacingRight) { 
                        posToDelete = new Vector2(position.x - NomadConstants.TILE_DESTROY_DISPLACEMENT, position.y);
                        }
                    else { Debug.Log("Bad collision on tile"); continue;}

                    var tile = Coll.collider.gameObject.GetComponent<Tilemap>().GetTile(Coll.gameObject.GetComponent<Tilemap>().WorldToCell(posToDelete));
                    if(tile != null)
                    {
                        Coll.collider.gameObject.GetComponent<Tilemap>().SetTile(Coll.gameObject.GetComponent<Tilemap>().WorldToCell(posToDelete), null);
                        break;
                    }
                }     
            }
        }
    }
}
