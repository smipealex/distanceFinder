namespace DistanceFinder;

public static class Program
{
    public static void Main(string[] args)
    {
        var firstSegmentPointer = GetPointer("Please enter the first pointer of the line: ");
        var secondSegmentPointer = GetPointer("Please enter the second pointer of the segment: ");
        var freePointer = GetPointer("Please enter a free pointer: ");
        
        Console.WriteLine($"The segment with pointers at {firstSegmentPointer.GetInfo()} and {secondSegmentPointer.GetInfo()} " +
                          $"and the free pointer at {freePointer.GetInfo()}");

        var theFirstAngle = GetAngle(firstSegmentPointer, secondSegmentPointer, freePointer);
        var theSecondAngle = GetAngle(secondSegmentPointer, firstSegmentPointer, freePointer);
        
        Console.WriteLine($"The first angle equals {theFirstAngle} and the second angle equals {theSecondAngle}");

        double theHeight;

        if (theFirstAngle + theSecondAngle <= 90)
        {
            // GEREON'S THEOREM
            var a = firstSegmentPointer.GetDistance(freePointer);
            var b = freePointer.GetDistance(secondSegmentPointer);
            var c = secondSegmentPointer.GetDistance(firstSegmentPointer);

            var p = (a + b + c) / 2;

            var squareByGereonTheorem = Math.Sqrt(p * (p - a) * (p - b) * (p - c));
            theHeight = squareByGereonTheorem / c * 2;
        }
        else
        {
            var theFirstDistance = firstSegmentPointer.GetDistance(freePointer);
            var theSecondDistance = secondSegmentPointer.GetDistance(freePointer);

            theHeight = theFirstDistance > theSecondDistance ? theSecondDistance : theFirstDistance;
        }
        
        Console.WriteLine($"The height equals {theHeight}");
    }

    private static Pointer GetPointer(string text)
    {
        Console.Write(text);
        string?[]? inputData = Console.ReadLine()?.Split(' ');

        return new Pointer(double.Parse(inputData?[0] ?? string.Empty), double.Parse(inputData?[1] ?? string.Empty));
    }


    /// <summary>
    /// This makes vectors from two segments and gets cos from this vectors
    /// </summary>
    /// <param name="generalPoint">The common point of two segments</param>
    /// <param name="firstPoint">The point of the first segment</param>
    /// <param name="secondPoint">The point of the second segment</param>
    /// <returns>angle between two segments</returns>
    private static double GetAngle(Pointer generalPoint, Pointer firstPoint, Pointer secondPoint)
    {
        var firstVector = firstPoint.Subtraction(generalPoint);
        var secondVector = secondPoint.Subtraction(generalPoint);

        var cos = firstVector.Multiply(secondVector) / (firstVector.GetModule() * secondVector.GetModule());

        return Math.Sqrt(Math.Pow(90 - Math.Acos(cos) * (180 / Math.PI), 2));
    }

    private class Pointer
    {
        private double XPosition { get; }
        private double YPosition { get; }

        public Pointer(double x, double y)
        {
            XPosition = x;
            YPosition = y;
        }

        public string GetInfo()
        {
            return $"({XPosition};{YPosition})";
        }

        public double Multiply(Pointer other)
        {
            return XPosition * other.XPosition + YPosition * other.YPosition;
        }

        public double GetModule()
        {
            return
                Math.Sqrt(Math.Pow(XPosition, 2) + Math.Pow(YPosition, 2));
        }

        public Pointer Subtraction(Pointer other)
        {
            return
                new Pointer(XPosition - other.XPosition, YPosition - other.YPosition);
        }

        public double GetDistance(Pointer other)
        {
            return
                Math.Sqrt(Math.Pow(XPosition - other.XPosition, 2) + Math.Pow(YPosition - other.YPosition, 2));
        }
    }
}