using Homework_7_2.Models.Domains;
using Homework_7_2.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_7_2.Models.Converters
{
    public static class StatusConverter
    {
        public static StatusWrapper ToWrapper(this Status model)
        {
            return new StatusWrapper()
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        public static Status ToDao(this StatusWrapper model)
        {
            return new Status()
            {
                Id = model.Id,
                Name = model.Name
            };
        }

    }
}
