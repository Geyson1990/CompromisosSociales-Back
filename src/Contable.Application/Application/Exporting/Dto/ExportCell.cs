using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Exporting.Dto
{
    public class ExportCell
    {
        public ExportCell(decimal value, bool hasEmpty = false)
        {
            Decimal = value;
            Type = ExportCellType.Decimal;
            HasEmpty = hasEmpty;
        }

        public ExportCell(string value, ExportCellType type, bool hasEmpty = true)
        {
            Value = value;
            Type = type;
            HasEmpty = hasEmpty;
        }

        public ExportCell(DateTime? value, string format, ExportCellType type = ExportCellType.DateTime)
        {
            DateTime = value;
            Format = format;
            Type = type == ExportCellType.DateTime ? ExportCellType.DateTime : ExportCellType.Date;
            HasEmpty = true;
        }

        public ExportCell(string value)
        {
            Value = value;
            Type = ExportCellType.String;
        }

        public decimal Decimal { get; set; }
        public bool HasEmpty { get; set; }
        public string Format { get; set; }
        public DateTime? DateTime { get; set; }
        public string Value { get; set; }
        public ExportCellType Type { get; set; }
    }

    public enum ExportCellType
    {
        Numeric,
        Decimal,
        String,
        Date,
        DateTime
    }
}
