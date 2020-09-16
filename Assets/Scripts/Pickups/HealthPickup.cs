using UnityEngine;

public class HealthPickup : MonoBehaviour
{
        [SerializeField, Range(0,100)] private int hpToHeal;
        [SerializeField, Range(0f, 10f)] float fallingSpeed;

        private void OnCollisionEnter2D (Collision2D other) {
            GameHandler.instance.player.PickupHP(hpToHeal);
        
            Destroy(this.gameObject);
        }

        private void Update()
        {
            //MoveDown
            transform.position += Vector3.down * (fallingSpeed * Time.deltaTime);
        }
    
}
