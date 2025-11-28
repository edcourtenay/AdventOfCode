namespace AdventOfCode.Solutions;

static class CircularLinkedList {
    extension<T>(LinkedListNode<T> current)
    {
        public LinkedListNode<T> NextOrFirst()
        {
            return (current.Next ?? current.List!.First)!;
        }

        public LinkedListNode<T> PreviousOrLast()
        {
            return (current.Previous ?? current.List!.Last)!;
        }
    }
}
