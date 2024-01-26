using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float lifeTime = 1f;
    private float _life = 0f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * PlayerWeaponsManager.GetLaserSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        _life += Time.deltaTime;
        if (_life >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if ((other.gameObject.CompareTag("Enemy") || (other.gameObject.CompareTag("Enemy Sniper")) && !PlayerWeaponsManager.GetPenetrationRounds()))
        {
            StartCoroutine(Co_DestroyLaserRoutine());
        }
        else if (other.gameObject.CompareTag("Enemy Shield") && !PlayerWeaponsManager.GetPenetrationRounds())
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Co_DestroyLaserRoutine()
    {
        yield return new WaitForSeconds(0.05f);
        Destroy(gameObject);
    }
}
