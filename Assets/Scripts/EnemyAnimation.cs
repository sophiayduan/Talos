using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private static int inputY = Animator.StringToHash("umm");
    private EnemyScript _enemyscript;

    private void Awake()
    {
        _enemyscript = GetComponent<EnemyScript>();
    }
    private void Update()
    {
        UpdateAnimation();
    }

    private void UpdateAnimation(){
        Vector2 inputTarget = _enemyscript.Heading;
        _animator.SetFloat(inputY, inputTarget.y);
    }
}
