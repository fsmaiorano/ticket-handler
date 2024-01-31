using Application.UseCases.Holder.Queries;
using IntegrationTest.Application.UseCases.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Application.UseCases.Holder;

[TestClass]
public class DeleteHolderIntegrationTest : Testing
{
    [TestInitialize]
    public void TestInitialize()
    {

    }

    [TestMethod]
    public async Task DeleteHolder()
    {
        var holder = CreateHolderIntegrationTest.CreatedHolder;

        var command = new DeleteHolderCommand
        {
            Id = holder!.Id
        };

        var deletedHolderId = await SendAsync(command);
        Assert.IsNotNull(deletedHolderId);
        Assert.IsInstanceOfType(deletedHolderId, typeof(Guid));

        var query = new GetHolderByIdQuery
        {
            Id = holder.Id
        };

        var deletedHolder = await SendAsync(query);
        Assert.IsNull(deletedHolder);
    }
}
