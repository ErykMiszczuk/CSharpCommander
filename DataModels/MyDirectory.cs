using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CSharpCommander.DataModels
{
    
    class MyDirectory : DiscElement
    {

        ///// <summary>
        ///// konstruktor klasy mydirectory
        ///// </summary>
        ///// <param name="dirpath">ścieżka folderu</param>
        //public mydirectory(string dirpath)
        //{
        //    this.dirpath = dirpath;
        //    creationtime = directory.getcreationtime(dirpath);
        //}

        public MyDirectory (string dirPath) : base(dirPath)
        {
           
        }

        public List<DiscElement> GetSubDiscElements()
        {
            List<DiscElement> result = new List<DiscElement>();
            result.AddRange(GetSubDirectories());
            result.AddRange(GetAllFiles());
            return result;
        }

        public override DateTime CreationTime
        {
            get
            {
                return Directory.GetCreationTime(Path);
            }
        }

        /// <summary>
        /// Nazwa folderu
        /// </summary>
        //string name;

        public override string Name
        {
            get
            {
                return System.IO.Path.GetDirectoryName(Path);
            }
        }


        public int NumberOfSubDirs
        {
            get
            {
                return Directory.GetDirectories(Path).Length; 
            }    
        }

        /// <summary>
        /// zwraca szystkie pliki w folderze
        /// </summary>
        /// <returns></returns>
        public List<MyFile> GetAllFiles()
        {
            string[] subFiles = Directory.GetFiles(Path);

            List<MyFile> result = new List<MyFile>();
            foreach (string file in subFiles)
            {
                result.Add(new MyFile(file));
            }
            return result;

        }


        /// <summary>
        /// zwraca wszytskie foldery i podfoldery rekurencyjnie danego folderu
        /// </summary>
        /// <returns></returns>
        public List<MyDirectory> GetSubDirectoriesRecursively()
        {
            string[] subDirs = Directory.GetDirectories(Path);

            List<MyDirectory> result = new List<MyDirectory>();
            foreach (string dir in subDirs)
            {
                MyDirectory subDir = new MyDirectory(dir);
                result.Add(subDir);
                result.AddRange(subDir.GetSubDirectoriesRecursively());
            }
            result = result.OrderBy(o => o.Path).ToList();
            //result.Reverse();
            return result;
        }
        

        /// <summary>
        /// Pobierz wszytskie swoje podfoldery
        /// </summary>
        /// <returns>zwraca listę podfolderów danego folderu</returns>
        public MyDirectory[] GetSubDirectories()
        {
            List<MyDirectory> result = new List<MyDirectory>();
            try
            {

                string[] subDirs = Directory.GetDirectories(Path);

                foreach (string dir in subDirs)
                {
                    result.Add(new MyDirectory(dir));
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return result.ToArray();

        }

        public List<MyFile> GetAllFilesRecursively()
        {
            List<MyDirectory> subDirs = GetSubDirectoriesRecursively();
            List<MyFile> result = new List<MyFile>();
            foreach( MyDirectory dir in subDirs)
            {
                result.AddRange(dir.GetAllFiles());
            }
            return result.OrderBy(o=>o.Name).ToList();
        }

        public int NumberOfRecursiveFiles
        {
            get
            {
                return GetAllFilesRecursively().Count;
            }
        }

        public override string GetDescription
        {
            get
            {
                return Path + " creationTime:" + CreationTime + " NumberOfSubDir" + NumberOfSubDirs;
            }
        }



        /// <summary>
        /// zwraca ładny opis folderu oraz jego czas stworzenia i liczbę podfolderów
        /// </summary>
        /// <returns></returns>
        public string GetNiceDescription()
        {
            return Path + " creationTime:" + CreationTime + " NumberOfSubDir" + NumberOfSubDirs;
        }
    }
}
