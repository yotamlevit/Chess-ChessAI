using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chess.Game{
    public class BoardUI : MonoBehaviour
    {

        public bool whiteIsBottom = true;

        MeshRenderer[,] squareRenderers;
        SpriteRenderer[,] squarePieceRenderers;

        const float pieceDepth = -0.1f;
        const float pieceDragDepth = -0.2f;

        public BoardTheme boardTheme;


        // Start is called before the first frame update
        void Start()
        {
            CreateBoardUI();
        }


        void CreateBoardUI()
        {
            Shader squareShader = Shader.Find("Unlit/Color");
            squareRenderers = new MeshRenderer[8, 8];
            squarePieceRenderers = new SpriteRenderer[8, 8];

            for (int rank = 0; rank < 8; rank++)
            {
                for (int file = 0; file < 8; file++)
                {
                    // Create square
                    Transform square = GameObject.CreatePrimitive(PrimitiveType.Quad).transform;
                    square.parent = transform;
                    square.name = rank.ToString() + '_' + file.ToString();//BoardRepresentation.SquareNameFromCoordinate(file, rank);
                    square.position = PositionFromCoord(file, rank, 0);
                    Material squareMaterial = new Material(squareShader);

                    squareRenderers[file, rank] = square.gameObject.GetComponent<MeshRenderer>();
                    squareRenderers[file, rank].material = squareMaterial;

                    // Create piece sprite renderer for current square
                    SpriteRenderer pieceRenderer = new GameObject("Piece").AddComponent<SpriteRenderer>();
                    pieceRenderer.transform.parent = square;
                    pieceRenderer.transform.position = PositionFromCoord(file, rank, pieceDepth);
                    pieceRenderer.transform.localScale = Vector3.one * 100 / (2000 / 6f);
                    squarePieceRenderers[file, rank] = pieceRenderer;
                }
            }

            ResetSquareColours();
        }


        public void ResetSquareColours(bool highlight = true)
        {
            for (int rank = 0; rank < 8; rank++)
            {
                for (int file = 0; file < 8; file++)
                {
                    SetSquareColour(new Coord(file, rank), boardTheme.lightSquares.normal, boardTheme.darkSquares.normal);
                }
            }
        }


        void SetSquareColour(Coord square, Color lightCol, Color darkCol)
        {
            squareRenderers[square.file, square.rank].material.color = (square.IsLightSquare()) ? lightCol : darkCol;
        }


        public Vector3 PositionFromCoord(int file, int rank, float depth = 0)
        {
            if (whiteIsBottom)
            {
                return new Vector3(-3.5f + file, -3.5f + rank, depth);
            }
            return new Vector3(-3.5f + 7 - file, 7 - rank - 3.5f, depth);

        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}
