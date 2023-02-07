using UnityEngine;

namespace _GameFiles.Scripts.Controllers
{
    //MoneyMaker object and its controller.
    public class MoneyMakerController : MonoBehaviour
    {
        [SerializeField] private Transform origin;
        private Vector2 _direction;

        private void Awake()
        {
            _direction = new Vector3(10, 8);
        }

        //Throws new coin to the container.
        public void ThrowMoney(Controller coin)
        {
            coin.gameObject.SetActive(true);
            coin.transform.position = origin.position;
            coin.GetComponent<Rigidbody2D>().AddForce(_direction, ForceMode2D.Impulse);
        }
    }
}