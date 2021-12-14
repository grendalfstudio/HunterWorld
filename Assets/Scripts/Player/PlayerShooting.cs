using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.LoadingScene;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private GameObject aim;

        private int bulletsCount;
        
        // Start is called before the first frame update
        void Awake()
        {
            bulletsCount = PlayerProfile.Instance.GameData.BulletsCount;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            if (bulletsCount <= 0)
            {
                return;
            }
            
            var direction = aim.transform.position - transform.position;
            RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position,direction,5);
            if (hit.transform != null) {
                Debug.Log ("You Hit: " + hit.transform.parent.gameObject.name);
            }
            
        }
    }
}
