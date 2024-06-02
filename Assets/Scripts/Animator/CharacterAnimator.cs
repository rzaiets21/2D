using UnityEngine;

[RequireComponent(typeof(Animator))]
public sealed class CharacterAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void UpdateState(int parameterId, float value)
    {
        _animator.SetFloat(parameterId, value);
    }

    public void UpdateState(int parameterId, bool value)
    {
        _animator.SetBool(parameterId, value);
    }
}