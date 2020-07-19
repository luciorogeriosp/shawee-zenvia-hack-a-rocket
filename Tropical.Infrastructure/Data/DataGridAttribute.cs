using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tropical.Infrastructure.Data
{
    public class DataGridAttribute : Attribute
    {
        public enum DataHideType
        {
            None = 0x0,
            Phone = 0x1,
            Tablet = 0x2
        }

        public bool DataHidePhone { get; set; }

        public bool DataHideTablet { get; set; }

        public DataHideType DataHide { get; set; }

        public String DataFormat { get; set; }

        public DataGridAttribute(DataHideType DataHide)
        {
            this.DataHide = DataHide;
            this.FormatInit();
        }
        public DataGridAttribute(DataHideType DataHide, String DataFormat)
        {
            this.DataHide = DataHide;
            this.DataFormat = DataFormat;
            this.FormatInit();
        }

        private void FormatInit()
        {
            if ((this.DataHide & DataHideType.Phone) == DataHideType.Phone)
            {
                this.DataHidePhone = true;
            }

            if ((this.DataHide & DataHideType.Phone) == DataHideType.Phone)
            {
                this.DataHideTablet = true;
            }
        }
    }
}

