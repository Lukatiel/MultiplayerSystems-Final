using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pawn : Piece
{
    private bool isFirstMove = true;

    public override void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager)
    {
        base.Setup(newTeamColor, newSpriteColor, newPieceManager);

        //Set movement back to first move.
        isFirstMove = true;

        //Pawn specific. Set direction based on what side the pawn is at.
        movement = color == Color.white ? new Vector3Int(0, 1, 1) : new Vector3Int(0, -1, -1);
        GetComponent<Image>().sprite = Resources.Load<Sprite>("white_pawn");

        name = "Pawn";
    }

    protected override void Move()
    {
        base.Move();

        isFirstMove = false;
    }

    private bool SameState(int targetX, int targetY, SpaceState targetState)
    {
        SpaceState spaceState = SpaceState.None;

        spaceState = currentSpace.board.ValidateSpace(targetX, targetY, this);

        if (spaceState == targetState)
        {
            highlightedSpaces.Add(currentSpace.board.allSpaces[targetX, targetY]);
            return true;
        }

        return false;
    }

    protected override void CheckPathing()
    {
        int currX = currentSpace.boardPosition.x;
        int currY = currentSpace.boardPosition.y;

        //Check diagonal left.
        SameState(currX - movement.z, currY + movement.z, SpaceState.Enemy);

        //Check forward.
        if (SameState(currX, currY + movement.y, SpaceState.Free))
            if (isFirstMove)
                SameState(currX, currY + (movement.y * 2), SpaceState.Free);
        
        //Check diagonal right.
        SameState(currX + movement.z, currY + movement.z, SpaceState.Enemy);
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.name = transform.name.Replace("(clone)", "").Trim();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
