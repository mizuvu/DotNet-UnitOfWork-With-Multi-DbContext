using Base.Application.Abstractions.DataAccess;
using Microsoft.AspNetCore.Mvc;
using SampleApi.Data.SQLite;
using SampleApi.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly IUnitOfWork<ISampleMssqlContext> _mssqlUnitOfWork;

        private readonly IUnitOfWork<SampleSqliteContext> _sqliteUnitOfWork;

        public DemoController(
            IUnitOfWork<ISampleMssqlContext> mssqlUnitOfWork,
            IUnitOfWork<SampleSqliteContext> sqliteUnitOfWork)
        {
            _mssqlUnitOfWork = mssqlUnitOfWork;
            _sqliteUnitOfWork = sqliteUnitOfWork;
        }

        [HttpGet("mssql")]
        public async Task<IActionResult> GetFromMssql()
        {
            var response = await _mssqlUnitOfWork.Repository<Data.Mssql.Product>().GetAllAsync();
            return Ok(response);
        }

        
        [HttpGet("sqlite")]
        public async Task<IActionResult> GetFromSqlite()
        {
            var response = await _sqliteUnitOfWork.Repository<Data.SQLite.Product>().GetAllAsync();
            return Ok(response);
        }
    }
}
