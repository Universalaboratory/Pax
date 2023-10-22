using UnityEngine;
[RequireComponent (typeof(Card))]
public class CardMover : MonoBehaviour
{
    private bool _isMoving;
    private Vector3 _startPosition;
    private Vector3 _startScale;
    private Card _card;
    [SerializeField] private float _scaleFactor = 0.4f;
    [SerializeField] private bool _usedInTable = false;
    [SerializeField] private GameObjectVariable _houseSelected;

    private void Start()
    {
        _card = GetComponent<Card> ();
        _startPosition = transform.position;
        _startScale = transform.localScale;
    }
    private void Update()
    {
        if (_isMoving && !_usedInTable && PaxTurnManager.Instance.isMyTurn())
        {
            Vector3 newPosition = Input.mousePosition;
            newPosition.z = transform.position.z;
            transform.position = newPosition;

            // Verifica se o bot�o do mouse foi solto para parar o movimento
            if (Input.GetMouseButtonUp(0))
            {
                _isMoving = false;
                if (_houseSelected.CurrentValue == null)
                {
                    transform.position = _startPosition;
                    transform.localScale = _startScale;
                }
                else
                {
                    transform.position = Camera.main.WorldToScreenPoint(_houseSelected.CurrentValue.transform.position);
                    _usedInTable = true;

                    _card.OnCardUsed.Invoke(_card.cardData, this.gameObject);
                }
            }
        }
    }
    public void OnMouseEnterUI()
    {
        _card.OnCardHovered.Invoke(_card);
        Debug.Log("Entrou na UI, caso queira adicionar algum efeito � aqui");
    }

    public void OnMouseExitUI()
    {
        Debug.Log("Entrou na UI, caso queira remover algum efeito � aqui");
    }
    public void OnMouseDownUI()
    {
        if (!PaxTurnManager.Instance.isMyTurn()) return;
        transform.localScale = _startScale * _scaleFactor;
        _isMoving = true;
    }
}
