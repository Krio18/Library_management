using PlantUml.Net;
using System.Reflection;
using System.Text;
using ClassHypertext;

namespace ClassPlantUML
{
    internal class PlantUML
    {
        public static async Task GeneratePlantUml() {
            Hypertext link = new();
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes().Where(t => t.Namespace.StartsWith("Class") &&
                !t.Name.Contains("<") && !t.Name.Contains(">"));
            string codePlantUML = "@startuml\n";
            foreach (var type in types)
            {

                var properties = type.GetProperties();
                var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).Where(method => method.DeclaringType != typeof(object) &&
                    !method.Name.StartsWith("get_") &&
                    !method.Name.StartsWith("set_") &&
                    !method.Name.Contains("<") &&
                    !method.Name.Contains(">"));


                codePlantUML += @$"
    class {type.Name}" + @"
    {";
                StringBuilder sb = new();
                foreach (var property in properties)
                {
                    codePlantUML += $"\n        {property.Name} : {property.PropertyType.Name}";
                }
                codePlantUML += "";
                foreach (var method in methods)
                {
                    string access = (method.IsPrivate == true) ? "- " : "+ ";
                    codePlantUML += $"\n        {access} {method.Name}()";

                }
                codePlantUML += "\n";
                codePlantUML += "    [[secondPart.svg]]";
                codePlantUML += "\n    }\n";
                foreach (var t in types)
                {
                    if (type != t && type.IsSubclassOf(t)) 
                        codePlantUML += $"\n    {t.Name} <|-- {type.Name}";
                }
                codePlantUML += "\n";
            }
            codePlantUML += @"@enduml";

            Console.WriteLine(codePlantUML);
            var factory = new RendererFactory();

            var renderer = factory.CreateRenderer(new PlantUmlSettings());

            var mainBytes = await renderer.RenderAsync(codePlantUML, OutputFormat.Svg);
            File.WriteAllBytes("../../../src/ClassDiagram/mainDiagram.svg", mainBytes);

            var bytes = await renderer.RenderAsync(link.PlantUMLHypertext(), OutputFormat.Svg);
            File.WriteAllBytes("../../../src/ClassDiagram/secondPart.svg", bytes);
        }
    }
}
