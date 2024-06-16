using UnityEngine;

public class Explosion : MonoBehaviour
{
    //[SerializeField] private GameObject _explosionEffector;
    //[SerializeField] private GameObject _explosionEffect;

    private void Start()
    {
        //Explode();
        Destroy(gameObject, 1.5f);
    }

    //private void Explode()
    //{
    //    _explosionEffector.SetActive(true);
    //    _explosionEffect.SetActive(true);

    //    Invoke(nameof(HideExplosion), 1.5f);
    //}

    //private void HideExplosion()
    //{
    //    _explosionEffector.SetActive(false);
    //    _explosionEffect.SetActive(false);
    //}

    private void OnTriggerStay2D(Collider2D collision)
    {
        Player player = collision.GetComponentInParent<Player>();

        if (player)
        {
            player.BecomeDynamic();
        }
    }
}
