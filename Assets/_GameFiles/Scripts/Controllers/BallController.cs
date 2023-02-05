using System;
using System.Collections;
using System.Collections.Generic;
using _GameFiles.Scripts.ScriptableObjects;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.ProBuilder;

namespace _GameFiles.Scripts.Controllers
{
    public class BallController : MonoBehaviour
    {

        private Rigidbody2D _rb;
        
        private Vector3 _forceAmount;
        
        private List<float> _angleList = new List<float>
        {
            0,
            45,
            90,
            135,
            180,
            225,
            270,
            315
        };
        
        private int _forceCount;
        private int _explodeAmount;
        
        private float _checkRadius;
        private float _angle;
        
        private LayerMask _cubeLayer;
        private LayerMask _drawingLayer;
        
        
        private bool _isCollide;
        public void SetBall(float radius, float angle, int amount, Vector3 pos, LayerMask cubeLayer, LayerMask drawingLayer)
        {
            _rb = GetComponent<Rigidbody2D>();
            _checkRadius = radius;
            _angle = angle;
            _explodeAmount = amount;
            transform.position = pos;
            _cubeLayer = cubeLayer;
            _drawingLayer = drawingLayer;
        }

        private void Update()
        {
            RayThrower();
        }

        private void RayThrower()
        {
            for (int i = 0; i < _angleList.Count; i++)
            {
                bool drawing = CloseDistRaycast(_angleList[i], _drawingLayer);
                bool cube = CloseDistRaycast(_angleList[i], _cubeLayer);
                if (drawing)
                {
                    
                }
                else if (cube)
                {
                    
                }
            }
        }
        
        private bool CloseDistRaycast(float angle, LayerMask layer)
        {
            Vector3 direction = Quaternion.AngleAxis(angle, transform.forward) * transform.up;
            Debug.DrawRay(transform.position, direction, Color.red, 5);

            if (layer == 1 << 6)
            {
                if (Physics.Raycast(transform.position, direction, 1f, layer))
                {
                    return true;
                }
                return false;
            }
            
            if (Physics2D.Raycast(transform.position, direction, 1, layer))
            {
                return true;
            }
                
            return false;
            

        }
        
        
        
        
        
        
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Drawing"))
            {
                if (_isCollide)
                {
                    return;
                }
            
                _isCollide = true;
                AddForceAtAngle(.5f, _angle);
            }
            else
            {
                Debug.Log("FU");
                CheckCubeCollider(); 
            }
        }
        
        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Drawing"))
            {
                _isCollide = false;
            }
        }

        private void AddForceAtAngle(float force, float angle)
        {
            if (_angle > 10f)
            {
                return;
            }
            angle *= Mathf.Deg2Rad;
            float x = Mathf.Cos(angle) * force;
            float y = Mathf.Sin(angle) * force;
            _forceAmount = new Vector3(x, y , 0);
        }
        private void FixedUpdate()
        {
            if (_forceAmount == Vector3.zero || !_isCollide)
            {
                return;
            }

            while (_rb.velocity.magnitude <= 3 && _forceCount < 8)
            {
                _forceCount++; 
                _rb.AddForceAtPosition(_forceAmount, Vector2.Reflect(_forceAmount, transform.position), (ForceMode2D)ForceMode.Acceleration);
            }
        }
        private void CheckCubeCollider()
        {
            // Collider2D[] hitCubes = new Collider2D[_explodeAmount];
            // int numCubes = Physics2D.overlap(transform.position, _checkRadius, hitCubes, cubeLayer);
            // Debug.Log(numCubes);
            // for (int i = 0; i < numCubes; i++)
            // {
            //     CubeController cube = hitCubes[i].GetComponent<CubeController>();
            //     Debug.Log("boom");
            //     //TODO: action
            //     //OnCarCollideWithMetalPiece?.Invoke(cube);
            // }
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _checkRadius);
        }
    }
}