using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using VipCon.Data;

namespace VipCon.Pages.Prospect
{
    public class IndexModel : PageModel
    {
        private readonly VipCon.Data.ApplicationDbContext _context;
        private IHostingEnvironment _hostingEnvironment;

        public IndexModel(VipCon.Data.ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public IList<VipCon.Models.Prospect> Prospect { get;set; }

        public async Task OnGetAsync()
        {
            Prospect = await _context.Prospect.ToListAsync();
        }

        public async Task<IActionResult> OnPostExport()
        {
            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = @"Lista Simulações.xlsx";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            var memory = new MemoryStream();
            using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook;
                workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("Simulações");

                IList<VipCon.Models.Prospect> prospects = await _context.Prospect.ToListAsync();
                IRow row = excelSheet.CreateRow(0);

                row.CreateCell(0).SetCellValue("Nome");
                row.CreateCell(1).SetCellValue("Telefone");
                row.CreateCell(2).SetCellValue("Email");
                row.CreateCell(3).SetCellValue("Modalidade Consórcio");
                row.CreateCell(4).SetCellValue("Data Simulação");
                row = excelSheet.CreateRow(1);

                int numeroLinha = 1;
                foreach (VipCon.Models.Prospect prop in prospects)
                {
                    numeroLinha++;
                    row.CreateCell(0).SetCellValue(prop.Nome);
                    row.CreateCell(1).SetCellValue(prop.Telefone);
                    row.CreateCell(2).SetCellValue(prop.Email);
                    row.CreateCell(3).SetCellValue(prop.ModalidadeConsorcio);
                    row.CreateCell(4).SetCellValue(prop.DataSimulacao.ToString("dd/MM/yyyy HH:mm"));

                    row = excelSheet.CreateRow(numeroLinha);
                }

                workbook.Write(fs);
            }
            using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);
        }
    }
}
