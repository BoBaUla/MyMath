using Mathematik;

namespace MyMath.Interfaces
{
    public interface ISplineCondition
    {
        void AddConditionalValues(ref Matrix matrix, double leftEnd, double rightEnd);
    }
}
