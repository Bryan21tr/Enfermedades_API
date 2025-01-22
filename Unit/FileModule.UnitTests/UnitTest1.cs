using TsaakAPI.Model.DAO;
using ActivoFijoAPI.Util;
using Xunit;
using TsaakAPI.Entities;
using Moq;
using System.Collections.Generic;

namespace FileModule.UnitTests;

public interface IEfermedadCardiovascularDao
{
    Task<ResultOperation<VMCatalog>> GetByIdAsync(int id);
    Task<ResultOperation<List<VMCatalog>>> GetAll();
    Task<ResultOperation<VMCatalog>> Create(VMCatalog vmCatalog);
    Task<ResultOperation<VMCatalog>> Update(VMCatalog vmCatalog, int id);
}

public class ResultOperation<T>
{
    public bool Success { get; set; }
    public T Data { get; set; }
}

public class UnitTest1
{
    [Fact]
    public async Task GetById()
    {
        var mockDao = new Mock<IEfermedadCardiovascularDao>();
        var vmCatalog = new VMCatalog { Id = 5 };
        mockDao.Setup(x => x.GetByIdAsync(5)).ReturnsAsync(new ResultOperation<VMCatalog> { Success = true, Data = vmCatalog });

        var resultado = await mockDao.Object.GetByIdAsync(5);
        Assert.Equal(true, resultado.Success);
    }

[Fact]
public async Task GetAllX()
    {
        var Lista = new List<VMCatalog>();
        var mockDao = new Mock<IEfermedadCardiovascularDao>();
        var vmCatalog = new VMCatalog { Id = 1 };
        Lista.Add(vmCatalog);

        mockDao.Setup(x => x.GetAll()).ReturnsAsync(new ResultOperation<List<VMCatalog>> { Success = true,  Data = Lista });

        var resultado = await mockDao.Object.GetAll();
        Assert.Equal(true, resultado.Success);
    }

[Fact]
    public async Task GetCreate()
    {
        var mockDao = new Mock<IEfermedadCardiovascularDao>();
        var vmCatalog = new VMCatalog { Id = 1, Nombre = "Unit", Descripcion = "xunit", Estado = true };
       
        mockDao.Setup(x => x.Create(It.IsAny<VMCatalog>())).ReturnsAsync(new ResultOperation<VMCatalog> { Success = true,  Data = vmCatalog });

        var resultado = await mockDao.Object.Create(vmCatalog);
        Assert.Equal(true, resultado.Success);
    }

   [Fact]
    public async Task GetUpdate()
    {
        var mockDao = new Mock<IEfermedadCardiovascularDao>();
        var vmCatalog = new VMCatalog { Id = 1, Nombre = "Unit", Descripcion = "xunit", Estado = true };
       
        mockDao.Setup(x => x.Update(It.IsAny<VMCatalog>(), 1)).ReturnsAsync(new ResultOperation<VMCatalog> { Success = true,  Data = vmCatalog });

        var resultado = await mockDao.Object.Update(vmCatalog, 1);
        Assert.Equal(true, resultado.Success);
    }
}