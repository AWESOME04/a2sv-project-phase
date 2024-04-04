namespace ShapeHierarchy
{
    public class Shape
    {
        public string? Name { get; set; }

        public virtual double CalculateArea()
        {
            return 0;
        }
    }

    public class Circle : Shape
    {
        public double Radius { get; set; }

        public override double CalculateArea()
        {
            return Math.PI * Radius * Radius;
        }
    }

    public class Rectangle : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public override double CalculateArea()
        {
            return Width * Height;
        }
    }

    public class Triangle : Shape
    {
        public double Base { get; set; }
        public double Height { get; set; }

        public override double CalculateArea()
        {
            return (Base * Height) / 2;
        }
    }


    public class Program
    {
        public static void PrintShapeArea(Shape shape)
        {
            Console.WriteLine($"Name of the Shape: {shape.Name}");
            Console.WriteLine($"The area of the {shape.Name} is: {Math.Round(shape.CalculateArea(), 2)}");
            Console.WriteLine("");
        }
        static void Main(string[] args)
        {
            Circle circle = new Circle() { Name = "Circle", Radius = 5 };
            Rectangle rectangle = new Rectangle() { Name = "Rectangle", Height = 2, Width = 3 };
            Triangle triangle = new Triangle() { Name = "Triangle", Base = 2, Height = 4 };

            PrintShapeArea(circle);
            PrintShapeArea(rectangle);
            PrintShapeArea(triangle);

        }
    }
}
