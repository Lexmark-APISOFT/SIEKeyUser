//-- =============================================
//--Author:		< Jorge Zamora >
//-- =====================================================================================================================
//--CODE        | NAME                                | MODIFIED DATE       | DESCRIPTION
//-- =====================================================================================================================
//-NA           Daniel Omar Mendoza Rodriguez 51105		06/02/2023			 


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIE_KEY_USER.model
{
    public class RequestFile
    {
        public int requestId;
        public string tipoCarta;
        public string filePath;
        public string fileName;
        public string printingDate;


        public RequestFile() { 
        }
        public RequestFile(int requestId,string tipoCarta,string filePath, string fileName, string printingDate)
        {
            this.requestId = requestId;
            this.tipoCarta = tipoCarta;
            this.filePath = filePath;
            this.fileName = fileName;
            this.printingDate = printingDate;   
        }

    }
}