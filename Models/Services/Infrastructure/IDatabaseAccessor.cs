//COME ESTRARRE

using System;
using System.Data;
using System.Threading.Tasks;

namespace MyCourse.Models.Services.Infrastructure
{
    public interface IDatabaseAccessor
    {
        //qui asynch non è necessario
        Task<DataSet> QueryAsync(FormattableString query);
    }
}