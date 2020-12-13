using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Knight : Piece
{
    public override void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager)
    {
        base.Setup(newTeamColor, newSpriteColor, newPieceManager);

        GetComponent<Image>().sprite = Resources.Load<Sprite>("white_knight");
    }
    private void SameState(int targetX, int targetY)
    {
        SpaceState spaceState = SpaceState.None;

        spaceState = currentSpace.board.ValidateSpace(targetX, targetY, this);

        if (spaceState != SpaceState.Friendly && spaceState != SpaceState.OutOfBounds)
        {
            highlightedSpaces.Add(currentSpace.board.allSpaces[targetX, targetY]);
        }
    }

    private void MakeSpacePath(int flipper)
    {
        int currX = currentSpace.boardPosition.x;
        int currY = currentSpace.boardPosition.y;

        SameState(currX - 2, currY + (1 * flipper));
        SameState(currX - 1, currY + (2 * flipper));

        SameState(currX + 1, currY + (2 * flipper));
        SameState(currX + 2, currY + (1 * flipper));
    }

    protected override void CheckPathing()
    {
        //Show top spaces.
        MakeSpacePath(1);

        //Show bottom spaces.
        MakeSpacePath(-1);
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