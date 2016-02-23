using System.Collections.Generic;
using ReportMS.DataTransferObjects.Dtos;

namespace ReportMS.Web.Client.Models
{
    public class ReportModifyViewModel
    {
        public ReportDto Report { get; set; }

        public IEnumerable<TableSchemaDto> TableSchemas { get; set; }
    }
}
