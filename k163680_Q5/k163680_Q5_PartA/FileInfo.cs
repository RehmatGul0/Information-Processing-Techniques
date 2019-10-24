using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace k163680_Q5_PartA
{
    public class FileInfo
    {
        private String name;
        private String fullPath;
        public FileInfo(String name, String fullPath)
        {
            this.name = name;
            this.fullPath = fullPath;
        }

        public String getName()
        {
            return this.name;
        }
        public String getFullPath()
        {
            return this.fullPath;
        }
    }
}
