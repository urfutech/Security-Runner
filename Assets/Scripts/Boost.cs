using System.Collections;
using UnityEngine;

public abstract class Boost : MonoBehaviour
{
    [SerializeField] protected float duration;

    protected PlayerMove playerMove;
    protected Transform playerTransform;

	protected static bool isBoostActive = false;

	private void Start()
    {
        playerMove = GameManager.Instance.PlayerMove;
        playerTransform = GameManager.Instance.PlayerTransform;
    }

    public void Initialize(int lineId)
    {
        playerTransform = GameManager.Instance.PlayerTransform;
        playerMove = GameManager.Instance.PlayerMove;

        transform.position = new(playerTransform.position.x + 50, 1f, GameManager.Instance.LineManager.Lines[lineId]);
    }

    public virtual void PickUp()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        isBoostActive = true;
        StartCoroutine(RemoveAfterDuration());
    }

    protected virtual IEnumerator RemoveAfterDuration()
    {
        yield return new WaitForSeconds(duration);
        Remove();
        Destroy(gameObject);
    }

    public abstract void Remove();
}
