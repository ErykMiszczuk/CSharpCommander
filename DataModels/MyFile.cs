using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace CSharpCommander.DataModels
{
    /// <summary>
    /// Klasa opisująca strukturę pliku
    /// </summary>
    class MyFile : DiscElement
    {

       //private string name;

        public MyFile(string Path) : base(Path)
        {
         
        }

        public override DateTime CreationTime
        {
            get
            {
                return File.GetCreationTime(Path);
            }
        }

        public override string Name
        {
            get
            {
                return System.IO.Path.GetFileName(Path);
            }
        }

        public override string GetDescription
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
