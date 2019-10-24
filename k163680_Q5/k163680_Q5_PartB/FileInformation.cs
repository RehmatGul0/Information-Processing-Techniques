using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace k163680_Q5_PartB
{
    public class FileInformation
    {
        private String name;
        private String fullPath;
        private long size;
        public FileInformation(String name, String fullPath , long size)
        {
            this.name = name;
            this.fullPath = fullPath;
            this.size = size;
        }

        public String getName()
        {
            return this.name;
        }
        public String getFullPath()
        {
            return this.fullPath;
        }
        public long getSize()
        {
            return this.size;
        }
    }
}
