using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNPM.Core.Models;
using CNPM.Core.Models.CanHo;
using Microsoft.AspNetCore.Mvc;
namespace CNPM.Service.Interfaces
{
    public interface ICanHoService
    {
        public IActionResult GetListCanHo(int index, int limit);
        public IActionResult GetCanHo(int maCanHo);
        public IActionResult CreateCanHo(string token, CanHoDto1000 newCanHo);
        public IActionResult UpdateCanHo(string token, int maCanHo, CanHoDto1002 newCanHo);
        public IActionResult DeleteCanHo(int maCanHo, string token, int version);
    }
}
