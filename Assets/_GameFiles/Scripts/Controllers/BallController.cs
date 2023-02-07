using System;
using System.Collections.Generic;
using UnityEngine;

namespace _GameFiles.Scripts.Controllers
{
    //Ball object and its controller.
    public class BallController : Controller
    {

        public event Action<BallController> OnBallExplodeEvent; 

        private Rigidbody2D _rb;
        
        private Vector3 _forceAmount;
        
        private LayerMask _cubeLayer;
        private LayerMask _drawingLayer;
        
        private readonly List<float> _rayAngleList = new List<float>
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
        
        private bool _isCollideWithDrawing;
        private bool _isCollideWithCube;

        //Sets settings of object.
        public void SetBall(float radius, float angle, int amount, Vector3 pos, LayerMask cubeLayer, LayerMask drawingLayer)
        {
            _rb = GetComponent<Rigidbody2D>();
            _checkRadius = radius;
            _angle = angle;
            _explodeAmount = amount;
            transform.position = pos;
            _cubeLayer = cubeLayer;
            _drawingLayer = drawingLayer;
            
            _forceAmount = Vector3.zero;
            _forceCount = 0;
            _isCollideWithCube = false;
            _isCollideWithDrawing = false;
        }

        //Calls RayThrower for detecting drawing and cubes.
        private void Update()
        {
            RayThrower();
        }
        
        //Applies force to object if its necessarily
        private void FixedUpdate()
        {
            if (_forceAmount == Vector3.zero || !_isCollideWithDrawing)
            {
                return;
            }

            while (_rb.velocity.magnitude <= 3 && _forceCount < 8)
            {
                _forceCount++; 
                _rb.AddForceAtPosition(_forceAmount, Vector2.Reflect(_forceAmount, transform.position), (ForceMode2D)ForceMode.Acceleration);
            }
        }
        private void RayThrower()
        {
            foreach (float rayAngle in _rayAngleList)
            {

                //Detects drawing.
                if (CloseDistRaycast(rayAngle, _drawingLayer) && !_isCollideWithDrawing)
                {
                    _isCollideWithDrawing = true;
                    CalculateForceByAngle(.5f, _angle);
                }
                //Detects cubes.
                else if (CloseDistRaycast(rayAngle, _cubeLayer) && !_isCollideWithCube)
                {
                    _isCollideWithCube = true;
                    CheckCubeCollider();
                }
            }
        }
        
        //Throws Ray by angle.
        private bool CloseDistRaycast(float rayAngle, LayerMask layer)
        {
            Transform transform1;
            Vector3 direction = Quaternion.AngleAxis(rayAngle, transform.forward) * (transform1 = transform).up;
            Debug.DrawRay(transform1.position, direction, Color.red, 5);

            if (layer == 1 << 6)
            {
                return Physics.Raycast(transform.position, direction, .25f, layer);
            }
            
            return Physics2D.Raycast(transform.position, direction, 1, layer);
        }
        
        //Calculates force amount vector.
        private void CalculateForceByAngle(float force, float angle)
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
        
        //Throws sphere cast to detect area after if ball detects cube.
        private void CheckCubeCollider()
        {
            RaycastHit[] hitCubes = new RaycastHit[_explodeAmount];
            int numberOfCubes = Physics.SphereCastNonAlloc(transform.position, _checkRadius, Vector3.forward, hitCubes, 0, _cubeLayer);

            for (int i = 0; i < numberOfCubes; i++)
            {
                CubeController cube = hitCubes[i].transform.GetComponent<CubeController>();
                cube.FallDown();
            }
            OnBallExplodeEvent?.Invoke(this);
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _checkRadius);
        }
    }
}