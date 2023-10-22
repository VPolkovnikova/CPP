namespace Logics;


public static class Algorithm
{
    public static int CalculateNumberFriends(bool[,] matrix, int index)
    {
        if (matrix.GetLength(0) != matrix.GetLength(1))
        {
            throw new ArgumentException("Має бути квадратною.", nameof(matrix));
        }
        var size = matrix.GetLength(0);

        var isVisited = new bool[size];
        return Visit(index);


        int Visit(int i)
        {
            isVisited[i] = true;
            var numberFriends = 0;
            for (int j = 0; j < size; ++j)
            {
                if (matrix[i, j] && !isVisited[j])
                {
                    numberFriends = 1 + Visit(j);
                }
            }
            return numberFriends;
        }
    }
}