using System;
using _GameFiles.Scripts.Utilities;
using UnityEngine;

namespace _GameFiles.Scripts.Controllers
{
    //Cube object and its controller.
    public class CubeController : Controller
    {
        public event Action<CubeController> OnCubeIsExplodeEvent;

        private Rigidbody _rb;
        private Renderer _renderer;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _renderer = GetComponent<Renderer>();
        }

        public void SetCube(Vector3 pos, Color color)
        {
            _rb.isKinematic = true;
            transform.position = pos;
            MaterialPropertyBlockUtility.ColorSetter(_renderer, color);
           _renderer.material.color = color;
        }
        
        //Sets physical force if ball hits.
        public void FallDown()
        {
            OnCubeIsExplodeEvent?.Invoke(this);
            
            _rb.isKinematic = false;
            _rb.AddForceAtPosition(new Vector3(20, -8), transform.position, ForceMode.Impulse);
        }
    }
}
