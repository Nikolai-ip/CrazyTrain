using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Color _changeColor;
    [SerializeField] private float _timeChangeColor;
    private SpriteRenderer _sp;

    private void Start()
    {
        _sp = GetComponent<SpriteRenderer>();
    }

    public async void ChangeColorForWhile()
    {
        Color originalColor = _sp.color;
        _sp.color = _changeColor;
        await Task.Delay((int)(_timeChangeColor * 1000));
        if (!_sp.IsDestroyed())
        {
            _sp.color = originalColor;
        }
    }
}