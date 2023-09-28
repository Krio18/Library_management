using System.Reflection;
using System.Text;

namespace ClassIPlantUML_Interface
{
    internal interface IPlantUML_Interface
    {
        void _WriteproProperties(StringBuilder codePlantUML, PropertyInfo[] properties);

        void _WriteproMethods(StringBuilder codePlantUML, IEnumerable<MethodInfo> methods);

        void _WriteproLink(StringBuilder codePlantUML, IEnumerable<Type> classTypes, Type type);
    }
}
