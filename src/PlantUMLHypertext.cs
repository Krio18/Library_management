using System.Text;

namespace ClassPlantUMLHypertext
{
    public class PlantUMLHypertext
    {
		private StringBuilder _PlantUML { get; set; }
        public String Hypertext()
        {
            _PlantUML.Append(@"@startuml");
            _PlantUML.Append(@"class Program
        {
			- _PlantUML : string
	        Hypertext()
            [[mainDiagram.svg]]
        }");
            _PlantUML.Append(@"@enduml");

            return _PlantUML.ToString();
        }
    }
}
