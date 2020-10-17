using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class Piece : EventTrigger
{
    public Color color = Color.clear;
    protected Space originalSpace = null; //The space the piece should be at when the game is reset, the original board.
    protected Space currentSpace = null; //The space the piece is currently residing at.
    protected RectTransform rectTransform = null;
    protected PieceManager pieceManager;
    protected Space targetSpace = null;
    protected Vector3Int movement = Vector3Int.one;
    protected List<Space> highlightedSpaces = new List<Space>(); //List of available spaces the piece can move to.

    public virtual void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager)
    {
        pieceManager = newPieceManager;

        color = newTeamColor;
        GetComponent<Image>().color = newSpriteColor;
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetSpace(Space newSpace)
    {
        currentSpace = newSpace;
        originalSpace = newSpace;
        currentSpace.currentPiece = this;

        transform.position = newSpace.transform.position;
        gameObject.SetActive(true);
    }

    public void Reset()
    {
        Kill();
        SetSpace(originalSpace);
    }

    public void Kill()
    {
        currentSpace.currentPiece = null;
        gameObject.SetActive(false);
    }

    private void CreateCellPath(int xDir, int yDir, int movement)
    {
        int currX = currentSpace.boardPosition.x;
        int currY = currentSpace.boardPosition.y;

        for (int i = 1; i <= movement; i++)
        {
            currX += xDir;
            currY += yDir;

            highlightedSpaces.Add(currentSpace.board.allSpaces[currX, currY]);
        }
    }

    protected virtual void CheckPathing()
    {
        //horizontal
        CreateCellPath(1, 0, movement.x);
        CreateCellPath(-1, 0, movement.x);

        //vertical
        CreateCellPath(0, 1, movement.y);
        CreateCellPath(0, -1, movement.y);

        //upper diagonal
        CreateCellPath(1, 1, movement.z);
        CreateCellPath(-1, 1, movement.z);

        //lower diagonal
        CreateCellPath(-1, -1, movement.z);
        CreateCellPath(1, -1, movement.z);
    }

    protected void ShowCells()
    {
        foreach (Space space in highlightedSpaces)
        {
          space.mOutlineImage.enabled = true;
          Debug.Log("pos: " + space.boardPosition + " ?: " + space.isActiveAndEnabled);
        } 
    }

    protected void ClearCells()
    {
        foreach (Space space in highlightedSpaces)
            space.mOutlineImage.enabled = false;

        highlightedSpaces.Clear();
    }

    protected virtual void Move()
    {
        targetSpace.RemovePiece();

        currentSpace.currentPiece = null;

        currentSpace = targetSpace;
        currentSpace.currentPiece = this;

        transform.position = currentSpace.transform.position;
        targetSpace = null;
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);

        CheckPathing();
        ShowCells();
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);

        rectTransform.position += (Vector3)eventData.delta;

        foreach (Space space in highlightedSpaces)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(space.rectTransform, Input.mousePosition))
            {
                //if the mouse is within the space of a valid space, move to it then break out of the foreach loop.
                targetSpace = space;
                break;
            }

            //if the mouse ISN'T within a valid space, do nothing.
            targetSpace = null;
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        ClearCells();

        if (!targetSpace) //if no current space, set back to original space.
        {
            transform.position = currentSpace.gameObject.transform.position;
            return;
        }

        Move();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
