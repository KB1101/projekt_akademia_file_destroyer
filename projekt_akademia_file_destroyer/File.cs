using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Tutaj pokazuje, że umiem dziedziczenie własnych klas i własne interfejsy :P */
namespace projekt_akademia_file_destroyer
{
    /* Klasa File dziedziczy po klasie Fileinformator i implementuje interfejs iFileUpdate,
     * Tak wiem nadpisuje tym System.IO.File w przestrzeni nazw - chciałem pokazać, że wiem jak sobie z tym radzić */
    class File : FileInformator,IFileInfoUpdate
    {
        
        public int id { get; set; } // id pliku
        public string status { get; set; } // status pliku

        public File(int counter,string fileUrl)
            :base(fileUrl)
        {
            id = counter + 1;
            status = "Waiting...";
           
        }

        public void StatusUpdate(string newStatus) // Zmiana statusu pliku
        {
            this.status = newStatus;
        }

        public void SizeUpdate() // Zmiana rozmiaru pliku , gdyby zaszła taka potrzeba uaktualnienia
        {
            var fileInfo = new System.IO.FileInfo(fileFullPath);
            fileSize = (long)System.Math.Ceiling(fileInfo.Length / 1024.0);
        }
    }

    class FileInformator : IEquatable<String> 
    {

        public string fileName { get; set; } // nazwa pliku
        public string fileFullPath { get; set; } // pełna ścieżka do pliku
        protected long fileSize;

        public string fileDetails
        {
            get
            {
                return String.Format("Full path to file {0}\nFile size: {1}kB", this.fileFullPath, this.fileSize);
            }
        }


        public FileInformator(string filePath)
        {
            var fileInfo = new System.IO.FileInfo(filePath);
            fileSize = (long)System.Math.Ceiling(fileInfo.Length / 1024.0);
            fileName = System.IO.Path.GetFileName(filePath);
            fileFullPath = filePath;
        }

        // Implementacja interfejsu IEquatable<T> 
        public bool Equals(string fileInfo)
        {
            if ((string)this.fileFullPath == (string)fileInfo) return true;
            else return false;
        }


    }
    interface IFileInfoUpdate{
        void SizeUpdate();
        void StatusUpdate(string newStatus);
    }
}
