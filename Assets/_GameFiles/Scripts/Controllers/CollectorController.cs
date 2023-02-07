using System;
using DG.Tweening;
using UnityEngine;

namespace _GameFiles.Scripts.Controllers
{
    //Collector object and its controller.
    public class CollectorController : MonoBehaviour
    {
        public event Action OnCubeCollectedEvent;
        [SerializeField] private LayerMask layer;
        [SerializeField] private Transform convertArea;
        [SerializeField] private float radius, distance;
        
        //Detects exploded cubes with sphere cast.
        private void Update()
        {
            if (Physics.SphereCast(transform.position, radius, Vector3.up, out RaycastHit hit, distance, layer, QueryTriggerInteraction.UseGlobal))
            {
                hit.transform.gameObject.layer = 7;
                ConvertCubeToMoney(hit.transform);
            }
        }

        //Pulls exploded cubes to the origin.
        private void ConvertCubeToMoney(Transform cube)
        {
            cube.GetComponent<Rigidbody>().isKinematic = true;
            cube.DOMove(convertArea.position, 1).OnComplete((() =>
            {
                cube.gameObject.SetActive(false);
                OnCubeCollectedEvent?.Invoke();
            }));
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position + Vector3.up * distance , radius);
        }
    }
}