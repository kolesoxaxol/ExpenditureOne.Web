using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ExpenditureOne.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenditureOne.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenditureController : ControllerBase
    {
        public readonly IExpenditureService _expenditureService;
        private readonly IMapper _mapper;

        public ExpenditureController(IMapper mapper, IExpenditureService expenditureService)
        {
            _expenditureService = expenditureService;
            _mapper = mapper;
        }
    }
}
