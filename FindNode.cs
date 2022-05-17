namespace N_Puzzle
{
    class FindNode
    {
        // Fields Area
        public Board CurrentNode = null;
        public int NumofMoves = 0;
        public int Priorty = 0;
        // public FindNode parent;
        // public int[] arr;
        // Methods Area
        //Checking The Priorty and Assigning Number of Moves
        public FindNode(Board InitialBoard)
        {

            NumofMoves = InitialBoard.Get_NUmber_ofmoves();
            CurrentNode = InitialBoard;
            if (Program.Method == '1')
            {
                Priorty = InitialBoard.MDistance + NumofMoves;
            }
            else
            {
                Priorty = InitialBoard.Hamming_Distance() + NumofMoves;
            }
           
           /* if (InitialBoard.BoardParent != null)
            {
                parent = new FindNode();
                parent.arr = InitialBoard.BoardParent;
            }*/
        }
        // Returning Priorty
        public int GetPriorty()
        {
            return Priorty;
        }
        // Returning Current Node
        public Board getBoard()
        {
            Board CurrNode = CurrentNode;
            return CurrNode;
        }
        // Returning Moves
        public int GetMoves()
        {
            return NumofMoves;
        }
        // Comparing Priorties
        public int Comparer(FindNode AnotherNode)
        {
            if (this.Priorty > AnotherNode.Priorty)
            {
                return 1;
            }
            else if (this.Priorty < AnotherNode.Priorty)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
