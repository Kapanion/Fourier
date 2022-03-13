using System.Numerics;
using Aspose.Svg;
using Aspose.Svg.Collections;

public class FileToPointsConverter
{
    private const int StepLengthCoefficient = 500; // roughly indicates how many points would fit on the longest axis in a straight line
    private readonly string _fileAddress;
    private float _minX = float.MaxValue;
    private float _minY = float.MaxValue;
    private float _maxX = float.MinValue;
    private float _maxY = float.MinValue;

    static void Main(string[] args)
    {
        var program = new FileToPointsConverter("/home/luka/Downloads/Biocinematics_Fourier_Vector.svg");

        var vecListList = program.ConvertSvgToPoints();

        PrintVecListList(vecListList);
    }

    public FileToPointsConverter(string fileAddress)
    {
        this._fileAddress = fileAddress;
    }

    public List<List<Vector2>> ConvertSvgToPoints()
    {
        var document = new SVGDocument(_fileAddress);
        var documentElement = document.DocumentElement;
        var g = documentElement.GetElementsByTagName("g").First() as SVGGraphicsElement ?? throw new ArgumentNullException("Existence of g element is required");
        var elements = g.GetElementsByTagName("path");

        CalculateBBoxValues(elements);

        var vecListList = ConvertPathsToPoints(elements);

        return vecListList;
    }

    private void CalculateBBoxValues(HTMLCollection elements)
    {
        foreach (var element in elements)
        {
            // TODO: may need to throw exception instead of continuing
            if (element is not SVGGeometryElement path) continue;
            var box = path.GetBBox();
            _minX = Math.Min(_minX, box.X);
            _minY = Math.Min(_minY, box.Y);
            _maxX = Math.Max(_maxX, box.X + box.Width);
            _maxY = Math.Max(_maxY, box.Y + box.Height);
        }

        _minX -= 1.0f;
        _minY -= 1.0f;
        _maxX -= 1.0f;
        _maxY -= 1.0f;
    }

    private List<List<Vector2>> ConvertPathsToPoints(HTMLCollection elements)
    {
        List<List<Vector2>> vecListList = new List<List<Vector2>>();
        float stepLength = Math.Max(_maxX - _minX, _maxY - _minY) / StepLengthCoefficient;
        
        foreach (var element in elements)
        {
            // TODO: may need to throw exception instead of continuing
            if (element is not SVGGeometryElement path) continue;
            List<Vector2> vecList = new List<Vector2>();

            for (float i = 0; i < path.GetTotalLength(); i += stepLength)
            {
                var point = path.GetPointAtLength(i);
                vecList.Add(new Vector2(point.X - _minX, point.Y - _minY));
            }

            vecListList.Add(vecList);
        }

        // sort lists by descending order based on their lengths
        vecListList.Sort((x, y) => y.Count.CompareTo(x.Count));

        return vecListList;
    }

    public static void PrintVecListList(List<List<Vector2>> vecListList)
    {
        var counter = 0;
        foreach (var vecList in vecListList)
        {
            foreach (var vec in vecList)
            {
                Console.WriteLine("drawCoordinates(" + vec.X + ", " + vec.Y + ");");
            }
            Console.WriteLine("# " + (++counter));
        }
    }
}