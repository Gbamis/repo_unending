using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UE
{

    public class Missile : MonoBehaviour
    {
        public OWNER owner;
        public float takeDamge;
        private Transform target = null;
        private Transform parent;
        private float speed;
        public Rigidbody rb;

        private void Update()
        {
            if (target != null)
            {

                //TMovement();

                //Movement();
            }

        }

        private void Movement()
        {
            rb.velocity = transform.forward * speed;
            var dir = (target.position - transform.position);
            var look = Quaternion.LookRotation(dir);
            rb.MoveRotation(look);
        }

        private void TMovement()
        {
            Vector3 dir = target.position - transform.position;
            Vector3 rot = Vector3.RotateTowards(transform.forward, dir, Time.deltaTime * 10, 0.0f);
            transform.rotation = Quaternion.LookRotation(rot);
            Vector3 newPos = Vector3.MoveTowards(transform.position,target.position,Time.deltaTime * speed);
            transform.position = newPos - parent.position;
        }

        public void SetOwner(OWNER oWNER, float damageAmount)
        {
            owner = oWNER;
            takeDamge = damageAmount;
        }

        public void FireAt(Transform m_target, Transform m_parent,float m_speed)
        {
            target = m_target;
            speed = m_speed;
            parent = m_parent;
        }

        private void OnTriggerEnter(Collider other)
        {
            GameManager.Instance.missileBulletPoolManager.ReturnBullet(gameObject);
        }
    }
}
