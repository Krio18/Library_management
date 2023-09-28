using ClassPlantUMLHypertext;
using ClassIPlantUML_Interface;
using PlantUml.Net;
using System.Reflection;
using System.Text;

namespace ClassPlantUML
{
    internal class PlantUML : IPlantUML_Interface
    {
        public void GeneratePlantUml()
        {
            StringBuilder codePlantUML = new();

            var assembly = Assembly.GetExecutingAssembly();
            IEnumerable<Type> classTypes = assembly.GetTypes().Where(type => type.Namespace?.StartsWith("Class") == true &&
                !type.Name.Contains('<') && !type.Name.Contains('>'));
            codePlantUML.Append("@startuml" + Environment.NewLine);

            foreach (Type type in classTypes)
            {
                PropertyInfo[] properties = type.GetProperties();
                IEnumerable<MethodInfo> methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).Where(method => method.DeclaringType != typeof(object) &&
                    !method.Name.StartsWith("get_") && !method.Name.StartsWith("set_") &&
                    !method.Name.Contains('<') && !method.Name.Contains('>'));
                string umlType = type.IsInterface ? "interface" : "class";

                codePlantUML.Append(Environment.NewLine + $"\t{umlType} {type.Name}" + Environment.NewLine + "\t" + @"{");

                _WriteproProperties(codePlantUML, properties);
                _WriteproMethods(codePlantUML, methods);

                codePlantUML.Append(Environment.NewLine + "\t\t[[secondPart.svg]]");
                codePlantUML.Append(Environment.NewLine + "\t}" + Environment.NewLine);

                _WriteproLink(codePlantUML, classTypes, type);
            }
            codePlantUML.Append(@"@enduml");

            Console.WriteLine(codePlantUML);
            _ = _CreateSVGFiles(codePlantUML.ToString());
        }

        public void _WriteproProperties(StringBuilder codePlantUML, PropertyInfo[] properties)
        {
            foreach (PropertyInfo property in properties)
            {
                codePlantUML.Append(Environment.NewLine + $"\t\t{property.Name} : {property.PropertyType.Name}");
            }
        }

        public void _WriteproMethods(StringBuilder codePlantUML, IEnumerable<MethodInfo> methods)
        {
            foreach (var method in methods)
            {
                string access = (method.IsPrivate == true) ? "-" : "+";
                codePlantUML.Append(Environment.NewLine + $"\t\t{access} {method.Name}()");
            }
        }

        public void _WriteproLink(StringBuilder codePlantUML, IEnumerable<Type> classTypes, Type type)
        {
            foreach (var t in classTypes)
            {
                if (type != t && type.IsSubclassOf(t))
                    codePlantUML.Append(Environment.NewLine + $"\t{t.Name} <|-- {type.Name}");
            }
            _ = codePlantUML.Append(Environment.NewLine);
        }

        private static async Task _CreateSVGFiles(string codePlantUML)
        {
            PlantUMLHypertext linkPlantUml = new();
            var factory = new RendererFactory();
            var renderer = factory.CreateRenderer(new PlantUmlSettings());
            var mainBytes = await renderer.RenderAsync(codePlantUML, OutputFormat.Svg);
            var bytes = await renderer.RenderAsync(linkPlantUml.Hypertext(), OutputFormat.Svg);

            File.WriteAllBytes("../../../src/ClassDiagram/mainDiagram.svg", mainBytes);
            File.WriteAllBytes("../../../src/ClassDiagram/secondPart.svg", bytes);
        }
    }

}
