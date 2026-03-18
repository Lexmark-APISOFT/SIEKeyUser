using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIE_KEY_USER.model
{
    public class File
    {
        public int requestID;
        public string tipoCarta;
        public string fechaImpresion;
        public string filePath;
        public string fileName;


        public File() { 
        
        }
        public File(int requestID,string tipoCarta, string fechaImpresion, string filePath, string fileName) {
            this.requestID = requestID;
            this.tipoCarta = tipoCarta;
            this.fileName = fileName;
            this.fechaImpresion = fechaImpresion;
            this.filePath = filePath;   
        }

        public void DeleteFile(File file) { 
        }
    }
}